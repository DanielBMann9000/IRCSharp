﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using IrcSharp.Core.Messages.Interfaces;
using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages
{
    public class JoinMessage : ISendableMessage, IReceivableMessage
    {
        public IrcUserInfo UserInfo { get; private set; }
        public ReadOnlyCollection<string> Channels { get; private set; }
        public ReadOnlyCollection<string> Keys { get; private set; }
        public bool LeaveAllChannels { get; private set; }

        internal JoinMessage(IrcUserInfo userInfo, string channel)
        {
            this.UserInfo = userInfo;
            this.Channels = new ReadOnlyCollection<string>(new [] { channel });
        }

        public JoinMessage(IList<string> channels = null, IList<string> keys = null)
        {
            if (channels == null)
            {
                this.LeaveAllChannels = true;
            }
            this.Channels = channels != null ? new ReadOnlyCollection<string>(channels) : new ReadOnlyCollection<string>(new string[0]);
            this.Keys = keys != null ? new ReadOnlyCollection<string>(keys) : new ReadOnlyCollection<string>(new string[0]);
        }

        public JoinMessage(string channel, string key = null)
        {
            if (string.IsNullOrWhiteSpace(channel))
            {
                this.LeaveAllChannels = true;
            }
            this.Channels = channel != null ? new ReadOnlyCollection<string>(new List<string>{channel}) : new ReadOnlyCollection<string>(new string[0]);
            this.Keys = key != null ? new ReadOnlyCollection<string>(new List<string>{key}) : new ReadOnlyCollection<string>(new string[0]);
        }

        string ISendableMessage.ToMessage()
        {
            var joinMessage = new StringBuilder("JOIN ");
            if (this.LeaveAllChannels)
            {
                joinMessage.Append("0\r\n");
                return joinMessage.ToString();
            }

            joinMessage.AppendFormat("{0}", string.Join(",", this.Channels));
            if (this.Keys.Any())
            {
                joinMessage.AppendFormat(" {0}", string.Join(",", this.Keys));
            }
            joinMessage.Append("\r\n");
            return joinMessage.ToString();
        }
    }
}