using System;

namespace IrcSharp.Core.Connectivity
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}