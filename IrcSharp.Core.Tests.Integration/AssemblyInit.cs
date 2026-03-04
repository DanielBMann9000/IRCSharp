using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace IrcSharp.Core.Tests.Integration;
    internal class AssemblyInit
    {
        private static bool _isInitialized;

        static AssemblyInit()
        {
            StartIrcServerInternal();
        }

        private static void StartIrcServerInternal()
        {
            StopIrcServerInternal();
            var psi = new ProcessStartInfo(@".\IrcServer\bircd.exe") { CreateNoWindow = true };
            Process.Start(psi);
            _isInitialized = true;
        }

        public static void StopIrcServer()
        {
            StopIrcServerInternal();
            _isInitialized = false;
        }

        public static void StartIrcServer()
        {
            StartIrcServerInternal();
        }

        private static void StopIrcServerInternal()
        {
            var psi = new ProcessStartInfo(@".\IrcServer\bircd.exe") { CreateNoWindow = true, Arguments = "signal stop" };
            Process.Start(psi);
        }
    }