using System.Diagnostics;
using System.Net;
using MonoDevelop.WakaTime;
using System.IO;
using System;
using Gtk;

namespace MonoDevelop.WakaTime.Common
{
    /// <summary>
    /// Downloader.
    /// </summary>
    static class Downloader
    {
        /// <summary>
        /// Downloads the cli.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="dir">Dir.</param>
        static public void DownloadCli(string url, string dir)
        {
            Logger.Instance.Info("Downloading wakatime cli...");

            var client = new WebClient();
            var localZipFile = Path.Combine(dir, "wakatime-cli.zip");

            // Download wakatime cli
            client.DownloadFile(url, localZipFile);

            Logger.Instance.Info("Finished downloading wakatime cli.");

            Logger.Instance.Info("Extracting wakatime cli: " + dir.ToString());

            // Extract wakatime cli zip file
            ZipFile.ExtractToDirectory(localZipFile, dir);

            Logger.Instance.Info("Finished extracting wakatime cli.");
        }

        /// <summary>
        /// Downloads the python.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="dir">Dir.</param>
        static public void DownloadPython(string url, string dir)
        {
            if (PlatformID.Win32NT != Environment.OSVersion.Platform)
            {
                MessageBox.Show("Automatic download of python is not supported for your operation system." + Environment.NewLine +
                    "Please download it manually using package manager or python.org website.", MessageType.Error);
                return;
            }

            var localFile = dir + "\\python.msi";

            Logger.Instance.Info("Downloading python...");

            var client = new WebClient();
            client.DownloadFile(url, localFile);

            Logger.Instance.Info("Finished downloading python.");

            var arguments = "/i \"" + localFile + "\"";
            arguments = arguments + " /norestart /qb!";

            var procInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardError = true,
                FileName = "msiexec",
                CreateNoWindow = true,
                Arguments = arguments
            };

            Logger.Instance.Info("Installing python...");

            Process.Start(procInfo);

            Logger.Instance.Info("Finished installing python.");
        }
    }
}