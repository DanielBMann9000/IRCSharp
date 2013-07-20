using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IrcSharp.Core.Connectivity;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Integration
{
    [TestClass]
    public class When_Interacting_With_A_Real_Server
    {
        [TestMethod]
        public async Task Can_Initiate_A_Connection_By_Providing_A_Hostname()
        {
            using (var cm = new ConnectionManager())
            {
                Assert.AreEqual(false, cm.Connected);
                await cm.ConnectAsync("localhost", 5454);
                Assert.AreEqual(true, cm.Connected);
            }
        }

        [TestMethod]
        public async Task Can_Initiate_A_Connection_By_Providing_An_IP()
        {
            using (var cm = new ConnectionManager())
            {
                Assert.AreEqual(false, cm.Connected);
                await cm.ConnectAsync(IPAddress.Parse("127.0.0.1"), 5454);
                Assert.AreEqual(true, cm.Connected);
            }
        }

        [TestMethod]
        public async Task Can_Disconnect()
        {
            using (var cm = new ConnectionManager())
            {
                Assert.AreEqual(false, cm.Connected);
                await cm.ConnectAsync("localhost", 5454);
                Assert.AreEqual(true, cm.Connected);
                await cm.DisconnectAsync();
                Assert.AreEqual(false, cm.Connected);
            }
        }

        [TestMethod]
        public async Task Can_Receive_A_Message()
        {
            using (var cm = new ConnectionManager())
            {
                var mre = new ManualResetEvent(false);
                string message = string.Empty;

                cm.OnMessageReceived += (sender, args) =>
                {
                    message = args.Message;
                    mre.Set();
                };

                Assert.AreEqual(false, cm.Connected);
                await cm.ConnectAsync("localhost", 5454);
                Assert.AreEqual(true, cm.Connected);
                mre.WaitOne(1000);
                Assert.AreEqual("NOTICE AUTH :*** Checking Ident", message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ConnectionFailedException))]
        public async Task Throws_An_Appropriate_Exception_If_The_Server_Is_Not_Available()
        {
            using (var cm = new ConnectionManager())
            {
                Assert.AreEqual(false, cm.Connected);
                await cm.ConnectAsync("localhostxxx", 12343);
                Assert.AreEqual(false, cm.Connected);
            }
        }    
    }
}
