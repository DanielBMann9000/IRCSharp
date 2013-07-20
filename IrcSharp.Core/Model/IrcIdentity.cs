﻿namespace IrcSharp.Core.Model
{
    public class IrcIdentity
    {
        public string Nick { get; private set; }
        public string Identity { get; private set; }
        public string Host { get; private set; }
        public string Server { get; private set; }

        public IrcIdentity(string nick, string identity, string host)
        {
            this.Nick = nick;
            this.Identity = identity;
            this.Host = host;
        }

        public IrcIdentity(string server)
        {
            this.Server = server;
        }
    }
}