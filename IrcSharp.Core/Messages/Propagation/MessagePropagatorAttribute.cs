using System;

namespace IrcSharp.Core.Messages.Propagation
{
    internal class MessagePropagatorAttribute : Attribute
    {
        internal MessagePropagatorAttribute(string commandName)
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
