using System;
using System.IO;
using System.Reflection;
using MonoDevelop.Ide;
using MonoDevelop.Core;
using MonoDevelop.Core.Logging;
using MonoDevelop.Ide.Gui;
using System.Threading;
using System.Collections.Generic;
using MonoDevelop.Projects;
using System.Linq;

namespace MonoDevelop.WakaTime.Common
{
    public class WakaTimePackage 
    {
        #region Fields
        private static string _version = string.Empty;
        private static string _editorVersion = string.Empty;     

        private static string _lastFile;
        private static string _solutionName = string.Empty;

        DateTime _lastHeartbeat = DateTime.UtcNow.AddMinutes(-3);
        private static readonly object ThreadLock = new object();

        #endregion

        #region Startup/Cleanup         

        internal void Initialize()
        {
            Logger.Instance.Initialize(new ConsoleLogger());
            try
            {
                _version = string.Format("{0}.{1}.{2}", CoreAssembly.Version.Major, CoreAssembly.Version.Minor, CoreAssembly.Version.Build);
                _editorVersion = IdeApp.Version.ToString();
                Logger.Instance.Info("Initializing WakaTime v" + _version);

                // Make sure python is installed
                if (!PythonManager.IsPythonInstalled())
                {
                    var url = PythonManager.GetPythonDownloadUrl();
                    Downloader.DownloadPython(url, ConfigDir);
                }

                if (!DoesCliExist() || !IsCliLatestVersion())
                {
                    try
                    {
                        Directory.Delete(ConfigDir + "wakatime-master", true);
                    }
                    catch { /* ignored */ }

                    Downloader.DownloadCli(WakaTimeConstants.CliUrl, ConfigDir);
                }

                if (string.IsNullOrEmpty(WakaTimeConfigFile.ApiKey))
                    PromptApiKey();

                IdeApp.Workbench.DocumentOpened += DocEventsOnDocumentOpened;
                IdeApp.Workbench.ActiveDocumentChanged += ActiveDocumentChanged;

                FileService.FileChanged += SolutionFileChanged;
                FileService.FileCreated += SolutionFileChanged;
                FileService.FileRemoved += SolutionFileChanged;

                // TODO use this: IdeApp.FocusIn += WindowEventsOnWindowActivated;
                IdeApp.Workbench.RootWindow.FocusActivated += WindowEventsOnWindowActivated;

                IdeApp.Workspace.SolutionLoaded += SolutionEventsOnOpened;
                IdeApp.ProjectOperations.CurrentSelectedSolutionChanged += SolutionEventsOnOpened;

                Logger.Instance.Info("Finished initializing WakaTime v" + _version);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.Message);
            }
        }
        #endregion

        #region Event Handlers
        private void DocEventsOnDocumentOpened(object sender, DocumentEventArgs args)
        {
            try
            {
                HandleActivity(args.Document.FileName, false);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("DocEventsOnDocumentOpened : " + ex.Message);
            }
        }

        void ActiveDocumentChanged(object sender, EventArgs e)
        {
            try
            {
                var document = IdeApp.Workbench.ActiveDocument;
                if (document != null)
                    HandleActivity(document.FileName, false);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("ActiveDocumentChanged : " + ex.Message);
            }
        }

        private void WindowEventsOnWindowActivated(object sender, EventArgs args)
        {
            try
            {
                var document = IdeApp.Workbench.ActiveDocument;
                if (document != null)
                    HandleActivity(document.FileName, false);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("WindowEventsOnWindowActivated : " + ex.Message);
            }
        }

        private void SolutionFileChanged(object sender, FileEventArgs args)
        {
            try
            {
                var solution = IdeApp.ProjectOperations.CurrentSelectedSolution;
                if(solution == null)
                    return;

                var fileEvent = args.LastOrDefault();
                if(fileEvent == null)
                    return;
                
                HandleActivity(fileEvent.FileName, true);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("FileChanged : " + ex.Message);
            }
        }

