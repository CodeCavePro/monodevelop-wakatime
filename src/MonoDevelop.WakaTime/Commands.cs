using MonoDevelop.Components.Commands;
using MonoDevelop.WakaTime.Common;

namespace MonoDevelop.WakaTime
{
    /// <summary>
    /// Addin commands.
    /// </summary>
    public enum Commands
    {
        ShowSettingsWindow
    }

    /// <summary>
    /// ShowSettingsWindow command handler.
    /// </summary>
    class ShowSettingsWindowHandler : CommandHandler
    {
        protected override void Run ()
        {
            WakaTimePackage.MenuItemCallback();
        }
    }

    /// <summary>
    /// WakaTime start-up handler.
    /// </summary>
    public class ShowSettingsOnStartUpHandler : CommandHandler
    {
        protected override void Run()
        {
            var package = new WakaTimePackage();
            package.Initialize();
        }
    }
}