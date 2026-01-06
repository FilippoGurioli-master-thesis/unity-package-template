public static class TemplateService
{
    /// <summary>
    /// Replaces a specific token in all files under the given root path
    /// </summary>
    /// <param name="rootPath"> The root directory to start the search from. </param>
    /// <param name="search"> The token to search for in the files. </param>
    /// <param name="replace"> The string to replace the token with. </param>
    /// <param name="ignorePatterns"> Patterns for files or directories to ignore. </param>
    public static void ReplaceInFiles(string rootPath, string search, string replace, string[] ignorePatterns)
    {
        var files = Directory.EnumerateFiles(rootPath, "*.*", SearchOption.AllDirectories)
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
    public static void RenameDirectories(string rootPath, string search, string replace, string[] ignorePatterns)
    {
        var dirs = Directory.GetDirectories(rootPath, $"*{search}*", SearchOption.AllDirectories)
            .Where(d => !ignorePatterns.Any(p => d.Contains(p)))
            .OrderByDescending(d => d.Length);
        foreach (var dir in dirs)
        {
            var newName = Path.Combine(Path.GetDirectoryName(dir), Path.GetFileName(dir).Replace(search, replace));
            if (dir != newName) Directory.Move(dir, newName);
        }
    }

    /// <summary>
    /// Renames files containing a specific token
    /// </summary>
    /// <param name="rootPath"> The root directory to start the search from. </param>
    /// <param name="search"> The token to search for in file names. </param>
    /// <param name="replace"> The string to replace the token with in file names. </param>
    /// <param name="ignorePatterns"> Patterns for files to ignore. </param>
    public static void RenameFiles(string rootPath, string search, string replace, string[] ignorePatterns)
    {
        var files = Directory.GetFiles(rootPath, $"*{search}*", SearchOption.AllDirectories)
            .Where(f => !ignorePatterns.Any(p => f.Contains(p)));
        foreach (var file in files)
        {
            var newName = Path.Combine(Path.GetDirectoryName(file), Path.GetFileName(file).Replace(search, replace));
            if (file != newName) File.Move(file, newName);
        }
    }
}