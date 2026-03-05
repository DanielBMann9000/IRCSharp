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


public class When_Parsing_Received_Messages
{
    [Fact]
    public async Task A_Parsed_Ping_Message_Has_The_Value_Property_Filled()
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
            Assert.Equal("12345678", actual.Value);
        }
    }

    [Fact]
    public async Task A_Parsed_Nick_Message_Has_The_Appropriate_Properties_Filled()
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
            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("NewNick", actual.Nick);
        }
    }

    [Fact]
    public async Task A_Parsed_Join_Message_Has_The_Appropriate_Properties_Filled()
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
            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.Channels[0]);
        }
    }

    [Fact]
    public async Task A_Parsed_Part_Message_With_A_Parting_Message_Has_The_Appropriate_Properties_Filled()
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

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.Channels[0]);
            Assert.Equal("byebye", actual.PartingMessage);
        }
    }

    [Fact]
    public async Task A_Parsed_Part_Message_With_No_Parting_Message_Has_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com PART #helloworld" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.Channels[0]);
            Assert.Null(actual.PartingMessage);
        }
    }

    [Fact]
    public async Task A_Parsed_ChannelMode_Message_Has_The_Appropriate_Properties_Filled()
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

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.Channel);
            Assert.Equal("+s", actual.RawCommand);
        }
    }

    [Fact]
    public async Task A_Parsed_Topic_Message_With_A_New_Topic_Has_The_Appropriate_Properties_Filled()
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

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.Channel);
            Assert.Equal("New Topic!", actual.Topic);
            Assert.False(actual.RemoveTopic);
        }
    }

    [Fact]
    public async Task A_Parsed_Topic_Message_For_Removing_The_Current_TopicHas_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com TOPIC #helloworld :" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.Channel);
            Assert.Null(actual.Topic);
            Assert.True(actual.RemoveTopic);
        }
    }

    [Fact]
    public async Task A_Parsed_Kick_Message_With_A_Reason_Provided_Has_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com KICK #helloworld Daniel :get out!" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.Channels[0]);
            Assert.Equal("Daniel", actual.Nicks[0]);
            Assert.Equal("get out!", actual.Message);
        }
    }

    [Fact]
    public async Task A_Parsed_Kick_Message_With_No_Reason_Provided_Has_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com KICK #helloworld Daniel" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.Channels[0]);
            Assert.Equal("Daniel", actual.Nicks[0]);
            Assert.Null(actual.Message);
        }
    }

    [Fact]
    public async Task A_Parsed_Quit_Message_With_No_Reason_Provided_Has_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com QUIT" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Null(actual.Reason);
        }
    }

    [Fact]
    public async Task A_Parsed_Quit_Message_With_A_Reason_Provided_Has_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com QUIT :bye bye!" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("bye bye!", actual.Reason);
        }
    }

    [Fact]
    public async Task A_Parsed_Squit_Message_Has_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com SQUIT whatever.com :bye bye!" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("whatever.com", actual.Server);
            Assert.Equal("bye bye!", actual.Reason);
        }
    }

    [Fact]
    public async Task A_Parsed_Invite_Message_Has_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com INVITE Daniel #helloworld" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.Channel);
            Assert.Equal("Daniel", actual.Nick);
        }
    }

    [Fact]
    public async Task A_Parsed_Notice_Message_Has_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com NOTICE #helloworld :oh god what's happening" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("#helloworld", actual.MessageDestination);
            Assert.Equal("oh god what's happening", actual.Message);
        }
    }

    [Fact]
    public async Task A_Parsed_PrivMsg_Message_Has_The_Appropriate_Properties_Filled()
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
            mockSocket.Raise(x => x.OnMessageReceived += null, new MessageEventArgs { Message = ":Test!daniel@foo.bar.com PRIVMSG Daniel :oh god what's happening" });
            if (!mre.WaitOne(1000))
            {
                throw new Exception("The event was never received.");
            }

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("Daniel", actual.MessageDestination);
            Assert.Equal("oh god what's happening", actual.Message);
        }
    }

    [Fact]
    public async Task A_Parsed_Generic_Numeric_Response_Message_Has_The_Appropriate_Properties_Filled()
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
            Assert.Equal("001", actual.ResponseCode);
            Assert.Equal("Welcome to the Internet Relay Network DBM", actual.ResponseText);
        }

    }

    [Fact]
    public async Task A_Parsed_NotRegisteredResponse_Message_Has_The_Appropriate_Properties_Filled()
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
            Assert.Equal("JOIN", actual.Command);
            Assert.Equal("Register first.", actual.Message);
        }
    }

    [Fact]
    public async Task A_Parsed_Kill_Message_Has_The_Appropriate_Properties_Filled()
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

            Assert.Equal("Test", actual.UserInfo.Nick);
            Assert.Equal("daniel", actual.UserInfo.Identity);
            Assert.Equal("foo.bar.com", actual.UserInfo.Host);
            Assert.Equal("daniel", actual.Nickname);
            Assert.Equal("byebye", actual.Comment);
        }
    }

}
