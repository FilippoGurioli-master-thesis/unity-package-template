#load "../Utils/Shell.csx"
#load "../Utils/Log.csx"
#load "../Models/ProjectConfig.csx"
#load "UnityService.csx"

using System.Runtime.InteropServices;

/// <summary>
/// Provides services for Git repository setup such as configuring merge tools.
/// </summary>
public static class GitService
{
    /// <summary>
    /// Configures the local git repository to use UnityYAMLMerge for scene and prefab files.
    /// </summary>
    /// <param name="config">The project configuration to detect Unity installation.</param>
    public static void ConfigureSmartMerge(ProjectConfig config)
    {
        Log.Info("Setting Unity SmartMerge as a merge solver...");
        var unityBasePath = UnityService.DetectUnityInstall(config);
        var smartMergeBin = FindSmartMergeExecutable(unityBasePath);
        if (string.IsNullOrEmpty(smartMergeBin) || !File.Exists(smartMergeBin))
        {
            Log.Warning("UnityYAMLMerge not found. Scene merging will be manual.");
            return;
        }
        Log.Info($"Found UnityYAMLMerge at: {smartMergeBin}");
        Shell.Run("git", "config --local merge.unityyamlmerge.name \"Unity SmartMerge\"");
        Shell.Run(
            "git",
            $"config --local merge.unityyamlmerge.driver \"'{smartMergeBin}' merge -p %O %B %A %R\""
        );
        Shell.Run("git", "config --local merge.unityyamlmerge.trustExitCode false");
        Log.Info("Merge rules configured successfully.");
    }

    /// <summary>
    /// Commits all current changes in the git repository with a standard message.
    /// </summary>
    public static void CommitAllChanges()
    {
        Log.Info("Committing all changes...");
        Shell.Run("git", "add .");
        Shell.Run("git", "commit -m \"chore(init): initialize project from template\"");
    }

    /// <summary>
    /// Creates a base tag "0.0.0" in the git repository.
    /// </summary>
    public static void CreateBaseTag()
    {
        Log.Info("Creating base tag 0.0.0...");
        try
        {
            Shell.Run("git", "tag 0.0.0");
        }
        catch { } // tag already exist
    }

    public static void CheckoutDevelop()
    {
        Log.Info("Checking out develop branch...");
        try
        {
            Shell.Run("git", "checkout develop");
        }
        catch
        {
            Shell.Run("git", "git checkout -b develop");
        }
    }

    private static string FindSmartMergeExecutable(string unityBasePath)
    {
        var relativePath = RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
            ? Path.Combine("Helpers", "UnityYAMLMerge")
            : Path.Combine(
                "Data",
                "Tools",
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? "UnityYAMLMerge.exe"
                    : "UnityYAMLMerge"
            );
        var fullPath = Path.Combine(unityBasePath, relativePath);
        return File.Exists(fullPath) ? fullPath : null;
    }
}
