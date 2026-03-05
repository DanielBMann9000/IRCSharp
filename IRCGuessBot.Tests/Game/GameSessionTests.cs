using IRCGuessBot.Game;
using Xunit;

namespace IRCGuessBot.Tests.Game;

public class GameSessionTests
{
    [Fact]
    public void SessionStartsGameCorrectly()
    {
        // Arrange
        var session = new GameSession("#test");

        // Act
        session.StartGame(100);

        // Assert
        Assert.True(session.IsGameActive);
        Assert.Equal("#test", session.ChannelName);
    }

    [Fact]
    public void ProcessGuessDelegatesToNumberGame()
    {
        // Arrange
        var session = new GameSession("#test");
        session.StartGame(100);

        // Act
        var result = session.ProcessGuess(50);

        // Assert
        Assert.True(result == GuessResult.TooHigh || result == GuessResult.TooLow || result == GuessResult.Correct);
    }

    [Fact]
    public void GameEndsWhenCorrectGuessMade()
    {
        // Arrange
        var session = new GameSession("#test");
        session.StartGame(10);

        // Act - guess until win
        while (session.IsGameActive)
        {
            session.ProcessGuess(Random.Shared.Next(1, 11));
        }

        // Assert
        Assert.False(session.IsGameActive);
    }

    [Fact]
    public void CannotStartGameWhileOneIsActive()
    {
        // Arrange
        var session = new GameSession("#test");
        session.StartGame(100);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => session.StartGame(50));
        Assert.Equal("A game is already in progress", exception.Message);
    }

    [Fact]
    public void CannotGuessWhenNoGameActive()
    {
        // Arrange
        var session = new GameSession("#test");

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => session.ProcessGuess(50));
        Assert.Equal("No game is active", exception.Message);
    }

    [Fact]
    public void CannotGuessWhenGameAlreadyEnded()
    {
        // Arrange
        var session = new GameSession("#test");
        session.StartGame(10);

        // Act - guess until win
        while (session.IsGameActive)
        {
            session.ProcessGuess(Random.Shared.Next(1, 11));
        }

        // Assert
        var exception = Assert.Throws<InvalidOperationException>(() => session.ProcessGuess(5));
        Assert.Equal("No game is active", exception.Message);
    }
}