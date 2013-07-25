using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class PartMessage : ISendableMessage, IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public ReadOnlyCollection<string> Channels { get; private set; }
        public string PartingMessage { get; private set; }

        internal PartMessage(IrcUserInfo userInfo, string channel, string partingMessage = null)
        {
            this.UserInfo = userInfo;
            this.Channels = new ReadOnlyCollection<string>(new [] {channel});
            this.PartingMessage = partingMessage;
        }


        public PartMessage(IEnumerable<string> channels, string partingMessage = null)
        {
            this.Channels = new ReadOnlyCollection<string>(channels.ToList());
            this.PartingMessage = partingMessage;
        }

        public PartMessage(string channel, string partingMessage = null)
        {
            this.Channels = new ReadOnlyCollection<string>(new List<string>{channel});
            this.PartingMessage = partingMessage;
        }

        string ISendableMessage.ToMessage()
        {
            var message = new StringBuilder();
            message.AppendFormat("PART {0}", string.Join(",", this.Channels));
            if (!string.IsNullOrWhiteSpace(this.PartingMessage))
            {
                message.AppendFormat(" :{0}", this.PartingMessage);
            }
            message.Append("\r\n");
            return message.ToString();
        }
    }
}
