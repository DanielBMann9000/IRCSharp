using IRCGuessBot.Game;
using Xunit;

namespace IRCGuessBot.Tests.Game;

public class NumberGameTests
{
    [Fact]
    public void GameStartsWithCorrectMaxValue()
    {
        // Arrange & Act
        var game = new NumberGame(100);

        // Assert
        Assert.Equal(100, game.MaxValue);
        Assert.False(game.IsGameOver);
        Assert.Equal(0, game.GuessCount);
    }

    [Fact]
    public void ValidGuessWithinRangeReturnsCorrectFeedback()
    {
        // Arrange
        var game = new NumberGame(100);
        var target = 50; // We don't actually know the target, but we can test the behavior

        // Act & Assert - make a guess that's too high
        var result = game.CheckGuess(75);
        Assert.Equal(GuessResult.TooHigh, result);
        Assert.Equal(1, game.GuessCount);
    }

    [Fact]
    public void OutOfRangeGuessThrowsArgumentException()
    {
        // Arrange
        var game = new NumberGame(100);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => game.CheckGuess(0));
        Assert.Throws<ArgumentException>(() => game.CheckGuess(101));
    }

    [Fact]
    public void GameEndsWhenCorrectGuessMade()
    {
        // Arrange - we need to guess until we hit the target
        var game = new NumberGame(10);

        // Act - make random guesses until we win
        int attempts = 0;
        while (!game.IsGameOver && attempts < 20)
        {
            var result = game.CheckGuess(Random.Shared.Next(1, 11));
            if (result == GuessResult.Correct)
                break;
            attempts++;
        }

        // Assert
        Assert.True(game.IsGameOver);
        Assert.True(game.GuessCount > 0);
    }

    [Fact]
    public void GuessCountIsAccurate()
    {
        // Arrange
        var game = new NumberGame(100);

        // Act
        game.CheckGuess(50);
        game.CheckGuess(25);
        game.CheckGuess(75);

        // Assert
        Assert.Equal(3, game.GuessCount);
    }

    [Fact]
    public void ThrowsWhenGuessAfterGameOver()
    {
        // Arrange
        var game = new NumberGame(10);

        // Act - guess until game over
        while (!game.IsGameOver)
        {
            game.CheckGuess(Random.Shared.Next(1, 11));
        }

        // Assert
        Assert.Throws<InvalidOperationException>(() => game.CheckGuess(5));
    }
}