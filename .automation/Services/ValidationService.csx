#load "../Utils/Shell.csx"
#load "../Utils/Log.csx"
#load "UnityService.csx"

using System.Text.RegularExpressions;

/// <summary>
/// Provides services for validating the current environment.
/// </summary>
public static class ValidationService
{
    /// <summary>
    /// Validate the current environment.
    /// </summary>
    /// <return>True if the current env is valid, false otherwise</return>
    public static bool Validate()
    {
        return CheckUnityCompatibility()
            && IsCommandInstalled("gh", new Version(2, 83, 0))
            && IsCommandInstalled("node", new Version(25, 3, 0))
            && IsCommandInstalled("npm", new Version(11, 7, 0))
            && IsCommandInstalled("dotnet", new Version(8, 0, 0))
            && IsCommandInstalled("gpg", new Version(2, 4, 0));
    }

    private static bool CheckUnityCompatibility()
    {
        try
        {
            UnityService.DetectUnityInstall(new ProjectConfig { Namespace = "__NAMESPACE__" });
            Log.Info("Unity installed version is compatible with this project.");
            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return false;
        }
    }

    private static bool IsCommandInstalled(string command, Version required)
    {
        try
        {
            string output = Shell.Run(command, "--version", hideOutput: true);
            var match = Regex.Match(output, @"(\d+\.\d+\.\d+)");
            if (!match.Success)
            {
                Log.Error(
                    $"Command {command} --version didn't dump any version. Cannot check {command} compatibility."
                );
                return false;
            }
            var installed = Version.Parse(match.Value);
            if (installed.Major > required.Major)
            {
                Log.Info($"Command {command} has a compatible version.");
                return true;
            }
            if (installed.Major < required.Major)
            {
                Log.Error(
                    $"Command {command} has not a compatible version (installed: {installed}, required: {required})."
                );
                return false;
            }
            if (installed.Minor < required.Minor)
            {
                Log.Error(
                    $"Command {command} has not a compatible version (installed: {installed}, required: {required})."
                );
                return false;
            }
            if (installed.Minor == required.Minor && installed.Build < required.Build)
            {
                Log.Error(
                    $"Command {command} has not a compatible version (installed: {installed}, required: {required})."
                );
                return false;
            }
            Log.Info($"Command {command} has compatible version.");
            return true;
        }
        catch
        {
            Log.Error(
                $"Command {command} not installed. Install it at least at version {required}."
            );
            return false;
        }
    }
}
