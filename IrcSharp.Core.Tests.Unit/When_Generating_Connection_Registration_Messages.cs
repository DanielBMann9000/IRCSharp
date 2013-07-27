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
    public class When_Generating_Connection_Registration_Messages
    {
        [TestMethod]
        public void A_Nick_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "NICK TestUser\r\n";
            ISendableMessage testMessage = new NickMessage("TestUser");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Pass_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "PASS foobar\r\n";
            ISendableMessage testMessage = new PassMessage("foobar");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_User_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "USER UserName 3 * :Test User\r\n";
            ISendableMessage testMessage = new UserMessage("UserName", UserMessage.Mode.Invisible | UserMessage.Mode.Wallops, "Test User");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void An_Oper_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "OPER User Pass\r\n";
            ISendableMessage testMessage = new OperMessage("User", "Pass");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void An_UserMode_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "MODE DBM +o\r\n";
            ISendableMessage testMessage = new UserModeMessage("DBM", "+o");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void An_Service_Message_Is_Successfully_Generated_And_Sent()
        {
            var expected = "SERVICE foo * *.us 0 0 :this is info\r\n";
            ISendableMessage testMessage = new ServiceMessage("foo", "*.us", "this is info");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Quit_Message_With_No_Reason_Is_Successfully_Generated()
        {
            var expected = "QUIT\r\n";
            ISendableMessage testMessage = new QuitMessage();
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Quit_Message_With_A_Reason_Is_Successfully_Generated()
        {
            var expected = "QUIT :Goodbye, cruel world!\r\n";
            ISendableMessage testMessage = new QuitMessage("Goodbye, cruel world!");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_SQuit_Message_Is_Successfully_Generated()
        {
            var expected = "SQUIT foo.bar.com :Goodbye, cruel world!\r\n";
            ISendableMessage testMessage = new SquitMessage("foo.bar.com", "Goodbye, cruel world!");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }
    }
}