using System;

namespace IrcSharp.Core.Connectivity
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}