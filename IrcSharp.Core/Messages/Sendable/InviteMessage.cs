namespace IrcSharp.Core.Messages.Sendable
{
    public class InviteMessage : ISendableMessage
    {
        public string Nick { get; private set; }
        public string Channel { get; private set; }

        public InviteMessage(string nick, string channel)
        {
            this.Nick = nick;
            this.Channel = channel;
        }

        public string ToMessage()
        {
            return string.Format("INVITE {0} {1}\r\n", this.Nick, this.Channel);
        }
    }
}