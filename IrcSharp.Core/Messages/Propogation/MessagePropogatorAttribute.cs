using System;

namespace IrcSharp.Core.Messages.Propogation
{
    internal class MessagePropogatorAttribute : Attribute
    {
        internal MessagePropogatorAttribute(string commandName)
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
