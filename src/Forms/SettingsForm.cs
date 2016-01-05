using System;
using Gtk;
using WakaTime;

public partial class SettingsForm: Window
{
    internal event EventHandler ConfigSaved;

    public SettingsForm()
        : base(WindowType.Toplevel)
    {
        Build();
        SettingsForm_Load();
    }

    void SettingsForm_Load()
    {
        try
        {
            txtAPIKey.Text = WakaTimeConfigFile.ApiKey;
            txtProxy.Text = WakaTimeConfigFile.Proxy;
            chkDebugMode.Active = WakaTimeConfigFile.Debug;
        }
        catch (Exception ex)
        {
            Logger.Error("Error when loading form SettingsForm:", ex);
            MessageBox.Show(ex.Message);
        }
    }

    void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        a.RetVal = true;
        Destroy();
    }

    void btnCancel_Clicked(object sender, EventArgs e)
    {
        Destroy();
    }

    void btnOK_Clicked(object sender, EventArgs e)
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
                OnConfigSaved();
                Destroy();
            }
            else
            {
                MessageBox.Show(@"Please enter valid Api Key."); // do not close dialog box
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Error when saving data from SettingsForm:", ex);
            MessageBox.Show(ex.Message);
        }
    }

    protected void OnConfigSaved()
    {
        var handler = ConfigSaved;
        if (handler != null)
            handler(this, EventArgs.Empty);
    }
       
}
