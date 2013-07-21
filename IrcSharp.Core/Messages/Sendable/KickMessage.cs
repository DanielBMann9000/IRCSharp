using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IrcSharp.Core.Messages.Sendable
{
    public class KickMessage : ISendableMessage
    {
        public ReadOnlyCollection<string> Channels { get; private set; }
        public ReadOnlyCollection<string> Nicks { get; private set; }
        public string Message { get; private set; }

        private KickMessage()
        {
            this.Channels = new ReadOnlyCollection<string>(new string[] {});
            this.Nicks = new ReadOnlyCollection<string>(new string[] { });
        }

        public KickMessage(string channel, string nick, string message = null) : this()
        {
            this.Channels = new ReadOnlyCollection<string>(new [] { channel });
            this.Nicks = new ReadOnlyCollection<string>(new [] { nick });
            this.Message = message;
        }

        public KickMessage(IList<string> channels, string nick, string message = null) : this()
        {
            this.Channels = new ReadOnlyCollection<string>(channels);
            this.Nicks = new ReadOnlyCollection<string>(new[] { nick });
            this.Message = message;
        }

        public KickMessage(string channel, IList<string> nicks, string message = null)
            : this()
        {
            this.Channels = new ReadOnlyCollection<string>(new[] { channel });
            this.Nicks = new ReadOnlyCollection<string>(nicks);
            this.Message = message;
        }


        public KickMessage(IList<string> channels, IList<string> nicks, string message = null)
            : this()
        {
            this.Channels = new ReadOnlyCollection<string>(channels);
            this.Nicks = new ReadOnlyCollection<string>(nicks);
            this.Message = message;
        }

        public override string ToString()
        {
            var message = new StringBuilder();
            message.Append("KICK ");
            message.AppendFormat("{0} {1}", string.Join(",", this.Channels), string.Join(",", this.Nicks));
            if (!string.IsNullOrWhiteSpace(this.Message))
            {
                message.AppendFormat(" :{0}", this.Message);
            }
            message.Append("\r\n");
            return message.ToString();
        }
    }
}

