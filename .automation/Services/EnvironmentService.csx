#load "../Utils/Log.csx"
#load "../Utils/Shell.csx"

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
        Shell.Run("npx", "lefthook install");
    }

    /// <summary>
    /// Deletes template initialization files that are no longer needed.
    /// </summary>
    public static void DeleteTemplateFiles()
    {
        Log.Info("Deleting template files...");
        var templateFiles = new string[]
        {
            ".template",
            "init.sh",
            "init.ps1"
        };
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
}