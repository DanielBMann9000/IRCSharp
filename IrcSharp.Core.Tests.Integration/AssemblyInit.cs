using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IrcSharp.Core.Tests.Integration
{
    [TestClass]
    internal class AssemblyInit
    {
        [AssemblyInitialize]
        public static void StartIrcServer(TestContext context)
        {
            StopIrcServer();
            var psi = new ProcessStartInfo(@".\IrcServer\bircd.exe") { CreateNoWindow = true };
            Process.Start(psi);
        }

        [AssemblyCleanup]
        public static void StopIrcServer()
        {
            var psi = new ProcessStartInfo(@".\IrcServer\bircd.exe") { CreateNoWindow = true, Arguments = "signal stop" };
            Process.Start(psi);
        }
    }
}
