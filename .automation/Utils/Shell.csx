#load "Log.csx"

public static class Shell
{
    public static string Run(string command, string arguments, bool hideOutput = false)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
        };

        using var process = new Process { StartInfo = startInfo };
        var output = new StringBuilder();
        var error = new StringBuilder();

        process.OutputDataReceived += (s, e) =>
        {
            if (e.Data != null)
            {
                output.AppendLine(e.Data);
                if (!hideOutput)
                    Log.Info(e.Data);
            }
        };

        process.ErrorDataReceived += (s, e) =>
        {
            if (e.Data != null)
                error.AppendLine(e.Data);
        };
        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();
        if (process.ExitCode != 0)
        {
            Log.Warning($"Command failed: {command} {arguments}");
            if (!string.IsNullOrEmpty(error.ToString()))
            {
                Log.Warning(error.ToString());
            }
            throw new Exception($"Process exited with code {process.ExitCode}");
        }
        return output.ToString().Trim();
    }
}
