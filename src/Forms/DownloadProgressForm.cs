using System;
using Gtk;
using WakaTime;
using System.ComponentModel;
using System.Net;
using GLib;

public partial class DownloadProgressForm: Window, IDownloadProgressReporter
{
    public DownloadProgressForm(Window parent)
        : base(WindowType.Toplevel)
    {
        Build();

        TransientFor = parent;
        SetPosition(WindowPosition.CenterOnParent);
    }

    void OnDeleteEvent(object sender, SignalArgs a)
    {
        a.RetVal = true;
        Destroy();
    }

    public void Show(string message = "")
    {
        Application.Invoke(delegate
            {
                progressbar1.Text = message;
                base.Show();
            });
    }

    public void Close(AsyncCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            MessageBox.Show(e.Error.Message);
        }

        Destroy();
    }

    public void Report(DownloadProgressChangedEventArgs e)
    {
        Application.Invoke(delegate
            {
                progressbar1.Adjustment.Value = e.ProgressPercentage;
            });
    }
}