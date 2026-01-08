public static class Log
{
    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="msg">The message to log</param>
    public static void Info(string msg) => LogWithLevel(LogLevel.Info, msg);

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="msg">The message to log</param>
    public static void Warning(string msg) => LogWithLevel(LogLevel.Warning, msg);

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="msg">The message to log</param>
    public static void Error(string msg) => LogWithLevel(LogLevel.Error, msg);

    private static void LogWithLevel(LogLevel level, string msg)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.Write("[");
        switch (level)
        {
            case LogLevel.Info:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("INFO");
                break;
            case LogLevel.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("WARN");
                break;
            case LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ERROR");
                break;
        }
        Console.ForegroundColor = originalColor;
        Console.WriteLine($"] {msg}");
    }

    private enum LogLevel
    {
        Info,
        Warning,
        Error,
    }
}