        private void SolutionEventsOnOpened(object sender, SolutionEventArgs args)
        {
            try
            {
                _solutionName = args.Solution.Name;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("SolutionEventsOnOpened : " + ex.Message);
            }
        }
        #endregion

        #region Methods

        private void HandleActivity(string currentFile, bool isWrite)
        {
            if (currentFile == null) return;

            var thread = new Thread(
                delegate()
                {
                    lock (ThreadLock)
                    {
                        if (!isWrite && _lastFile != null && !EnoughTimePassed() && currentFile.Equals(_lastFile))
                            return;

                        SendHeartbeat(currentFile, isWrite);
                        _lastFile = currentFile;
                        _lastHeartbeat = DateTime.UtcNow;
                    }
                });
            thread.Start();
        }

        private bool EnoughTimePassed()
        {
            return _lastHeartbeat < DateTime.UtcNow.AddMinutes(-1);
        }

        static string ConfigDir
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); }
        }

        static string GetCli()
        {
            return Path.Combine(ConfigDir, WakaTimeConstants.CliFolder);
        }

        public static void SendHeartbeat(string fileName, bool isWrite)
        {
            var arguments = new List<string>
                {
                    GetCli(),
                    "--key",
                    WakaTimeConfigFile.ApiKey,
                    "--file",
                    fileName,
                    "--plugin",
                    WakaTimeConstants.EditorName + "/" + _editorVersion + " " + WakaTimeConstants.PluginName + "/" + _version
                };

            if (isWrite)
                arguments.Add("--write");

            var projectName = GetProjectName();
            if (!string.IsNullOrEmpty(projectName))
            {
                arguments.Add("--project");
                arguments.Add(projectName);
            }

            var pythonBinary = PythonManager.GetPython();
            if (pythonBinary != null)
            {

                var process = new RunProcess(pythonBinary, arguments.ToArray());
                if (WakaTimeConfigFile.Debug)
                {
                    Logger.Instance.Info("[\"" + pythonBinary + "\", \"" + string.Join("\", ", arguments) + "\"]");
                    process.Run();
                    Logger.Instance.Info("WakaTime CLI STDOUT:" + process.Output);
                    Logger.Instance.Info("WakaTime CLI STDERR:" + process.Error);
                }
                else
                {
                    process.RunInBackground();
                }

            }
            else
            {
                Logger.Instance.Error("Could not send heartbeat because python is not installed.");
            }
        }

        static bool DoesCliExist()
        {
            return File.Exists(GetCli());
        }

        static bool IsCliLatestVersion()
        {
            var process = new RunProcess(PythonManager.GetPython(), GetCli(), "--version");
            process.Run();

            return process.Success && process.Error.Equals(WakaTimeConstants.CurrentWakaTimeCliVersion);
        }

        public static void MenuItemCallback()
        {
            try
            {
                SettingsPopup();
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("MenuItemCallback : " + ex.Message);
            }
        }

        private static void PromptApiKey()
        {
            using (var form = new ApiKeyWindow())
            {
                form.Show();
            }
        }

        private static void SettingsPopup()
        {
            using (var form = new SettingsWindow())
            {
                form.Show();
            }
        }

        private static string GetProjectName()
        {
            if (!string.IsNullOrEmpty(_solutionName))
                return Path.GetFileNameWithoutExtension(_solutionName);

            var solution = IdeApp.ProjectOperations.CurrentSelectedSolution;
            if (solution == null && IdeApp.ProjectOperations.CurrentSelectedWorkspaceItem != null)
            {
                var solutions = IdeApp.ProjectOperations.CurrentSelectedWorkspaceItem.GetAllSolutions();
                solution = solutions.FirstOrDefault(); // TODO might throw exceptions
            }

            return (solution != null) 
                ? (_solutionName = solution.Name)
                : IdeApp.Workbench.ActiveDocument.Name;
        }
        #endregion

        static class CoreAssembly
        {
            static readonly Assembly Reference = typeof(CoreAssembly).Assembly;
            public static readonly Version Version = Reference.GetName().Version;
        }
    }
}

