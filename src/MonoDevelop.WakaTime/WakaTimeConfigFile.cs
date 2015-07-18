using System;
using Salaros.Config.Ini;
using System.IO;

namespace MonoDevelop.WakaTime
{
    public static class WakaTimeConfigFile
    {
        private static readonly string _configFilepath;
        private static readonly IniParser _configIni;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoDevelop.WakaTime.WakaTimeConfigFile"/> class.
        /// </summary>
        static WakaTimeConfigFile()
        {
            _configFilepath = GetConfigFilePath();
            _configIni = new IniParser(_configFilepath);
            Read();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>The API key.</value>
        internal static string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the proxy.
        /// </summary>
        /// <value>The proxy.</value>
        internal static string Proxy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MonoDevelop.WakaTime.WakaTimeConfigFile"/> is debug.
        /// </summary>
        /// <value><c>true</c> if debug; otherwise, <c>false</c>.</value>
        internal static bool Debug { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Read the settings.
        /// </summary>
        internal static void Read()
        {
            ApiKey = _configIni.GetValue("settings", "api_key", string.Empty);
            Proxy = _configIni.GetValue("settings", "proxy", string.Empty);
            Debug = _configIni.GetValue("settings", "debug", false);
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        internal static void Save()
        {
            _configIni.SetValue("settings", "api_key", ApiKey.Trim());
            _configIni.SetValue("settings", "proxy", Proxy.Trim());
            _configIni.SetValue("settings", "debug", Debug);
            _configIni.Write();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the config file path.
        /// </summary>
        /// <returns>The config file path.</returns>
        static string GetConfigFilePath()
        {
            var userHomeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userHomeDir,".wakatime.cfg");
        }

        #endregion
    }
}

