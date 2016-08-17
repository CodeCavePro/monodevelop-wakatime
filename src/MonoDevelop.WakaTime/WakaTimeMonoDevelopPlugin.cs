using System;
using Mono.TextEditor;
using MonoDevelop.Ide;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;
using System.Linq;
using WakaTime;

namespace MonoDevelop.WakaTime
{
    internal class WakaTimeMonoDevelopPlugin : WakaTimeIdePlugin<object>
    {
        #region Fields

        ILogService _logService;
        EditorInfo _editorInfo;
        bool _disposed;
        Document _document;

        #endregion

        #region Startup/Cleanup

        public WakaTimeMonoDevelopPlugin(object editor)
            : base(editor)
        {
        }

        #endregion

        #region Event Handlers

        public override void BindEditorEvents()
        {
            IdeApp.Workbench.DocumentOpened += DocEventsOnDocumentOpened;
            IdeApp.Workbench.ActiveDocumentChanged += ActiveDocumentChanged;

            FileService.FileChanged += SolutionFileChanged;
            FileService.FileCreated += SolutionFileChanged;
            FileService.FileRemoved += SolutionFileChanged;

            // IdeApp.FocusIn += WindowEventsOnWindowActivated;
            IdeApp.Workspace.SolutionLoaded += SolutionEventsOnOpened;
            IdeApp.ProjectOperations.CurrentSelectedSolutionChanged += SolutionEventsOnOpened;
        }


        void DocEventsOnDocumentOpened(object sender, DocumentEventArgs args)
        {
            OnDocumentOpened(args.Document.FileName);
        }

        void ActiveDocumentChanged(object sender, EventArgs e)
        {
            var document = IdeApp.Workbench.ActiveDocument;
            if (document != null)
            {
                if (_document != null && _document.Editor != null)
                    _document.Editor.CaretPositionChanged -= OnCaretPositionChanged;
                OnDocumentOpened(document.FileName.FullPath);
                if (document != null && document.Editor != null)
                    document.Editor.CaretPositionChanged += OnCaretPositionChanged;
                _document = document;
            }
        }

        void OnCaretPositionChanged(object sender, EventArgs e)
        {
            OnDocumentOpened(_document.FileName.FullPath);
        }

        void SolutionFileChanged(object sender, FileEventArgs args)
        {
            var docEvent = args.FirstOrDefault(f => !f.IsDirectory);
            if (docEvent != null)
                OnDocumentChanged(docEvent.FileName.FullPath);
        }

        void SolutionEventsOnOpened(object sender, SolutionEventArgs args)
        {
            var solution = args.Solution;
            var solutionName = (solution == null)
                ? GetActiveSolutionPath()
                : solution.FileName.FileName;
            OnSolutionOpened(solutionName);
        }

        #endregion

        #region implemented abstract members of WakaTimeIdePlugin

        public override ILogService GetLogger()
        {
            if (_logService == null)
            {
                _logService = new LogService();
            }

            return _logService;
        }

        public override EditorInfo GetEditorInfo()
        {
            if (_editorInfo == null)
            {
                _editorInfo = new EditorInfo
                {
                    Name = typeof(IdeApp).Namespace.ToLowerInvariant(),
                    Version = IdeApp.Version,
                    PluginKey = "monodevelop-wakatime",
                    PluginName = "WakaTime",
                    PluginVersion = typeof(WakaTimeMonoDevelopPlugin).Assembly.GetName().Version
                };
            }

            return _editorInfo;
        }

        public override IDownloadProgressReporter GetReporter()
        {
            return new DownloadProgressForm(IdeApp.Workbench.RootWindow);
        }

        public override string GetActiveSolutionPath()
        {
            var solution = GetSolution();
            var solutionFilePath = (solution != null) 
                ? solution.FileName
                : null;
            return (solutionFilePath != null)
                ? solutionFilePath.FileName
                : null;
        }

        public override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                IdeApp.Workbench.DocumentOpened -= DocEventsOnDocumentOpened;
                IdeApp.Workbench.ActiveDocumentChanged -= ActiveDocumentChanged;

                FileService.FileChanged -= SolutionFileChanged;
                FileService.FileCreated -= SolutionFileChanged;
                FileService.FileRemoved -= SolutionFileChanged;

                // IdeApp.FocusIn -= WindowEventsOnWindowActivated;
                IdeApp.Workspace.SolutionLoaded -= SolutionEventsOnOpened;
                IdeApp.ProjectOperations.CurrentSelectedSolutionChanged -= SolutionEventsOnOpened;
            }

            _disposed = true;
        }

        public override void PromptApiKey()
        {
            ApiKeyForm apiKeyForm = null;
            try
            {
                apiKeyForm = new ApiKeyForm();
                apiKeyForm.Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to prompt for API", ex);
            }
        }

        public override void SettingsPopup()
        {
            SettingsForm settingForm;
            try
            {
                settingForm = new SettingsForm();
                settingForm.Show();
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to prompt for API", ex);
            }
        }

        #endregion

        #region Helpers

        static Solution GetSolution()
        {
            var solution = IdeApp.ProjectOperations.CurrentSelectedSolution;
            if (solution == null && IdeApp.ProjectOperations.CurrentSelectedWorkspaceItem != null)
            {
                var solutions = IdeApp.ProjectOperations.CurrentSelectedWorkspaceItem.GetAllItems<Solution>();
                solution = solutions.FirstOrDefault(); // TODO might throw exceptions
            }

            return solution;
        }


        #endregion
    }
}

