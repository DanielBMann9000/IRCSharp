using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IrcSharp.Core.Messages.Sendable
{
    public class JoinMessage : ISendableMessage
    {
        public ReadOnlyCollection<string> Channels { get; private set; }
        public ReadOnlyCollection<string> Keys { get; private set; }
        public bool LeaveAllChannels { get; private set; }

        public JoinMessage(IList<string> channels = null, IList<string> keys = null)
        {
            if (channels == null)
            {
                this.LeaveAllChannels = true;
            }
            this.Channels = channels != null ? new ReadOnlyCollection<string>(channels) : new ReadOnlyCollection<string>(new string[0]);
            this.Keys = keys != null ? new ReadOnlyCollection<string>(keys) : new ReadOnlyCollection<string>(new string[0]);
        }

        public override string ToString()
        {
            var joinMessage = new StringBuilder("JOIN ");
            if (this.LeaveAllChannels)
            {
                joinMessage.Append("0\r\n");
                return joinMessage.ToString();
            }

            joinMessage.AppendFormat("{0}", string.Join(",", this.Channels));
            if (this.Keys.Any())
            {
                joinMessage.AppendFormat(" {0}", string.Join(",", this.Keys));
            }
            joinMessage.Append("\r\n");
            return joinMessage.ToString();
        }
    }
}