using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using IrcSharp.Core.Messages.Interfaces;

namespace IrcSharp.Core.Messages
{
    public class WhowasMessage : ISendableMessage
    {
        public ReadOnlyCollection<string> Nicknames { get; private set; }
        public int? Count { get; private set; }
        public string Target { get; private set; }

        public WhowasMessage(string nickname)
        {
            this.Nicknames = new ReadOnlyCollection<string>(new [] { nickname });
        }

        public WhowasMessage(string nickname, int count) : this(nickname)
        {
            this.Count = count;
        }

        public WhowasMessage(string nickname, int count, string target) : this(nickname, count)
        {
            this.Target = target;
        }

        public WhowasMessage(IList<string> nicknames)
        {
            this.Nicknames = new ReadOnlyCollection<string>(nicknames);
        }

        public WhowasMessage(IList<string> nicknames, int count) : this(nicknames)
        {
            this.Count = count;
        }

        public WhowasMessage(IList<string> nicknames, int count, string target) : this(nicknames, count)
        {
            this.Target = target;
        }

        public string ToMessage()
        {
            var message = new StringBuilder();
            message.Append("WHOWAS");
            message.AppendFormat(" {0}", string.Join(",", this.Nicknames));

            if (this.Count.HasValue)
            {
                message.AppendFormat(" {0}", this.Count);

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