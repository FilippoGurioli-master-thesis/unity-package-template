#load "../Models/ProjectConfig.csx"
#load "../Utils/Log.csx"

using System.Text.RegularExpressions;

/// <summary>
/// Provides services for template manipulation such as replacing tokens in files and renaming files or directories.
/// </summary>
public static class TemplateService
{
    /// <summary>
    /// Replaces template tokens in files and renames files/directories based on the provided project configuration.
    /// </summary>
    /// <param name="config">The project configuration containing replacement values.</param>
    public static void Replace(ProjectConfig config)
    {
        var ignorePatterns = new string[]
        {
            ".git",
            ".automation/Models",
            ".automation/Services",
            ".automation/Utils",
            ".automation/Init.csx",
            "README.md",
            "CHANGELOG.md",
            "docs",
            $"Sandbox.{config.Namespace}/Library",
            $"Sandbox.{config.Namespace}/Temp",
            $"Sandbox.{config.Namespace}/obj",
            $"Sandbox.{config.Namespace}/Logs",
        };
        var tokenMap = new Dictionary<string, string>
        {
            { "__GIT_USER__", config.GitUser },
            { "__GIT_MAIL__", config.GitMail },
            { "__DOMAIN__", config.Domain },
            { "__COMPANY__", config.Company },
            { "__PACKAGE__", config.Package },
            { "__NAMESPACE__", config.Namespace },
            { "__NAME__", config.DisplayName },
            { "__DESCRIPTION__", config.Description },
        };
        var allFiles = Directory.EnumerateFiles(".", "*", SearchOption.AllDirectories);
        var filteredFiles = allFiles.Where(file =>
        {
            var normalizedPath = file.Replace("\\", "/");
            return !ignorePatterns.Any(pattern =>
            {
                var p = pattern.Replace("\\", "/");
                if (normalizedPath.EndsWith("/" + p) || normalizedPath == p)
                    return true;
                if (normalizedPath.Contains("/" + p + "/") || normalizedPath.StartsWith(p + "/"))
                    return true;
                return false;
            });
        });
        foreach (var file in filteredFiles)
        {
            string content = File.ReadAllText(file);
            bool modified = false;
            foreach (var token in tokenMap)
            {
                if (content.Contains(token.Key))
                {
                    content = content.Replace(token.Key, token.Value);
                    modified = true;
                }
            }
            if (modified)
                File.WriteAllText(file, content);
        }
        foreach (var token in tokenMap)
            RenameFiles(".", token.Key, token.Value, ignorePatterns);
        foreach (var token in tokenMap)
            RenameDirectories(".", token.Key, token.Value, ignorePatterns);
    }

    public static void ReplaceRenovateConfigAssignee(string assignee)
    {
        string file = "renovate.json";
        if (File.Exists(file))
        {
            string content = File.ReadAllText(file);
            if (content.Contains("\"assignees\":"))
            {
                var newContent = Regex.Replace(
                    content,
                    "\"assignees\":\\s*\\[.*?\\]",
                    $"\"assignees\": [ \"{assignee}\" ]",
                    RegexOptions.Singleline
                );
                File.WriteAllText(file, newContent);
            }
        }
        else
        {
            Log.Warning($"File not found, skipping: {file}");
        }
    }

    /// <summary>
    /// Replaces a specific token in all files under the given root path
    /// </summary>
    /// <param name="rootPath"> The root directory to start the search from. </param>
    /// <param name="search"> The token to search for in the files. </param>
    /// <param name="replace"> The string to replace the token with. </param>
    /// <param name="ignorePatterns"> Patterns for files or directories to ignore. </param>
    public static void ReplaceInFiles(
        string rootPath,
        string search,
        string replace,
        string[] ignorePatterns
    )
    {
        var files = Directory
            .EnumerateFiles(rootPath, "*.*", SearchOption.AllDirectories)
            .Where(f => !ignorePatterns.Any(p => f.Contains(p)));
        foreach (var file in files)
        {
            string content = File.ReadAllText(file);
            if (content.Contains(search))
            {
                File.WriteAllText(file, content.Replace(search, replace));
            }
        }
    }

    /// <summary>
    /// Renames directories containing a specific token
    /// </summary>
    /// <param name="rootPath"> The root directory to start the search from. </param>
    /// <param name="search"> The token to search for in directory names. </param>
    /// <param name="replace"> The string to replace the token with in directory names. </param>
    /// <param name="ignorePatterns"> Patterns for directories to ignore. </param>
    public static void RenameDirectories(
        string rootPath,
        string search,
        string replace,
        string[] ignorePatterns
    )
    {
        var dirs = Directory
            .GetDirectories(rootPath, $"*{search}*", SearchOption.AllDirectories)
            .Where(d => !ignorePatterns.Any(p => d.Contains(p)))
            .OrderByDescending(d => d.Length);
        foreach (var dir in dirs)
        {
            var newName = Path.Combine(
                Path.GetDirectoryName(dir),
                Path.GetFileName(dir).Replace(search, replace)
            );
            if (dir != newName)
                Directory.Move(dir, newName);
        }
    }

    /// <summary>
    /// Renames files containing a specific token
    /// </summary>
    /// <param name="rootPath"> The root directory to start the search from. </param>
    /// <param name="search"> The token to search for in file names. </param>
    /// <param name="replace"> The string to replace the token with in file names. </param>
    /// <param name="ignorePatterns"> Patterns for files to ignore. </param>
    public static void RenameFiles(
        string rootPath,
        string search,
        string replace,
        string[] ignorePatterns
    )
    {
        var files = Directory
            .GetFiles(rootPath, $"*{search}*", SearchOption.AllDirectories)
            .Where(f => !ignorePatterns.Any(p => f.Contains(p)));
        foreach (var file in files)
        {
            var newName = Path.Combine(
                Path.GetDirectoryName(file),
                Path.GetFileName(file).Replace(search, replace)
            );
            if (file != newName)
                File.Move(file, newName);
        }
    }
}
