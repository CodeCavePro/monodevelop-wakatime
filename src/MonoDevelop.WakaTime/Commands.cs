using MonoDevelop.Components.Commands;

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
            using (var form = new SettingsWindow())
            {
                form.Show();
            }
        }
    }
}