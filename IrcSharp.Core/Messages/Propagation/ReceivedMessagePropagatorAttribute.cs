using System;

namespace IrcSharp.Core.Messages.Propagation
{
    internal class ReceivedMessagePropagatorAttribute : Attribute
    {
        internal ReceivedMessagePropagatorAttribute(string commandName)
        {
            this.CommandName = commandName;
        }

        internal string CommandName
        {
            get;
            private set;
        }
    }
}
