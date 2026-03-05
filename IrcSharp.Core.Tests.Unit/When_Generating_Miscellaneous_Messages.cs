using Xunit;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Interfaces;


namespace IrcSharp.Core.Tests.Unit;
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToConstant.Local


public class When_Generating_Miscellaneous_Messages
{
    [Fact]
    public void A_Pong_Message_Is_Successfully_Generated()
    {
        var expected = "PONG 12345678\r\n";
        ISendableMessage testMessage = new PongMessage("12345678");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public void A_Kill_Message_Is_Successfully_Generated()
    {
        var expected = "KILL Nobody :I'm a pacifist!!\r\n";
        ISendableMessage testMessage = new KillMessage("Nobody", "I'm a pacifist!!");
        Assert.Equal(expected, testMessage.ToMessage());
    }

    [Fact]
    public async Task A_Ping_Message_Is_Automatically_Responded_To_With_An_Appropriate_Pong()
    {
        var mockSocket = new Mock<ISocketConnection>();

        var sentMessages = new List<string>();
        mockSocket.Setup(x => x.SendMessageAsync(It.IsAny<ISendableMessage>()))
                  .Returns(Task.CompletedTask)
                  .Callback((ISendableMessage m) => sentMessages.Add(m.ToMessage()));

        mockSocket.Setup(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>()))
                  .Returns(Task.CompletedTask)
                  .Callback(() =>
                  {
                      mockSocket.Raise(x => x.OnMessageReceived += null,
                          new MessageEventArgs { Message = ":localhost.com 001 DBM :Welcome to the Internet Relay Network DBM" });
                  });

        mockSocket.Setup(x => x.DisconnectAsync())
                  .Returns(Task.CompletedTask);

        using (var con = new IrcConnection(mockSocket.Object))
        {
            await con.ConnectAsync("foo", "bar", "baz", 0);
            var expected = "PONG 12345678\r\n";
            mockSocket.Raise(x => x.OnMessageReceived += null,
                             new MessageEventArgs { Message = "PING :12345678" });
            Assert.Contains(expected, sentMessages);
        }
    }
}
