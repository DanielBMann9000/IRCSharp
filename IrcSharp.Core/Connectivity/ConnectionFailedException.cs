using System;

namespace IrcSharp.Core.Connectivity
{
    public class ConnectionFailedException : Exception
    {
        public ConnectionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}