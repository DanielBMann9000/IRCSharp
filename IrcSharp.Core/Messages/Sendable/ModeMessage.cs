using System.Text;

namespace IrcSharp.Core.Messages.Sendable
{
    public class ModeMessage : ISendableMessage
    {
        public string Channel { get; private set; }

        public ModeMessage(string channel)
        {
            this.Channel = channel;
        }

        public override string ToString()
        {
            var message = new StringBuilder();
            message.AppendFormat("MODE {0}", this.Channel);
            message.Append("\r\n");
            return message.ToString();
        }
    }
}