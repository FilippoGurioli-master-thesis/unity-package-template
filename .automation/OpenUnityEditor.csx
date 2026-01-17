#load "Services/UnityService.csx"
#load "Models/ProjectConfig.csx"

UnityService.OpenProject(new ProjectConfig { Namespace = GetNamespace() });

string GetNamespace() =>
    Path.GetFileName(Directory.EnumerateDirectories(".", "Sandbox.*").First()).Split(".")[1];
