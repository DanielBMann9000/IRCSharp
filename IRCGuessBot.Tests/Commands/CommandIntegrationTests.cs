using IRCGuessBot.Commands;
using IRCGuessBot.Game;
using IRCGuessBot.State;
using Xunit;

namespace IRCGuessBot.Tests.Commands;

public class CommandIntegrationTests
{
    [Fact]
    public void PlayCommandStartsGame()
    {
        // Arrange
        var gameSession = new GameSession("#test");
        var winTracker = new WinTracker();
        var messages = new List<string>();
        var sendMessage = new Func<string, Task>(msg =>
        {
            messages.Add(msg);
            return Task.CompletedTask;
        });

        var gameCommands = new GameCommands(gameSession, winTracker, sendMessage);
        var commandHandler = new CommandHandler();
        var parsed = commandHandler.Parse("!play 100", "testuser");

        // Act
        gameCommands.HandlePlayCommand(parsed);

        // Assert
        Assert.True(gameSession.IsGameActive);
        Assert.Contains(messages, m => m.Contains("Game started"));
    }

    [Fact]
    public void GuessCommandReturnsFeedback()
    {
        // Arrange
        var gameSession = new GameSession("#test");
        var winTracker = new WinTracker();
        var messages = new List<string>();
        var sendMessage = new Func<string, Task>(msg =>
        {
            messages.Add(msg);
            return Task.CompletedTask;
        });

        var gameCommands = new GameCommands(gameSession, winTracker, sendMessage);

        // Start a game
        gameSession.StartGame(100);

        // Act - make a guess
        var commandHandler = new CommandHandler();
        var parsed = commandHandler.Parse("!guess 50", "testuser");
        gameCommands.HandleGuessCommand(parsed);

        // Assert - should get some feedback
        Assert.Contains(messages, m => m.Contains("testuser") && (m.Contains("too high") || m.Contains("too low")));
    }

    [Fact]
    public void WinTrackerIntegration()
    {
        // Arrange
        var winTracker = new WinTracker();

        // Act
        winTracker.RecordWin("user1");
        winTracker.RecordWin("user1");
        winTracker.RecordWin("user2");

        // Assert
        Assert.Equal(2, winTracker.GetWins("user1"));
        Assert.Equal(1, winTracker.GetWins("user2"));
    }
}