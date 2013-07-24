﻿namespace IrcSharp.Core.Messages.Sendable
{
    public class PassMessage : ISendableMessage
    {
        public string Password { get; private set; }

        public PassMessage(string password)
        {
            this.Password = password;
        }

        public string ToMessage()
        {
            return string.Format("PASS {0}\r\n", this.Password);
        }
    }
}