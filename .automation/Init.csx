#load "Models/ProjectConfig.csx"
#load "Services/ValidationService.csx"
#load "Services/Configurator.csx"
#load "Services/TemplateService.csx"
#load "Services/LicenseService.csx"
#load "Services/EnvironmentService.csx"
#load "Services/UnityService.csx"
#load "Services/GitHubService.csx"
#load "Services/GitService.csx"
#load "Utils/Log.csx"

if (!ValidationService.Validate())
{
    Log.Error("Validation failed.");
    return;
}

// Initialize configuration
var projectConfig = Configurator.PreparePlan();

// PHASE 1: IDENTITY
// Replace template values with actual configuration
TemplateService.Replace(projectConfig);

// Generate LICENSE
LicenseService.Generate(projectConfig);

// PHASE 2: ENVIRONMENT
// Install deps
EnvironmentService.InstallDependencies();

// Setup Unity project
UnityService.OpenProject(projectConfig, batch: true);

// Install hooks and delete template files
EnvironmentService.InstallHooks();
EnvironmentService.DeleteTemplateFiles();

// Generate .csproj for DocFX support
EnvironmentService.GenerateCsproj(projectConfig);
UnityService.OpenProject(projectConfig, batch: true);

// PHASE 3: OPEN PROJECT
UnityService.OpenProject(projectConfig);

// PHASE 4: MISC
// Update Renovate config assignee
TemplateService.ReplaceRenovateConfigAssignee(projectConfig.GitUser);

// PHASE 5: GITHUB CONFIG
// Setup GitHub Pages
GitHubService.SetPagesToWorkflow(projectConfig);

// Protect branches and tags
GitHubService.ProtectRepository(projectConfig);

// Push secrets
GitHubService.PushSecrets(projectConfig);

// PHASE 6: GIT
GitService.ConfigureSmartMerge(projectConfig);
GitService.CommitAllChanges();
GitService.CreateBaseTag();
GitService.CheckoutDevelop();

// FINISH
Log.Info("Init done. Remember to:");
Log.Info(
    $"  - configure precisely the {projectConfig.Namespace}/package.json file before starting your development."
);
Log.Info("  - download Renovate GitHub App in your account/organization to enable renovate bot");
Log.Info("  - push tags too (git push --tags)");
