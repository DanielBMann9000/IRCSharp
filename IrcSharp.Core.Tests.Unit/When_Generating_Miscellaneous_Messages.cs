using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class When_Generating_Miscellaneous_Messages
    {
        [TestMethod]
        public void A_Pong_Message_Is_Successfully_Generated()
        {
            var expected = "PONG 12345678\r\n";
            ISendableMessage testMessage = new PongMessage("12345678");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Kill_Message_Is_Successfully_Generated()
        {
            var expected = "KILL Nobody :I'm a pacifist!!\r\n";
            ISendableMessage testMessage = new KillMessage("Nobody", "I'm a pacifist!!");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public async Task A_Ping_Message_Is_Automatically_Responded_To_With_An_Appropriate_Pong()
        {
            using (var cm = new FakeSocketConnection())
            using (var con = new IrcConnection(cm))
            {
                await con.ConnectAsync("foo", "bar", "baz", 0);
                var expected = "PONG 12345678\r\n";
                cm.SimulateMessageReceipt("PING :12345678");
                Assert.IsTrue(cm.Messages.Any(m => m == expected));
            }
        }
    }
}