using System;
using System.IO;
using IniParser;
using IniParser.Model;
using System.Globalization;
using MonoDevelop.WakaTime.Common;
using System.Text;

namespace MonoDevelop.WakaTime
{
    public static class WakaTimeConfigFile
    {
        private static readonly string _configFilepath;
        private static readonly FileIniDataParser _configParser;
        private static readonly IniData _configData;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoDevelop.WakaTime.WakaTimeConfigFile"/> class.
        /// </summary>
        static WakaTimeConfigFile()
        {
            _configParser = new FileIniDataParser();
            _configFilepath = GetConfigFilePath();
            _configData = (File.Exists(_configFilepath))
                ? _configParser.ReadFile(_configFilepath, Encoding.UTF8)
                : new IniData();
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
            _configData.Sections.Add(new SectionData("settings"));
            ApiKey = _configData["settings"]["api_key"] ?? string.Empty;
            Proxy = _configData["settings"]["proxy"] ?? string.Empty;
            var debugRaw = _configData["settings"]["debug"];
            Debug = (debugRaw == null || debugRaw.Equals(true.ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// Save the settings.
        /// </summary>
        internal static void Save()
        {
            _configData.Sections.Add(new SectionData("settings"));
            _configData["settings"]["api_key"] = ApiKey.Trim();
            _configData["settings"]["proxy"] = Proxy.Trim();
            _configData["settings"]["debug"] = Debug.ToString(CultureInfo.InvariantCulture);
            _configParser.WriteFile(_configFilepath, _configData, Encoding.UTF8);
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

