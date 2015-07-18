using System;
using Gtk;

namespace MonoDevelop.WakaTime
{
    /// <summary>
    /// Message box class.
    /// </summary>
    public static class MessageBox
    {
        /// <summary>
        /// Show the specified message.
        /// </summary>
        /// <param name="message">Message to show.</param>
        /// <param name="type">Type of the message to show.</param>
        public static void Show(string message, MessageType type = MessageType.Info)
        {
            var md = new MessageDialog (null, DialogFlags.Modal, type, ButtonsType.Ok, message);
            md.Run ();
            md.Destroy();
        }
    }
}

