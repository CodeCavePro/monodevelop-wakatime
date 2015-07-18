using Gtk;
using System;

namespace MonoDevelop.WakaTime
{
    /// <summary>
    /// Settings window.
    /// </summary>
    public sealed partial class SettingsWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonoDevelop.WakaTime.SettingsWindow"/> class.
        /// </summary>
        public SettingsWindow()
            : base(WindowType.Toplevel)
        {
            Shown += OnShown;
            Build();
        }

        /// <summary>
        /// Raises the button ok clicked event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnBtnOkClicked(object sender, System.EventArgs e)
        {
            try
            {
                Guid apiKey;
                var parse = Guid.TryParse(txtAPIKey.Text.Trim(), out apiKey);                              
                if (parse)
                {
                    WakaTimeConfigFile.ApiKey = apiKey.ToString();
                    WakaTimeConfigFile.Proxy = txtProxy.Text.Trim();
                    WakaTimeConfigFile.Debug = chkDebugMode.Active;
                    WakaTimeConfigFile.Save();
                }
                else
                {
                    MessageBox.Show(@"Please enter valid Api Key.");
                    return; // do not close dialog box
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Destroy();
            Application.Quit();
        }

        /// <summary>
        /// Raises the shown event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnShown(object sender, System.EventArgs e)
        {
            txtAPIKey.Text = WakaTimeConfigFile.ApiKey;
            txtProxy.Text = WakaTimeConfigFile.Proxy;
            chkDebugMode.Active = WakaTimeConfigFile.Debug;
        }
    }
}

