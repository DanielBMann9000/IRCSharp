using System.Text.RegularExpressions;

namespace IRCGuessBot.Commands;

public record ParsedCommand(string Username, string CommandName, string? Argument);

public class CommandHandler
{
    private const string CommandPrefix = "!";

    public ParsedCommand? Parse(string message, string username)
    {
        if (string.IsNullOrWhiteSpace(message))
            return null;

        if (!message.StartsWith(CommandPrefix))
            return null;

        // Explicitly convert to string to work around .NET 10.0 ReadOnlySpan changes
        string trimmed = message.Substring(CommandPrefix.Length).Trim();
        
        var spaceIndex = trimmed.IndexOf(' ');
        string commandName;
        string? argument;
        
        if (spaceIndex < 0)
        {
            commandName = trimmed.ToLower();
            argument = null;
        }
        else
        {
            commandName = trimmed.Substring(0, spaceIndex).ToLower();
            argument = trimmed.Substring(spaceIndex + 1).Trim();
        }

        return new ParsedCommand(username, commandName, argument);
    }

    public bool IsKnownCommand(string commandName)
    {
        return commandName is "play" or "guess";
    }
}
