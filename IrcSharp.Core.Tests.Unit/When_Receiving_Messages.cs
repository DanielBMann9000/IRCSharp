using System;
using Xunit;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;


namespace IrcSharp.Core.Tests.Unit;
// ReSharper disable InconsistentNaming


public class When_Receiving_Messages
{
    [Fact]
    public async Task A_Message_That_Falls_Into_No_Other_Categories_Fires_An_Unknown_Message_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            await con.ConnectAsync("foo", "bar", "baz", 0);
            UnknownMessage actual = null;
            con.MessagePropagator.OnUnknownMessage += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = "garbage" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
            Assert.NotNull(actual);
            Assert.Equal("garbage", actual.UnparsedMessage);
        }
    }

    [Fact]
    public async Task A_Ping_Message_Fires_A_Ping_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            PingMessage actual = null;
            con.MessagePropagator.OnPingMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = "PING :12345678" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Nick_Message_Fires_A_Nick_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            NickMessage actual = null;
            con.MessagePropagator.OnNickMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com NICK NewNick" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Join_Message_Fires_A_Join_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            JoinMessage actual = null;
            con.MessagePropagator.OnJoinMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com JOIN #helloworld" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Part_Message_Fires_A_Part_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            PartMessage actual = null;
            con.MessagePropagator.OnPartMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com PART #helloworld :byebye" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_ChannelMode_Message_Fires_A_ChannelMode_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            ChannelModeMessage actual = null;
            con.MessagePropagator.OnChannelModeMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com MODE #helloworld +s" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Topic_Message_Fires_A_Topic_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            TopicMessage actual = null;
            con.MessagePropagator.OnTopicMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com TOPIC #helloworld :New Topic!" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task An_Invite_Message_Fires_An_Invite_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            InviteMessage actual = null;
            con.MessagePropagator.OnInviteMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com INVITE #helloworld Daniel" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Kick_Message_Fires_A_Kick_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            KickMessage actual = null;
            con.MessagePropagator.OnKickMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com KICK #helloworld Daniel :Get out!" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }


    [Fact]
    public async Task A_Notice_Message_Fires_A_Notice_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            NoticeMessage actual = null;
            con.MessagePropagator.OnNoticeMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com NOTICE #helloworld :Hi there!" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }


    [Fact]
    public async Task A_PrivMsg_Message_Fires_A_PrivMsg_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            PrivMsgMessage actual = null;
            con.MessagePropagator.OnPrivMsgMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com PRIVMSG Daniel :What's up?" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Quit_Message_Fires_A_Quit_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            QuitMessage actual = null;
            con.MessagePropagator.OnQuitMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com QUIT :byebye" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Squit_Message_Fires_A_Squit_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            SquitMessage actual = null;
            con.MessagePropagator.OnSquitMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com SQUIT someserver.com :byebye" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Numeric_Response_Message_With_A_Code_Of_001_Fires_A_GenericNumericResponse_Message()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            GenericNumericResponseMessage actual = null;
            con.MessagePropagator.OnWelcomeResponseMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":localhost.com 001 DBM :Welcome to the Internet Relay Network DBM" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Numeric_Response_Message_With_A_Code_Of_451_Fires_A_NotRegisteredResponse_Message()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            NotRegisteredNumericResponseMessage actual = null;
            con.MessagePropagator.OnNotRegisteredResponseMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":localhost.com 451 DBM JOIN :Register first." });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }

    [Fact]
    public async Task A_Kill_Message_Fires_A_Kill_Event()
    {
        var mre = new ManualResetEvent(false);
        var mockSocket = new Mock<ISocketConnection>();
        using (var con = new IrcConnection(mockSocket.Object))
        {
            KillMessage actual = null;
            con.MessagePropagator.OnKillMessageReceived += (sender, args) =>
            {
                actual = args;
                mre.Set();
            };

            await con.ConnectAsync("foo", "bar", "baz", 0);
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com KILL daniel :byebye" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }
        }
    }
}
