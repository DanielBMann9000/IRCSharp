﻿using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IrcSharp.Core.Connectivity;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Integration
{
    // ReSharper disable InconsistentNaming
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class When_Interacting_With_A_Real_Server
    {
        private static string server;
        private static int port;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            server = ConfigurationManager.AppSettings["Server"];
            port = int.Parse(ConfigurationManager.AppSettings["Port"]);
        }

        [TestMethod]
        public async Task Can_Initiate_A_Connection_By_Providing_A_Hostname()
        {
            using (var con = new IrcConnection())
            {
                Assert.AreEqual(false, con.Connected);
                await con.ConnectAsync("Foo", "Bar", server, port);
                Assert.AreEqual(true, con.Connected);
            }
        }

        [TestMethod]
        public async Task Can_Initiate_A_Connection_By_Providing_An_IP()
        {
            using (var con = new IrcConnection())
            {
                Assert.AreEqual(false, con.Connected);
                await con.ConnectAsync("Foo", "Bar", IPAddress.Parse("127.0.0.1"), port);
                Assert.AreEqual(true, con.Connected);
            }
        }

        [TestMethod]
        public async Task Can_Disconnect()
        {
            using (var con = new IrcConnection())
            {
                Assert.AreEqual(false, con.Connected);
                await con.ConnectAsync("Foo", "Bar", server, port);
                Assert.AreEqual(true, con.Connected);
                await con.DisconnectAsync();
                Assert.AreEqual(false, con.Connected);
            }
        }

        [TestMethod]
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

                Assert.AreEqual(false, con.Connected);
                await con.ConnectAsync("Foo", "Bar", server, port);
                Assert.AreEqual(true, con.Connected);
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The OnRawMessageReceived event never fired");
                }
                Assert.AreEqual("NOTICE AUTH :*** Checking Ident", message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ConnectionFailedException))]
        public async Task Throws_An_Appropriate_Exception_If_The_Server_Is_Not_Available()
        {
            using (var con = new IrcConnection())
            {
                Assert.AreEqual(false, con.Connected);
                await con.ConnectAsync("Foo", "Bar", "localhostxxx", port);
                Assert.AreEqual(false, con.Connected);
            }
        }

        [TestMethod]
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
                    Assert.IsTrue(disconnected.WaitOne(5000));
                    Assert.IsFalse(con.Connected, "Connection is still active");
                }
                finally
                {
                    AssemblyInit.StartIrcServer(null);
                    reconnected.Reset();
                }

                Assert.IsTrue(reconnected.WaitOne(5000));
                Assert.IsTrue(con.Connected, "Reconnect failed");

            }
        }
    }
}
