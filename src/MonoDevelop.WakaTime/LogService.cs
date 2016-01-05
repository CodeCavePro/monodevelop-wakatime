using WakaTime;
using MonoDevelop.Core.Logging;
using LogLevel = MonoDevelop.Core.Logging.LogLevel;

namespace MonoDevelop.WakaTime
{
    public class LogService : ILogService
    {
        readonly ILogger _logger;

        public LogService()
        {
            _logger = new ConsoleLogger();
        }

        #region ILogService implementation

        public void Log(string message)
        {
            _logger.Log(LogLevel.Warn, message);
        }

        #endregion
    }
}

