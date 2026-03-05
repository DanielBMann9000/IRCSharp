namespace IRCGuessBot.Config;

public class BotConfig
{
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Channel { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Server))
            throw new ArgumentException("Server cannot be empty");
        
        if (Port <= 0 || Port > 65535)
            throw new ArgumentException("Port must be between 1 and 65535");
        
        if (string.IsNullOrWhiteSpace(Channel))
            throw new ArgumentException("Channel cannot be empty");
        
        if (string.IsNullOrWhiteSpace(Nickname))
            throw new ArgumentException("Nickname cannot be empty");
    }
}