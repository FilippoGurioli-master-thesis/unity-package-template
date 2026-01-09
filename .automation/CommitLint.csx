#load "Utils/Log.csx"

using System.Text.RegularExpressions;

private var pattern =
    @"^(?=.{1,90}$)(?:build|feat|ci|chore|docs|fix|perf|refactor|revert|style|test)(?:\(.+\))*!?(?::).{4,}(?:#\d+)*(?<![\.\s])$";
private var msg = File.ReadAllLines(Args[0])[0];

if (Regex.IsMatch(msg, pattern))
    return 0;

Log.Error("Invalid commit message");
Log.Error("e.g: 'feat(scope): subject' or 'fix: subject'");
Log.Error("more info: https://www.conventionalcommits.org/en/v1.0.0/");

return 1;
