using System;
using System.Threading.Tasks;

namespace MonoDevelop.WakaTime
{
    public class WakaTimePackage : IDisposable
    {
        #region Fields

        static WakaTimeMonoDevelopPlugin _idePlugin;
        bool _disposed;

        #endregion

        #region Startup/Cleanup

        public void Initialize()
        {
            Task.Run(() =>
                {
                    InitializeAsync();
                });
        }

        static void InitializeAsync()
        {
            _idePlugin = new WakaTimeMonoDevelopPlugin(null);
        }

        ~WakaTimePackage()
        {
            Dispose(false);
        }

        #endregion

        #region Helpers

        public static void SettingsPopup()
        {
            _idePlugin.SettingsPopup();
        }

        #endregion

        #region IDisposable implementation

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_idePlugin != null)
                    _idePlugin.Dispose();
            }

            _disposed = true;
        }

        public void Dispose() // Implement IDisposable
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

