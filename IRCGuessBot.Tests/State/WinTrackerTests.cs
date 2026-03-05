using IRCGuessBot.State;
using Xunit;

namespace IRCGuessBot.Tests.State;

public class WinTrackerTests
{
    [Fact]
    public void RecordWinIncrementsCount()
    {
        // Arrange
        var tracker = new WinTracker();

        // Act
        tracker.RecordWin("user1");
        tracker.RecordWin("user1");

        // Assert
        Assert.Equal(2, tracker.GetWins("user1"));
    }

    [Fact]
    public void GetWinsReturnsCorrectCount()
    {
        // Arrange
        var tracker = new WinTracker();
        tracker.RecordWin("user1");
        tracker.RecordWin("user1");
        tracker.RecordWin("user1");

        // Act & Assert
        Assert.Equal(3, tracker.GetWins("user1"));
    }

    [Fact]
    public void MultipleUsersTrackedIndependently()
    {
        // Arrange
        var tracker = new WinTracker();

        // Act
        tracker.RecordWin("user1");
        tracker.RecordWin("user1");
        tracker.RecordWin("user2");
        tracker.RecordWin("user2");
        tracker.RecordWin("user2");
        tracker.RecordWin("user3");

        // Assert
        Assert.Equal(2, tracker.GetWins("user1"));
        Assert.Equal(3, tracker.GetWins("user2"));
        Assert.Equal(1, tracker.GetWins("user3"));
    }

    [Fact]
    public void GetAllWinsReturnsAllUsers()
    {
        // Arrange
        var tracker = new WinTracker();
        tracker.RecordWin("user1");
        tracker.RecordWin("user2");
        tracker.RecordWin("user3");

        // Act
        var allWins = tracker.GetAllWins().ToList();

        // Assert
        Assert.Equal(3, allWins.Count);
        Assert.Contains(allWins, w => w.Username == "user1" && w.Wins == 1);
        Assert.Contains(allWins, w => w.Username == "user2" && w.Wins == 1);
        Assert.Contains(allWins, w => w.Username == "user3" && w.Wins == 1);
    }

    [Fact]
    public void GetWinsReturnsZeroForUnknownUser()
    {
        // Arrange
        var tracker = new WinTracker();

        // Act & Assert
        Assert.Equal(0, tracker.GetWins("unknown"));
    }

    [Fact]
    public void GetWinsReturnsZeroForEmptyUsername()
    {
        // Arrange
        var tracker = new WinTracker();

        // Act & Assert
        Assert.Equal(0, tracker.GetWins(""));
        Assert.Equal(0, tracker.GetWins(null!));
    }
}