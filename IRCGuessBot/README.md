# IRCGuessBot

A console IRC bot that plays a "What number am I thinking of?" guessing game with users.

## Features

- Start games with `!play <max>` (e.g., `!play 100` for numbers 1-100)
- Guess numbers with `!guess <number>`
- Bot responds with "too high" or "too low" feedback
- Win announcement includes guess count and user's total wins
- In-memory win tracking per user

## Build Requirements

- .NET 10.0 SDK

## Configuration

Create an `appsettings.json` file in the project directory:

```json
{
  "Server": "irc.example.com",
  "Port": 6667,
  "Channel": "#general",
  "Nickname": "IRCGuessBot"
}
```

## Usage

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run
```

### Commands

- `!play <max>` - Start a new game with numbers from 1 to max
- `!guess <number>` - Guess a number in the current game

### Example Session

```
User: !play 100
IRCGuessBot: Game started in #general! Guess a number between 1 and 100.
User: !guess 50
IRCGuessBot: testuser, that's too high!
User: !guess 25
IRCGuessBot: testuser, that's too low!
User: !guess 30
IRCGuessBot: ACTION testuser won in 1 guesses! Total wins: 1
```

## Error Handling

- Invalid commands are silently ignored
- No feedback is provided for invalid input
- Bot handles connection errors gracefully

## Project Structure

```
IRCGuessBot/
├── Config/
│   └── BotConfig.cs          # Configuration model
├── Commands/
│   ├── CommandHandler.cs     # Command parsing
│   └── GameCommands.cs       # !play and !guess handlers
├── Game/
│   ├── NumberGame.cs         # Core game logic
│   └── GameSession.cs        # Per-channel game state
├── State/
│   └── WinTracker.cs         # In-memory win tracking
├── Program.cs                # Main entry point
└── appsettings.json          # Configuration file
```

## Testing

```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "FullyQualifiedName~NumberGameTests"
```

## License

MIT License