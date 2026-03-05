using IRCGuessBot.Commands;
using IRCGuessBot.Config;
using IRCGuessBot.Game;
using IRCGuessBot.State;
using IrcSharp.Core;
using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;
using Microsoft.Extensions.Configuration;

namespace IRCGuessBot;

class Program
{
    private static IrcConnection? _connection;
    private static GameSession? _gameSession;
    private static WinTracker? _winTracker;
    private static CommandHandler? _commandHandler;
    private static GameCommands? _gameCommands;

    static async Task Main(string[] args)
    {
        try
        {
            // Load configuration
            var config = LoadConfiguration();
            config.Validate();

            // Initialize game components
            _gameSession = new GameSession(config.Channel);
            _winTracker = new WinTracker();
            _commandHandler = new CommandHandler();
            _gameCommands = new GameCommands(_gameSession, _winTracker, SendMessageAsync);

            // Create IRC connection
            _connection = new IrcConnection();
            _connection.MessagePropagator.OnPrivMsgMessageReceived += OnPrivMsgMessageReceived;

            // Connect to server
            Console.WriteLine($"Connecting to {config.Server}:{config.Port}...");
            await _connection.ConnectAsync(config.Nickname, "IRCGuessBot", config.Server, config.Port);
            Console.WriteLine("Connected!");

            // Join channel
            Console.WriteLine($"Joining channel {config.Channel}...");
            await _connection.SendMessageAsync(new JoinMessage(config.Channel));

            Console.WriteLine("IRCGuessBot is running. Press Ctrl+C to exit.");

            // Keep running
            await Task.Delay(-1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static BotConfig LoadConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var config = new BotConfig
        {
            Server = configuration["Server"] ?? throw new ArgumentException("Server not configured"),
            Port = int.Parse(configuration["Port"] ?? "6667"),
            Channel = configuration["Channel"] ?? throw new ArgumentException("Channel not configured"),
            Nickname = configuration["Nickname"] ?? "IRCGuessBot"
        };

        return config;
    }

    private static void OnPrivMsgMessageReceived(object? sender, PrivMsgMessage e)
    {
        try
        {
            // Extract username from UserInfo
            var username = e.UserInfo?.Nick ?? "unknown";

            // Parse command
            var parsed = _commandHandler?.Parse(e.Message, username);
            if (parsed == null)
                return;

            // Handle commands
            switch (parsed.CommandName)
            {
                case "play":
                    _gameCommands?.HandlePlayCommand(parsed);
                    break;
                case "guess":
                    _gameCommands?.HandleGuessCommand(parsed);
                    break;
                default:
                    // Silently ignore unknown commands
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
    }

    private static string ExtractUsername(string ircUser)
    {
        // Extract just the nickname from user!user@host format
        var exclamationIndex = ircUser.IndexOf('!');
        if (exclamationIndex > 0)
            return ircUser.Substring(0, exclamationIndex);
        return ircUser;
    }

    private static async Task SendMessageAsync(string message)
    {
        if (_connection != null)
        {
            await _connection.SendMessageAsync(new PrivMsgMessage(_gameSession?.ChannelName ?? "#general", message));
        }
    }
}