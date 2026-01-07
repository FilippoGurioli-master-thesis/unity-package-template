#load "Models/ProjectConfig.csx"
#load "Services/Configurator.csx"
#load "Services/TemplateService.csx"
#load "Services/LicenseService.csx"
#load "Services/EnvironmentService.csx"
#load "Services/UnityService.csx"

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