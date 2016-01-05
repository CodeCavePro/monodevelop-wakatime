using System;
using Gtk;
using WakaTime;
using System.Diagnostics;

public partial class ApiKeyForm: Window
{
    public ApiKeyForm()
        : base(WindowType.Toplevel)
    {
        Build();
        ApiKeyForm_Load();
    }

    void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        a.RetVal = true;
        Destroy();
    }

    void ApiKeyForm_Load()
    {
        try
        {
            txtAPIKey.Text = WakaTimeConfigFile.ApiKey;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            Guid apiKey;
            var parse = Guid.TryParse(txtAPIKey.Text.Trim(), out apiKey);
            if (parse)
            {
                WakaTimeConfigFile.ApiKey = apiKey.ToString();
                WakaTimeConfigFile.Save();
                Destroy();
            }
            else
            {
                MessageBox.Show("Please enter valid API Key.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    void btnWamaTime_Clicked(object sender, EventArgs e)
    {
        Process.Start("http://wakatime.com");
    }
}
