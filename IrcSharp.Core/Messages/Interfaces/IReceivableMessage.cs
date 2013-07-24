using IrcSharp.Core.Model;

namespace IrcSharp.Core.Messages.Interfaces
{
    public interface IReceivableMessage
    {
        IrcUserInfo UserInfo { get; }
    }
}