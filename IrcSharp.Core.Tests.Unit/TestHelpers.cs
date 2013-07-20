using System.Linq;
using System.Threading.Tasks;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    internal static class TestHelpers
    {
        internal static async Task RunSendMessageTest(string expected, ISendableMessage messageToTest)
        {
            var cm = new FakeConnectionManager();
            var con = new IrcConnection(cm);
            await con.ConnectAsync(null, null, null, 0);
            await con.SendMessageAsync(messageToTest);
            Assert.IsTrue(cm.Messages.Any(m => m == expected));
        }
    }
}