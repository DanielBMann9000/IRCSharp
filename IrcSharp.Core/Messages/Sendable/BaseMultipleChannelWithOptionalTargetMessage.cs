using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IrcSharp.Core.Messages.Sendable
{
    public abstract class BaseMultipleChannelWithOptionalTargetMessage : ISendableMessage
    {
        private readonly string messageName;
        public ReadOnlyCollection<string> Channels { get; protected set; }
        public string Target { get; protected set; }

        internal BaseMultipleChannelWithOptionalTargetMessage(string messageName)
        {
            this.messageName = messageName;
            this.Channels = new ReadOnlyCollection<string>(new string[] { });
        }

        internal BaseMultipleChannelWithOptionalTargetMessage(string messageName, string channel, string target = null)
            : this(messageName)
        {
            this.Channels = new ReadOnlyCollection<string>(new[] { channel });
            this.Target = target;
        }

        internal BaseMultipleChannelWithOptionalTargetMessage(string messageName, IList<string> channels, string target = null) 
            : this(messageName)
        {
            this.Channels = new ReadOnlyCollection<string>(channels);
            this.Target = target;
        }

        public string ToMessage()
        {
            var message = new StringBuilder();
            message.Append(this.messageName);
            if (this.Channels.Any())
            {
                message.AppendFormat(" {0}", string.Join(",", this.Channels));
                if (!string.IsNullOrWhiteSpace(this.Target))
                {
                    message.AppendFormat(" {0}", this.Target);
                }
            }
            message.Append("\r\n");

            return message.ToString();
        }
    }
}