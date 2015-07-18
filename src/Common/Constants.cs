using System.IO;
using MonoDevelop.Ide;

namespace MonoDevelop.WakaTime.Common
{
    internal static class WakaTimeConstants
    {
        internal const string CurrentWakaTimeCliVersion = "4.1.0"; // https://github.com/wakatime/wakatime/blob/master/HISTORY.rst
        internal const string CliUrl = "https://github.com/wakatime/wakatime/archive/master.zip";
        internal static readonly string PluginName = typeof(WakaTimeConstants).Assembly.GetName().Name.ToLowerInvariant();
        internal static readonly string EditorName = typeof(IdeApp).Namespace.ToLowerInvariant();
        internal static readonly string CliFolder = string.Join(Path.DirectorySeparatorChar.ToString(), new[]{ "wakatime-master", "wakatime", "cli.py"});    }
}

