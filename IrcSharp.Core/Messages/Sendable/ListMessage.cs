using System.Collections.Generic;

namespace IrcSharp.Core.Messages.Sendable
{
    public class ListMessage : BaseMultipleChannelWithOptionalTargetMessage
    {
        private const string MessageName = "LIST";
        public ListMessage()
            : base(MessageName)
        {
        }

        public ListMessage(string channel, string target = null)
            : base(MessageName, channel, target)
        {
        }

        public ListMessage(IList<string> channels, string target = null)
            : base(MessageName, channels, target)
        {
        }
    }
}