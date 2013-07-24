using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class ChannelModeMessage : ISendableMessage
    {
        public string Channel { get; private set; }
        public string RawCommand { get; private set; }

        public ChannelModeMessage(string channel)
        {
            this.Channel = channel;
        }

        public ChannelModeMessage(string channel, string rawCommand) : this(channel)
        {
            this.RawCommand = rawCommand;
        }

        /*This class may need to have a deeper understanding of what commands are available for ease of API users
         * E.g. a "Secret" command is +s with no target, "+b *!*@*" is a ban for all users. not important for now.
         */

        string ISendableMessage.ToMessage()
        {
            var message = new StringBuilder();
            message.AppendFormat("MODE {0}", this.Channel);
            if (!string.IsNullOrWhiteSpace(this.RawCommand))
            {
                message.AppendFormat(" {0}", this.RawCommand);
            }
            message.Append("\r\n");
            return message.ToString();
        }
    }
}