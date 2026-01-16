#load "../Utils/Log.csx"
#load "../Utils/Shell.csx"
#load "../Models/ProjectConfig.csx"
#load "UnityService.csx"

/// <summary>
/// Provides services for environment setup such as installing dependencies.
/// </summary>
public static class EnvironmentService
{
    /// <summary>
    /// Installs necessary dependencies for the project environment.
    /// </summary>
    public static void InstallDependencies()
    {
        Log.Info("Installing dependencies...");
        Shell.Run("dotnet", "tool restore");
        Shell.Run("npm", "install");
    }

    /// <summary>
    /// Installs Git hooks using Lefthook.
    /// </summary>
    public static void InstallHooks()
    {
        Log.Info("Installing Git hooks...");
        Shell.Run("dotnet", "tool run husky install");
    }

    /// <summary>
    /// Deletes template initialization files that are no longer needed.
    /// </summary>
    public static void DeleteTemplateFiles()
    {
        Log.Info("Deleting template files...");
        var templateFiles = new string[] { ".template", "init.sh", "init.ps1" };
        foreach (var file in templateFiles)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                Log.Info($"Deleted: {file}");
            }
            else
            {
                Log.Warning($"File not found, skipping: {file}");
            }
        }
    }

    /// <summary>
    /// Generates a single SDK-style .csproj for the Unity package to support DocFX/IDEs.
    /// </summary>
    public static void GenerateCsproj(ProjectConfig config)
    {
        Log.Info("Generating documentation .csproj...");
        var packageRoot = Path.Combine(Environment.CurrentDirectory, config.Namespace);
        var sandboxRoot = Path.Combine(Environment.CurrentDirectory, config.SandboxPath);
        var scriptAssemblies = Path.Combine(sandboxRoot, "Library", "ScriptAssemblies");

        // 1. Validation
        if (!Directory.Exists(scriptAssemblies))
        {
            Log.Error(
                "ScriptAssemblies not found. You must open Unity at least once to generate assemblies."
            );
            throw new DirectoryNotFoundException("ScriptAssemblies directory not found.");
        }

        // 2. Resolve Unity Version and Path
        var unityRoot = UnityService.DetectUnityInstall(config);

        // 3. Collect DLLs (Unity + Project Assemblies)
        var dllPaths = new List<string>();
        dllPaths.AddRange(FindDlls(Path.Combine(unityRoot, "Data", "Managed")));
        dllPaths.AddRange(FindDlls(Path.Combine(unityRoot, "Data", "Managed", "UnityEngine")));
        dllPaths.AddRange(FindDlls(scriptAssemblies));

        // 4. Build XML Content
        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        sb.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
        sb.AppendLine("  <PropertyGroup>");
        sb.AppendLine("    <TargetFramework>netstandard2.1</TargetFramework>");
        sb.AppendLine("    <GenerateDocumentationFile>true</GenerateDocumentationFile>");
        sb.AppendLine("    <NoWarn>1591</NoWarn>");
        sb.AppendLine("  </PropertyGroup>");
        sb.AppendLine("  <ItemGroup>");

        foreach (var dll in dllPaths)
        {
            var relativePath = Path.GetRelativePath(packageRoot, dll).Replace("\\", "/");
            var name = Path.GetFileNameWithoutExtension(dll);
            sb.AppendLine($"    <Reference Include=\"{name}\">");
            sb.AppendLine($"      <HintPath>{relativePath}</HintPath>");
            sb.AppendLine($"    </Reference>");
        }

        sb.AppendLine("  </ItemGroup>");
        sb.AppendLine("</Project>");

        // 5. Write File
        var outputPath = Path.Combine(packageRoot, $"{config.Namespace}.csproj");
        File.WriteAllText(outputPath, sb.ToString());
        Log.Info($"Generated {outputPath}");
    }

    private static IEnumerable<string> FindDlls(string path)
    {
        if (!Directory.Exists(path))
            return [];
        return Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
    }
}
