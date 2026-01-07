#load "../Utils/Log.csx"
#load "../Utils/Shell.csx"
#load "../Models/ProjectConfig.csx"

/// <summary>
/// Provides services for Unity project setup such as installing dependencies.
/// </summary>
public static class UnityService
{
    public static void OpenProject(ProjectConfig config, bool batch = false)
    {
        Log.Info("Opening Unity project...");
        string args = $"-projectPath ./Sandbox.{config.Namespace} -logFile ./Sandbox.{config.Namespace}/unity.log";
        if (batch)
        {
            args = "-batchmode -nographics -quit " + args;
        }
        try
        {
            Shell.Run(config.UnityEditorPath, args, hideOutput: true);
        }
        catch
        {
            Log.Error("Failed to open Unity project. Check the ./Sandbox." + config.Namespace + "/unity.log file for more details.");
            throw;
        }
    }
}