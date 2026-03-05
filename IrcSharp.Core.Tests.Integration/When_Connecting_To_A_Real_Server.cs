using System.Configuration;
using Xunit;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IrcSharp.Core.Connectivity;

namespace IrcSharp.Core.Tests.Integration;

public class When_Interacting_With_A_Real_Server
{
    private static string server;
    private static int port;
    private static bool _isInitialized;

    static When_Interacting_With_A_Real_Server()
    {
        Initialize();
    }

    private static void Initialize()
    {
        if (_isInitialized) return;
        server = ConfigurationManager.AppSettings["Server"];
        port = int.Parse(ConfigurationManager.AppSettings["Port"]);
        _isInitialized = true;
    }

    [Fact(Skip = "Not working")]
    public async Task Can_Initiate_A_Connection_By_Providing_A_Hostname()
    {
        using (var con = new IrcConnection())
        {
            Assert.False(con.Connected);
            await con.ConnectAsync("Foo", "Bar", server, port);
            Assert.True(con.Connected);
        }
    }

    [Fact(Skip = "Not working")]
    public async Task Can_Initiate_A_Connection_By_Providing_An_IP()
    {
        using (var con = new IrcConnection())
        {
            Assert.False(con.Connected);
            await con.ConnectAsync("Foo", "Bar", IPAddress.Parse("127.0.0.1"), port);
            Assert.True(con.Connected);
        }
    }

    [Fact(Skip = "Not working")]
    public async Task Can_Disconnect()
    {
        using (var con = new IrcConnection())
        {
            Assert.False(con.Connected);
            await con.ConnectAsync("Foo", "Bar", server, port);
            Assert.True(con.Connected);
            await con.DisconnectAsync();
            Assert.False(con.Connected);
        }
    }

    [Fact(Skip = "Not working")]
    public async Task Can_Receive_A_Message()
    {
        using (var con = new IrcConnection())
        {
            var mre = new ManualResetEvent(false);
            string message = string.Empty;

            con.OnRawMessageReceived += (sender, args) =>
            {
                message = args.UnparsedMessage;
                mre.Set();
            };

            Assert.False(con.Connected);
            await con.ConnectAsync("Foo", "Bar", server, port);
            Assert.True(con.Connected);
            if (!mre.WaitOne(1000))
            {
                throw new Xunit.Sdk.XunitException("The OnRawMessageReceived event never fired");
            }
            Assert.Equal("NOTICE AUTH :*** Checking Ident", message);
        }
    }

    [Fact(Skip = "Not working")]
    public async Task Throws_An_Appropriate_Exception_If_The_Server_Is_Not_Available()
    {
        using (var con = new IrcConnection())
        {
            Assert.False(con.Connected);
            await Assert.ThrowsAsync<ConnectionFailedException>(() => con.ConnectAsync("Foo", "Bar", "localhostxxx", port));
            Assert.False(con.Connected);
        }
    }

    [Fact(Skip = "Not working")]
    public async Task Can_Reconnect_If_Client_Disconnects()
    {
        using (var con = new IrcConnection())
        {
            var reconnected = new ManualResetEvent(false);
            var disconnected = new ManualResetEvent(false);
            try
            {
                con.OnDisconnected += async (sender, e) =>
                {
                    var senderCon = (IrcConnection)sender;
                    while (senderCon.Connected)
                    {
                        await Task.Delay(500);
                    }
                    disconnected.Set();
                };
                con.OnRawMessageReceived += (sender, message) => reconnected.Set();
                await con.ConnectAsync("Foo", "Bar", "localhost", port);

                AssemblyInit.StopIrcServer();
                await Task.Delay(500);
                Assert.True(disconnected.WaitOne(5000));
                Assert.False(con.Connected, "Connection is still active");
            }
            finally
            {
                AssemblyInit.StartIrcServer();
                reconnected.Reset();
            }

            Assert.True(reconnected.WaitOne(5000));
            Assert.True(con.Connected, "Reconnect failed");
        }
    }
}