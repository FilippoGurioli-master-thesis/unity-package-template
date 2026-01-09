#load "Utils/Log.csx"

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

// 1. Get version from command line arguments
if (Args.Count == 0)
{
    Log.Error(
        "No version provided. Usage: dotnet tool run dotnet-script UpdateUnityPackageVersion.csx -- 1.2.3"
    );
    Environment.Exit(1);
}

string version = Args[0];

// 2. Resolve paths
string scriptDir = Path.GetDirectoryName(GetScriptPath());
string packageJsonPath = Path.GetFullPath(
    Path.Combine(scriptDir, "..", "__NAMESPACE__", "package.json")
);

if (!File.Exists(packageJsonPath))
{
    Log.Error($"Could not find package.json at: {packageJsonPath}");
    Environment.Exit(1);
}

// 3. Read and Parse JSON
string jsonContent = File.ReadAllText(packageJsonPath);

// Using JsonNode allows us to modify the object without creating a C# class/POCO
var jsonNode = JsonNode.Parse(jsonContent);
jsonNode["version"] = version;

// 4. Write back to file with indentation (pretty-print)
var options = new JsonSerializerOptions { WriteIndented = true };
string updatedJson = jsonNode.ToJsonString(options);

// Ensure we append the newline at the end to match the Node.js script
File.WriteAllText(packageJsonPath, updatedJson + Environment.NewLine);

Log.Info($"✔ Updated Unity package.json to version {version}");

// Helper to get the current script path
string GetScriptPath([System.Runtime.CompilerServices.CallerFilePath] string path = null) => path;
