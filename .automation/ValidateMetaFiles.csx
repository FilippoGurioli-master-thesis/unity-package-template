#load "Utils/Log.csx"

using System.Text.RegularExpressions;

string packageDir = "./__NAMESPACE__";

if (!Directory.Exists(packageDir))
{
    Log.Error($"❌ Package directory not found at {packageDir}");
    Environment.Exit(1);
}

Log.Info($"🔍 Validating .meta files in {packageDir}...");

bool hasErrors = false;
var guidMap = new Dictionary<string, string>();
var guidRegex = new Regex(@"guid:\s*([a-f0-9]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

static bool ShouldIgnore(string path) => path
    .Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
    .Any(part => part.EndsWith('~'));

try
{
    var allEntries = Directory.EnumerateFileSystemEntries(packageDir, "*", SearchOption.AllDirectories)
                              .Where(path => !ShouldIgnore(path))
                              .ToList();

    // 1. Check for missing .meta files
    foreach (var entry in allEntries)
    {
        if (entry.EndsWith(".meta")) continue;
        string expectedMeta = entry + ".meta";
        if (!File.Exists(expectedMeta))
        {
            Log.Error($"❌ Missing meta file: {expectedMeta}");
            hasErrors = true;
        }
    }

    // 2. Check for orphan .meta files & Duplicate GUIDs
    var metaFiles = Directory.EnumerateFiles(packageDir, "*.meta", SearchOption.AllDirectories)
                             .Where(path => !ShouldIgnore(path));

    foreach (var meta in metaFiles)
    {
        var assetPath = meta[..^5]; // Remove ".meta"
        if (!File.Exists(assetPath) && !Directory.Exists(assetPath))
        {
            Log.Error($"❌ Orphan meta file: {meta}");
            hasErrors = true;
        }
        try
        {
            var content = File.ReadAllText(meta);
            var match = guidRegex.Match(content);
            if (match.Success)
            {
                var guid = match.Groups[1].Value;
                if (guidMap.TryGetValue(guid, out var existingMeta))
                {
                    Log.Error($"❌ Duplicate GUID detected:\n   {meta}\n   {existingMeta}");
                    hasErrors = true;
                }
                else guidMap[guid] = meta;
            }
        }
        catch (Exception ex)
        {
            Log.Error($"Failed to read meta file {meta}: {ex.Message}");
            hasErrors = true;
        }
    }
}
catch (Exception ex)
{
    Log.Error($"Validation process failed: {ex.Message}");
    Environment.Exit(1);
}

// Final Result
if (hasErrors)
{
    Log.Error("❌ Meta validation failed");
    Environment.Exit(1);
}

Log.Info("✔ Meta validation passed");
Environment.Exit(0);