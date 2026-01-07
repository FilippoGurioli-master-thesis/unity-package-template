#load "../Utils/Shell.csx"
#load "../Utils/Log.csx"
#load "../Models/ProjectConfig.csx"

/// <summary>
/// Provides services for interacting with GitHub via the GitHub CLI.
/// </summary>
public static class GitHubService
{
    /// <summary>
    /// Helper method to run GitHub CLI commands
    /// </summary>
    /// <param name="args"> The arguments to pass to the gh command. </param>
    /// <param name="hide"> Whether to hide the command output. </param>
    /// <returns> The output of the gh command. </returns>
    private static string Gh(string args, bool hide = false)
        => Shell.Run("gh", args, hide);

    /// <summary>
    /// Pushes necessary secrets to the GitHub repository
    /// </summary>
    /// <param name="config"> The project configuration containing secret details. </param>
    public static void PushSecrets(ProjectConfig config)
    {
        Log.Info("Pushing secrets to GitHub...");
        SetSecretFromFile("UNITY_LICENSE", config.UnityLicensePath);
        SetSecret("UNITY_EMAIL", config.UnityEmail);
        SetSecret("UNITY_PASSWORD", config.UnityPassword);
        SetSecret("SONAR_HOST_URL", config.SonarUrl);
        SetSecret("SONAR_TOKEN", config.SonarToken);
        SetSecret("RELEASE_STEP_PAT", config.PersonalAccessToken);
        SetSecret("GPG_KEY_ID", config.GpgKey.KeyId);
        SetSecret("GPG_PRIVATE_KEY", config.GpgKey.PrivateKey);
    }

    /// <summary>
    /// Protects main and develop branches on GitHub by applying default protection rules
    /// </summary>
    /// <param name="config"> The project configuration containing GitHub repository details. </param>
    public static void ProtectBranches(ProjectConfig config)
    {
        Log.Info("Protecting main branch...");
        ProtectBranch($"{config.RepoFullName}", "main");
        Log.Info("Protecting develop branch...");
        ProtectBranch($"{config.RepoFullName}", "develop");
    }

    /// <summary>
    /// Protects all tags on GitHub by applying default protection rules
    /// </summary>
    /// <param name="config"> The project configuration containing GitHub repository details. </param>
    public static void ProtectTags(ProjectConfig config)
    {
        Log.Info("Applying protection to all tags...");
        Gh($"api -X POST \"/repos/{config.RepoFullName}/tag_protection\" -f \"pattern=*\"", hide: true);
    }

    /// <summary>
    /// Sets GitHub Pages to use the Actions workflow for deployment
    /// </summary>
    /// <param name="repoFullName"> The full name of the repository (e.g
    /// "owner/repo"). </param>
    public static void SetPagesToWorkflow(ProjectConfig config)
    {
        Log.Info("Setting GitHub Pages to use Actions workflow...");
        Gh($"api -X PATCH \"/repos/{config.RepoFullName}/pages\" -f \"build_type=workflow\"");
    }

    /// <summary>
    /// Retrieves the license text from GitHub for the specified license type
    /// </summary>
    /// <param name="licenseType"> The type of license (e.g., "mit", "apache-2.0"). </param>
    /// <returns> The license text. </returns>
    public static string GetLicense(string licenseType) =>
        Shell.Run("gh", $"api licenses/{licenseType} --jq .body", hideOutput: true);

    /// <summary>
    /// Sets a GitHub secret for the repository
    /// </summary>
    /// <param name="name"> The name of the secret. </param>
    /// <param name="value"> The value of the secret. </param>
    public static void SetSecret(string name, string value)
    {
        Log.Info($"Setting GitHub secret: {name}");
        // Using --body prevents issues with special characters in the shell
        Gh($"secret set {name} --body \"{value}\"", hide: true);
    }

    /// <summary>
    /// Sets a GitHub secret for the repository from a file
    /// </summary>
    /// <param name="name"> The name of the secret. </param>
    /// <param name="filePath"> The path to the file containing the secret value. </param>
    public static void SetSecretFromFile(string name, string filePath)
    {
        Log.Info($"Uploading secret {name} from file...");
        Gh($"secret set {name} < \"{filePath}\"", hide: true);
    }

    /// <summary>
    /// Protects a branch on GitHub by applying default protection rules
    /// </summary>
    /// <param name="repoFullName"> The full name of the repository (e.g
    /// "owner/repo"). </param>
    /// <param name="branch"> The branch to protect (e.g., "main"). </param>
    public static void ProtectBranch(string repoFullName, string branch)
    {
        Log.Info($"Applying protection to {branch}...");
        var json = "{\"required_status_checks\":null,\"enforce_admins\":false,\"required_pull_request_reviews\":null,\"restrictions\":null,\"allow_force_pushes\":false,\"allow_deletions\":false}";
        string tempJsonPath = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempJsonPath, json);
            Gh($"api -X PUT \"/repos/{repoFullName}/branches/{branch}/protection\" --input \"{tempJsonPath}\"", hide: true);
        }
        finally
        {
            if (File.Exists(tempJsonPath)) File.Delete(tempJsonPath);
        }
    }
}