using Xunit;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Interfaces;
using Moq;

namespace IrcSharp.Core.Tests.Unit;
    
    internal static class TestHelpers
    {
        internal static async Task RunSendableEventFiringTest(
            ISendableMessage message,
            Action<IrcConnection, ManualResetEvent> registrationAction)
        {
            var mre = new ManualResetEvent(false);
            var mockSocket = new Mock<ISocketConnection>();
22#HB|            mockSocket.Setup(s => s.Connected).Returns(true);
23#TB|            mockSocket.Setup(s => s.ConnectAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);
24#VW|            mockSocket.Setup(s => s.SendMessageAsync(It.IsAny<ISendableMessage>())).Returns(Task.CompletedTask);
25#ZN|            mockSocket.Setup(s => s.DisconnectAsync()).Returns(Task.CompletedTask);
            using (var con = new IrcConnection(mockSocket.Object))
            mockSocket.Setup(s => s.Connected).Returns(true);
            mockSocket.Setup(s => s.ConnectAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);
            mockSocket.Setup(s => s.SendMessageAsync(It.IsAny<ISendableMessage>())).Returns(Task.CompletedTask);
            mockSocket.Setup(s => s.DisconnectAsync()).Returns(Task.CompletedTask);
            using (var con = new IrcConnection(mockSocket.Object))
            {
                await con.ConnectAsync("foo", "bar", "baz", 0);
                registrationAction(con, mre);
                await con.SendMessageAsync(message);
                if (!mre.WaitOne(1000))
                {
                    throw new Exception("The event was never received.");
                }
            }
        }
    }