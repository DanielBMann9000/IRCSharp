﻿namespace IrcSharp.Core.Messages.Sendable
{
    public class NickMessage : ISendableMessage
    {
        public string Nick { get; private set; }

        public NickMessage(string newNick)
        {
            this.Nick = newNick;
        }

        public override string ToString()
        {
            return string.Format("NICK {0}\r\n", this.Nick);
        }
    }
}