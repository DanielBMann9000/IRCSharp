using System.Collections.Concurrent;

namespace IRCGuessBot.State;

public class WinTracker
{
    private readonly ConcurrentDictionary<string, int> _wins;

    public WinTracker()
    {
        _wins = new ConcurrentDictionary<string, int>();
    }

    public void RecordWin(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty", nameof(username));

        _wins.AddOrUpdate(username, 1, (key, oldValue) => oldValue + 1);
    }

    public int GetWins(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return 0;

        _wins.TryGetValue(username, out int count);
        return count;
    }

    public IEnumerable<(string Username, int Wins)> GetAllWins()
    {
        return _wins.Select(kv => (kv.Key, kv.Value));
    }
}