#load "Log.csx"

public static class Prompt
{
    /// <summary>
    /// Asks a question and returns the user's input, with an optional default value.
    /// </summary>
    /// <param name="question"> The question to ask the user. </param>
    /// <param name="default"> The default value to return if the user provides no input. </param>
    /// <returns>The user's input or the default value if no input is provided.</returns>
    public static string Ask(string question, string @default)
    {
        var suffix = string.IsNullOrEmpty(@default) ? "" : $" [default: {@default}]";
        Console.Write($"{question}{suffix}: ");
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
            return @default;
        return input.Trim();
    }

    /// <summary>
    /// Asks a question and ensures the user's input is not null or empty.
    /// </summary>
    /// <param name="question"> The question to ask the user. </param>
    /// <returns>The user's non-empty input.</returns>
    public static string AskNonNull(string question)
    {
        while (true)
        {
            Console.Write($"{question}: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();
            Log.Warning("Value cannot be empty. Please try again.");
        }
    }

    /// <summary>
    /// Asks a question and provides a list of options for the user to choose from.
    /// </summary>
    /// <param name="question"> The question to ask the user. </param>
    /// <param name="options"> The list of options to choose from. </param>
    /// <param name="defaultIndex"> The default option index (1-based) if the user provides no input. </param>
    /// <returns>The user's selected option.</returns>
    public static string AskInList(string question, string[] options, int defaultIndex = 1)
    {
        Log.Info(question);
        for (int i = 0; i < options.Length; i++)
            Console.WriteLine($"  {i + 1}) {options[i]}");
        var defaultVal = options[defaultIndex - 1];
        while (true)
        {
            var input = Ask($"Select an option (1-{options.Length})", defaultIndex.ToString());
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= options.Length)
                return options[choice - 1];
            Log.Warning($"Please enter a valid number between 1 and {options.Length}");
        }
    }

    /// <summary>
    /// Asks a question and returns the user's input as a masked password.
    /// </summary>
    /// <param name="question"> The question to ask the user. </param>
    /// <returns>The user's input as a password.</returns>
    public static string AskPassword(string question)
    {
        Console.Write($"{question}: ");
        string password = "";

        // C# way to mask password input
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                password = password[..^1];
            else if (!char.IsControl(key.KeyChar))
                password += key.KeyChar;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            Log.Warning("Password cannot be empty.");
            return AskPassword(question);
        }
        return password;
    }
}

