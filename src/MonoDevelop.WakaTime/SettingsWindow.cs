using Gtk;
using MonoDevelop.Ide;

namespace MonoDevelop.WakaTime
{
    /// <summary>
    /// Settings window.
    /// </summary>
    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonoDevelop.WakaTime.SettingsWindow"/> class.
        /// </summary>
        public SettingsWindow()
            : base(WindowType.Toplevel)
        {
            Build();
        }

        /// <summary>
        /// Raises the button ok clicked event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void OnBtnOkClicked(object sender, System.EventArgs e)
        {
            // TODO save API key and proxy address
        }
    }
}

