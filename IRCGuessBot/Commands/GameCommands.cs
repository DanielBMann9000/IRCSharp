using IRCGuessBot.Game;
using IRCGuessBot.State;
using IrcSharp.Core.Messages;

namespace IRCGuessBot.Commands;

public class GameCommands
{
    private readonly GameSession _gameSession;
    private readonly WinTracker _winTracker;
    private readonly Func<string, Task> _sendMessage;

    public GameCommands(
        GameSession gameSession,
        WinTracker winTracker,
        Func<string, Task> sendMessage)
    {
        _gameSession = gameSession;
        _winTracker = winTracker;
        _sendMessage = sendMessage;
    }

    public async Task HandlePlayCommand(ParsedCommand command)
    {
        if (!int.TryParse(command.Argument, out int maxValue) || maxValue < 1)
            return;

        try
        {
            _gameSession.StartGame(maxValue);
            await _sendMessageAsync($"Game started in {_gameSession.ChannelName}! Guess a number between 1 and {maxValue}.");
        }
        catch (InvalidOperationException)
        {
            // Game already in progress - silently ignore
        }
    }

    public async Task HandleGuessCommand(ParsedCommand command)
    {
        if (!_gameSession.IsGameActive)
            return;

        if (!int.TryParse(command.Argument, out int guess))
            return;

        try
        {
            var result = _gameSession.ProcessGuess(guess);

            switch (result)
            {
                case GuessResult.TooHigh:
                    await _sendMessageAsync($"{command.Username}, that's too high!");
                    break;
                case GuessResult.TooLow:
                    await _sendMessageAsync($"{command.Username}, that's too low!");
                    break;
                case GuessResult.Correct:
                    var totalWins = _winTracker.GetWins(command.Username) + 1;
                    _winTracker.RecordWin(command.Username);
                    await _sendMessageAsync(
                        $"\u0001ACTION {command.Username} won in 1 guess! Total wins: {totalWins}\u0001");
                    _gameSession.EndCurrentGame();
                    break;
            }
        }
        catch (InvalidOperationException)
        {
            // No game active - silently ignore
        }
    }

    private async Task _sendMessageAsync(string message)
    {
        await _sendMessage(message);
    }
}