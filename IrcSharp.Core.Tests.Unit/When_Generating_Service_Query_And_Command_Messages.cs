using System.Diagnostics.CodeAnalysis;

using IrcSharp.Core.Messages;
using IrcSharp.Core.Messages.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Unit
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToConstant.Local
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class When_Generating_Service_Query_And_Command_Messages
    {
        [TestMethod]
        public void A_Servlist_Message_With_No_Mask_Or_Type_Generates_Correctly()
        {
            var expected = "SERVLIST\r\n";
            ISendableMessage testMessage = new ServlistMessage();
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Servlist_Message_With_A_Mask_And_No_Type_Generates_Correctly()
        {
            var expected = "SERVLIST someMask\r\n";
            ISendableMessage testMessage = new ServlistMessage("someMask");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Servlist_Message_With_A_Mask_And_A_Type_Generates_Correctly()
        {
            var expected = "SERVLIST someMask someType\r\n";
            ISendableMessage testMessage = new ServlistMessage("someMask", "someType");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }

        [TestMethod]
        public void A_Squery_Message_Generates_Correctly()
        {
            var expected = "SQUERY someService :this is a service query\r\n";
            ISendableMessage testMessage = new SqueryMessage("someService", "this is a service query");
            Assert.AreEqual(expected, testMessage.ToMessage());
        }
    }
}