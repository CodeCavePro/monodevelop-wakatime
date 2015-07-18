using System.Globalization;
using MonoDevelop.Core.Logging;

namespace MonoDevelop.WakaTime.Common
{
    class Logger
    {
        // Singleton class for logging
        private static Logger _instance;
        private ILogger _log;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Logger Instance
        {
            get { return _instance ?? (_instance = new Logger()); }
        }

        /// <summary>
        /// Initialize the specified log.
        /// </summary>
        /// <param name="log">Log.</param>
        public void Initialize(ILogger log)
        {
            _log = log;
        }

        /// <summary>
        /// Logs the message at error level.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public void Error(string message)
        {
            _log.Log(LogLevel.Error, string.Format(CultureInfo.CurrentCulture, "{0}", message));

        }

        /// <summary>
        /// Logs the message at info level.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public void Info(string message)
        {
            _log.Log(LogLevel.Info, string.Format(CultureInfo.CurrentCulture, "{0}", message));
        }
    }
}



