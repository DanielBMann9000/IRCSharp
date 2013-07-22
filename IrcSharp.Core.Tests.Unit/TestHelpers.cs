using System;
using System.Threading;
using System.Threading.Tasks;

using IrcSharp.Core.Connectivity;
using IrcSharp.Core.Messages;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    internal static class TestHelpers
    {
        internal static async Task RunSendableEventFiringTest(
            ISendableMessage message,
            Action<IrcConnection, ManualResetEvent> registrationAction)
        {
            var mre = new ManualResetEvent(false);
            var cm = new FakeSocketConnection();
            using (var con = new IrcConnection(cm))
            {
                await con.ConnectAsync("foo", "bar", "baz", 0);
                registrationAction(con, mre);
                await con.SendMessageAsync(message);
                if (!mre.WaitOne(1000))
                {
                    Assert.Fail("The event was never received.");
                }
            }
        }
    }
}
