#load "../Utils/Shell.csx"
#load "../Utils/Prompt.csx"
#load "../Utils/Log.csx"
#load "../Utils/Gpg.csx"
#load "../Models/ProjectConfig.csx"

using System.Globalization;
using System.Text.RegularExpressions;

public static class Configurator
{
    public static ProjectConfig PreparePlan()
    {
        var config = new ProjectConfig();

        // PHASE 1: Auto-discovery (Non-blocking)
        Log.Info("Scanning environment...");
        config.GitUser = GetGithubUser();
        config.GitMail = Shell.Run("git", "config user.email", hideOutput: true);
        config.GitRepo = GetRepoName();

        // PHASE 2: User Input (Grouped by context)
        Log.Info("--- Package Identity ---");
        config.Domain = ToLowerWithWarning(Prompt.Ask("Top level domain", "com"));
        config.Company = ToLowerWithWarning(Prompt.Ask("Company name", config.GitUser.ToLower()));
        config.Package = ToLowerWithWarning(Prompt.Ask("Package name", config.GitRepo.ToLower()));
        config.Namespace = Prompt.Ask("Namespace", KebabToPascal(config.Package));
        config.Description = Prompt.Ask("Description", "");
        config.DisplayName = ToWords(config.Namespace);
        config.LicenseType = Prompt.AskInList(
            "License",
            new[] { "mit", "apache-2.0", "gpl-3.0", "isc" },
            1
        );

        Log.Info("--- CI/CD & Secrets ---");
        config.UnityLicensePath = ExpandPath(
            Prompt.Ask("Path to Unity .ulf license", GetUnityLicensePath())
        );
        config.UnityEmail = Prompt.Ask("Unity Email", config.GitMail);
        config.UnityPassword = Prompt.AskPassword("Unity Password");
        config.SonarUrl = Prompt.Ask("SonarQube URL", "https://sonarcloud.io");
        config.SonarToken = Prompt.AskNonNull("Sonar Token");
        config.GpgKey = Gpg.GenerateCiKey();

        Log.Info("Configuration plan completed.");
        Log.Info(config.ToString());

        return config;
    }

    private static string GetGithubUser()
    {
        try
        {
            var url = Shell.Run("git", "remote get-url origin", hideOutput: true);
            var match = Regex.Match(url, @"github\.com[:/](.+)/");
            if (match.Success)
                return match.Groups[1].Value;
        }
        catch { }
        return Shell.Run("git", "config user.name", hideOutput: true);
    }

    private static string GetRepoName()
    {
        try
        {
            var url = Shell.Run("git", "remote get-url origin", hideOutput: true);
            return Path.GetFileNameWithoutExtension(url.Split('/').Last());
        }
        catch
        {
            return Path.GetFileName(Directory.GetCurrentDirectory());
        }
    }

    private static string KebabToPascal(string input) =>
        string.Concat(input.Split('-').Select(CultureInfo.CurrentCulture.TextInfo.ToTitleCase));

    private static string ToWords(string input) => Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");

    private static string ToLowerWithWarning(string input)
    {
        if (input != input.ToLower())
            Log.Warning($"Converting '{input}' to lowercase.");
        return input.ToLower();
    }

    private static string GetUnityLicensePath()
    {
        if (OperatingSystem.IsWindows())
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Unity",
                "Unity_lic.ulf"
            );
        else if (OperatingSystem.IsMacOS())
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "Library",
                "Application Support",
                "Unity",
                "Unity_lic.ulf"
            );
        else
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".local",
                "share",
                "unity3d",
                "Unity",
                "Unity_lic.ulf"
            );
    }

    private static string ExpandPath(string path)
    {
        // Expand tilde
        if (path.StartsWith("~"))
        {
            path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                path.Substring(2)
            );
        }

        // Expand environment variables
        path = Environment.ExpandEnvironmentVariables(path);

        return path;
    }
}
