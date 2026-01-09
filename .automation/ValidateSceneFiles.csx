using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

// Configuration
string samplesDir = "./__NAMESPACE__/Samples~";
string packageDir = "./Package";

if (!Directory.Exists(samplesDir))
{
    Console.WriteLine("✔ No Samples~ folder found — skipping sample validation");
    Environment.Exit(0);
}

Console.WriteLine($"🔍 Validating sample scenes in {samplesDir}...");

bool hasError = false;

// Helper: Check if GUID exists inside Package/
bool GuidExists(string guid)
{
    // Search all .meta files in the Package directory for the specific GUID string
    return Directory
        .EnumerateFiles(packageDir, "*.meta", SearchOption.AllDirectories)
        .Any(file => File.ReadLines(file).Any(line => line.Contains($"guid: {guid}")));
}

// Find all .unity scenes
var scenes = Directory.EnumerateFiles(samplesDir, "*.unity", SearchOption.AllDirectories);

foreach (var scene in scenes)
{
    Console.WriteLine($"  • Checking {Path.GetFileName(scene)}");
    string content = File.ReadAllText(scene);

    // 1. Missing scripts
    if (content.Contains("m_Script: {fileID: 0"))
    {
        Console.WriteLine($"❌ Missing script in scene: {scene}");
        hasError = true;
    }

    // 2. Missing prefabs
    if (content.Contains("m_CorrespondingSourceObject: {fileID: 0"))
    {
        Console.WriteLine($"❌ Missing prefab in scene: {scene}");
        hasError = true;
    }

    // 3. Missing materials
    if (content.Contains("m_Material: {fileID: 0"))
    {
        Console.WriteLine($"❌ Missing material in scene: {scene}");
        hasError = true;
    }

    // 4 & 5. GUID validation
    // Regex matches the standard 32-character hex GUID in Unity YAML
    var guidMatches = Regex.Matches(content, @"guid:\s+([a-f0-9]{32})");
    foreach (Match match in guidMatches)
    {
        string guid = match.Groups[1].Value;
        if (!GuidExists(guid))
        {
            Console.WriteLine($"❌ Scene references missing or external GUID: {guid}");
            Console.WriteLine($"    in scene: {scene}");
            hasError = true;
        }
    }
}

// Final result
if (hasError)
{
    Console.WriteLine("❌ Sample scene validation failed");
    Environment.Exit(1);
}

Console.WriteLine("✔ Sample scene validation passed");
Environment.Exit(0);
