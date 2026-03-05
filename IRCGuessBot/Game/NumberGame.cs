namespace IRCGuessBot.Game;

public class NumberGame
{
    private readonly int _maxValue;
    private readonly int _targetNumber;
    private int _guessCount;
    private bool _gameOver;

    public NumberGame(int maxValue)
    {
        if (maxValue < 1)
            throw new ArgumentException("Max value must be at least 1", nameof(maxValue));

        _maxValue = maxValue;
        _targetNumber = Random.Shared.Next(1, maxValue + 1);
        _guessCount = 0;
        _gameOver = false;
    }

    public int MaxValue => _maxValue;
    public int GuessCount => _guessCount;
    public bool IsGameOver => _gameOver;

    public GuessResult CheckGuess(int guess)
    {
        if (_gameOver)
            throw new InvalidOperationException("Game has already ended");

        if (guess < 1 || guess > _maxValue)
            throw new ArgumentException($"Guess must be between 1 and {_maxValue}", nameof(guess));

        _guessCount++;

        if (guess == _targetNumber)
        {
            _gameOver = true;
            return GuessResult.Correct;
        }
        else if (guess < _targetNumber)
        {
            return GuessResult.TooLow;
        }
        else
        {
            return GuessResult.TooHigh;
        }
    }
}

public enum GuessResult
{
    TooHigh,
    TooLow,
    Correct
}