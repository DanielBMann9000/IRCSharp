using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class WhoisMessage : ISendableMessage 
    {
        public ReadOnlyCollection<string> Masks { get; private set; }
        public string Target { get; private set; }

        public WhoisMessage(string mask)
        {
            this.Masks = new ReadOnlyCollection<string>(new [] { mask });
        }

        public WhoisMessage(string mask, string target) : this(mask)
        {
            this.Target = target;
        }

        public WhoisMessage(IList<string> masks)
        {
            this.Masks = new ReadOnlyCollection<string>(masks);
        }

        public WhoisMessage(IList<string> masks, string target) : this(masks)
        {
            this.Target = target;
        }

        public string ToMessage()
        {
            var message = new StringBuilder();
            message.Append("WHOIS");
            if (!string.IsNullOrWhiteSpace(this.Target))
            {
                message.AppendFormat(" {0}", this.Target);
            }

            message.AppendFormat(" {0}", string.Join(",", this.Masks));

            message.Append("\r\n");
            return message.ToString();
        }
    }
}