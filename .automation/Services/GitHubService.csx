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
    private static string Gh(string args, bool hide = false) => Shell.Run("gh", args, hide);

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
        SetSecret("GPG_KEY_ID", config.GpgKey.KeyId);
        SetSecret("GPG_PRIVATE_KEY", config.GpgKey.PrivateKey);
    }

    /// <summary>
    /// Sets GitHub Pages to use the Actions workflow for deployment
    /// </summary>
    /// <param name="repoFullName"> The full name of the repository (e.g
    /// "owner/repo"). </param>
    public static void SetPagesToWorkflow(ProjectConfig config)
    {
        Log.Info("Setting GitHub Pages to use Actions workflow...");
        void pageApi(string method) =>
            Gh(
                $"api -X {method} \"/repos/{config.RepoFullName}/pages\" -f \"build_type=workflow\""
            );
        try
        {
            pageApi("POST");
        }
        catch
        {
            Log.Info("Pages already enabled...");
        }
        pageApi("PUT");
    }

    /// <summary>
    /// Retrieves the license text from GitHub for the specified license type
    /// </summary>
    /// <param name="licenseType"> The type of license (e.g., "mit", "apache-2.0"). </param>
    /// <returns> The license text. </returns>
    public static string GetLicense(string licenseType) =>
        Gh($"api licenses/{licenseType} --jq .body", hide: true);

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
        var content = File.ReadAllText(filePath).Replace("\"", "\\\"");
        Gh($"secret set {name} --body \"{content}\"", hide: true);
    }

    /// <summary>
    /// Protects the repository with basic rulesets for tags and branches
    /// <summary>
    public static void ProtectRepository(ProjectConfig config)
    {
        CreateRuleset(
            config,
            "Branch Protection",
            "branch",
            new[] { "refs/heads/main", "refs/heads/develop" }
        );
        CreateRuleset(config, "Tag Protection", "tag", new[] { "refs/tags/*" });
    }

    private static void CreateRuleset(
        ProjectConfig config,
        string name,
        string target,
        string[] includes
    )
    {
        Log.Info($"Applying {name} Ruleset...");
        var rules = new List<string>
        {
            "{\"type\": \"deletion\"}",
            "{\"type\": \"non_fast_forward\"}",
        };
        var rulesetJson =
            $@"
    {{
      ""name"": ""{name}"",
      ""target"": ""{target}"",
      ""enforcement"": ""active"",
      ""conditions"": {{
        ""ref_name"": {{
          ""include"": [{string.Join(",", includes.Select(i => $"\"{i}\""))}],
          ""exclude"": []
        }}
      }},
      ""rules"": [{string.Join(",", rules)}]
    }}";
        var tempPath = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempPath, rulesetJson);
            Gh(
                $"api -X POST \"/repos/{config.RepoFullName}/rulesets\" --input \"{tempPath}\"",
                hide: true
            );
        }
        catch
        {
            Log.Info($"{name} already exists, skipping...");
        }
        finally
        {
            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }
    }
}
