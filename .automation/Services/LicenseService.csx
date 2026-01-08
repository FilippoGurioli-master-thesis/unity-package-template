#load "../Utils/Log.csx"
#load "GitHubService.csx"
#load "../Models/ProjectConfig.csx"

/// <summary>
/// License Service for generating LICENSE files
/// </summary>
public static class LicenseService
{
    /// <summary>
    /// Generates a LICENSE file based on the selected license type
    /// </summary>
    /// <param name="config">The project configuration containing license details.</param>
    public static void Generate(ProjectConfig config)
    {
        Log.Info($"Generating {config.LicenseType.ToUpper()} license...");
        string outDir = Path.Combine(Environment.CurrentDirectory, config.Namespace);
        string outputPath = Path.Combine(outDir, "LICENSE");
        try
        {
            if (!Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);
            string licenseContent = GitHubService.GetLicense(config.LicenseType);
            string year = DateTime.Now.Year.ToString();
            string owner = config.Company;
            licenseContent = licenseContent
                .Replace("[year]", year)
                .Replace("[yyyy]", year)
                .Replace("[fullname]", owner)
                .Replace("[name of copyright owner]", owner);
            File.WriteAllText(outputPath, licenseContent);
            Log.Info($"Success: LICENSE created in {outDir}");
        }
        catch (Exception ex)
        {
            Log.Error($"Failed to generate license: {ex.Message}");
            File.WriteAllText(
                outputPath,
                $"Copyright (c) {DateTime.Now.Year} {config.Company}. All rights reserved."
            );
        }
    }
}

