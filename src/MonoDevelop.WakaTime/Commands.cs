using MonoDevelop.Components.Commands;
using Gtk;

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
            Application.Init();
            using (var form = new SettingsWindow())
            {
                form.Show();
            }
            Application.Run();
        }
    }
}