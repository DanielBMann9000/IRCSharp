using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
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

        string ISendableMessage.ToMessage()
        {
            return string.Format("INVITE {0} {1}\r\n", this.Nick, this.Channel);
        }
    }
}