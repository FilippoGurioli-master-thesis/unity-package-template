#load "../Utils/Log.csx"
#load "../Utils/Shell.csx"
#load "../Models/ProjectConfig.csx"

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

/// <summary>
/// Provides services for Unity project setup such as installing dependencies.
/// </summary>
public static class UnityService
{
    private static string _unityBasePath = null;

    public static void OpenProject(ProjectConfig config, bool batch = false)
    {
        Log.Info("Opening Unity project, this may take a few minutes...");
        var args =
            $"-projectPath {config.SandboxPath} -logFile {Path.Combine(config.SandboxPath, "unity_init.log")}";
        var unityEditorPath = Path.Combine(
            DetectUnityInstall(config),
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "MacOS/Unity"
                : RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Unity.exe"
                : "Unity"
        );
        if (batch)
        {
            args = "-batchmode -nographics -quit " + args;
        }
        try
        {
            if (!batch)
                new Thread(() => Shell.Run(unityEditorPath, args, hideOutput: true)).Start();
            else
                Shell.Run(unityEditorPath, args, hideOutput: true);
        }
        catch
        {
            Log.Error(
                "Failed to open Unity project. Check the "
                    + Path.Combine(config.SandboxPath, "unity_init.log")
                    + " file for more details."
            );
            throw;
        }
    }

    private static string GetUnityVersion(string sandboxPath)
    {
        string versionFile = Path.Combine(sandboxPath, "ProjectSettings", "ProjectVersion.txt");
        string content = File.ReadAllText(versionFile);
        var match = Regex.Match(content, @"m_EditorVersion:\s*(.+)");
        return match.Groups[1].Value.Trim();
    }

    /// <summary>
    /// Detects the installed Unity version that best matches the project's required version
    /// </summary>
    /// <param name="config"> The project configuration containing the sandbox path. </param>
    /// <returns> The path to the detected Unity installation. </returns>
    public static string DetectUnityInstall(ProjectConfig config)
    {
        if (_unityBasePath != null)
            return _unityBasePath;
        var projectVersion = GetUnityVersion(config.SandboxPath);
        // 1. Env Var still takes absolute priority (User knows best)
        var envPath = Environment.GetEnvironmentVariable("UNITY_EDITOR_PATH");
        if (!string.IsNullOrEmpty(envPath) && Directory.Exists(envPath))
        {
            _unityBasePath = envPath;
            return envPath;
        }
        _unityBasePath = null;
        // 2. Parse the Major version
        var projectMajor = int.Parse(projectVersion.Split('.')[0]);

        // 3. Define Hub paths based on OS
        string hubEditorsPath = "";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            hubEditorsPath = "/Applications/Unity/Hub/Editor";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            hubEditorsPath = @"C:\Program Files\Unity\Hub\Editor";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            hubEditorsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Unity/Hub/Editor"
            );

        if (Directory.Exists(hubEditorsPath))
        {
            // Get all installed versions (folders in the Hub directory)
            var installedVersions = Directory
                .GetDirectories(hubEditorsPath)
                .Select(Path.GetFileName)
                .Select(v => new
                {
                    Raw = v,
                    Major = int.TryParse(v.Split('.')[0], out int m) ? m : -1,
                })
                .Where(v => v.Major >= projectMajor) // Rule: Major installed must be >= Project Major
                .OrderBy(v => v.Major) // Pick the closest match (lowest major that satisfies the condition)
                .FirstOrDefault();

            if (installedVersions != null)
            {
                var bestFitPath = Path.Combine(hubEditorsPath, installedVersions.Raw);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    _unityBasePath = Path.Combine(bestFitPath, "Unity.app/Contents");
                    return Path.Combine(bestFitPath, "Unity.app/Contents");
                }
                _unityBasePath = Path.Combine(bestFitPath, "Editor");
                return Path.Combine(bestFitPath, "Editor");
            }
        }

        throw new Exception(
            $"No suitable Unity installation found. Need Major >= {projectMajor}. "
                + $"Found no matches in {hubEditorsPath}."
        );
    }
}
