#load "Utils/Shell.csx"
#load "Utils/Log.csx"

using System;
using System.IO;

string scriptDir = Path.GetDirectoryName(Args.FirstOrDefault() ?? GetScriptPath());
string rootDir = Path.GetFullPath(Path.Combine(scriptDir, ".."));

string configPath;

if (File.Exists(Path.Combine(rootDir, ".template")))
    configPath = Path.Combine(rootDir, "docs", "docfx.json");
else
    configPath = Path.Combine(rootDir, "__NAMESPACE__", "Documentation~", "docfx.json");

Log.Info($"Building documentation using config: {configPath}");

try
{
    Shell.Run("dotnet", $"tool run docfx \"{configPath}\"");
    Log.Info("✔ Documentation build complete.");
}
catch (Exception ex)
{
    Log.Error($"Failed to build documentation: {ex.Message}");
    Environment.Exit(1);
}

string GetScriptPath([System.Runtime.CompilerServices.CallerFilePath] string path = null) => path;
