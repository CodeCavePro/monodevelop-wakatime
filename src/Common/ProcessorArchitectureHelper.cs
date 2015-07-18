using System;
using System.Diagnostics;

namespace MonoDevelop.WakaTime.Common
{
    static class ProcessorArchitectureHelper
    {
        static readonly bool Is64BitProcess = (IntPtr.Size == 8);
        internal static bool Is64BitOperatingSystem = Is64BitProcess || InternalCheckIsWow64();

        public static bool InternalCheckIsWow64()
        {
            if (PlatformID.Win32NT != Environment.OSVersion.Platform)
                return  (Environment.Is64BitOperatingSystem && Environment.Is64BitProcess);

            if ((Environment.OSVersion.Version.Major != 5 || Environment.OSVersion.Version.Minor < 1) &&
                Environment.OSVersion.Version.Major < 6) return false;

            using (var p = Process.GetCurrentProcess())
            {
                bool retVal;
                return NativeMethods.IsWow64Process(p.Handle, out retVal) && retVal;
            }
        }
    }
}

