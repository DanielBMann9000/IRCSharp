using System.Collections.Generic;

namespace IrcSharp.Core.Messages.Sendable
{
    public class NamesMessage : BaseMultipleChannelWithOptionalTargetMessage
    {
        private const string MessageName = "NAMES";
        public NamesMessage() : base(MessageName)
        {   
        }

        public NamesMessage(string channel, string target = null) : base(MessageName, channel, target)
        {
        }

        public NamesMessage(IList<string> channels, string target = null) : base(MessageName, channels, target)
        {
        }
    }
}