# load "Services/Configurator.csx"
# load "Services/TemplateService.csx"

// Initialize configuration
var projectConfig = Configurator.PreparePlan();

// Replace template values with actual configuration
TemplateService.Replace(projectConfig);