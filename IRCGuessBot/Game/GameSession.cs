namespace IRCGuessBot.Game;

public class GameSession
{
    private readonly string _channelName;
    private NumberGame? _currentGame;

    public GameSession(string channelName)
    {
        _channelName = channelName ?? throw new ArgumentNullException(nameof(channelName));
        _currentGame = null;
    }

    public string ChannelName => _channelName;
    public bool IsGameActive => _currentGame != null && !_currentGame.IsGameOver;

    public void StartGame(int maxValue)
    {
        if (IsGameActive)
            throw new InvalidOperationException("A game is already in progress");

        _currentGame = new NumberGame(maxValue);
    }

    public GuessResult ProcessGuess(int guess)
    {
        if (_currentGame == null || _currentGame.IsGameOver)
            throw new InvalidOperationException("No game is active");

        return _currentGame.CheckGuess(guess);
    }

    public string GetStatus()
    {
        if (_currentGame == null)
            return "No game in progress";

        if (_currentGame.IsGameOver)
            return $"Game over. Target was {_currentGame.GuessCount} guesses.";

        return $"Game active. {(_currentGame.GuessCount)} guesses so far.";
    }

    public void EndCurrentGame()
    {
        _currentGame = null;
    }
}