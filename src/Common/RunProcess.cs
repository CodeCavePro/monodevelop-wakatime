using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MonoDevelop.WakaTime.Common
{
    class RunProcess
    {
        private readonly string _program;
        private readonly string[] _arguments;
        private bool _captureOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoDevelop.WakaTime.RunProcess"/> class.
        /// </summary>
        /// <param name="program">Program.</param>
        /// <param name="arguments">Arguments.</param>
        internal RunProcess(string program, params string[] arguments)
        {
            _program = program;
            _arguments = arguments;
            _captureOutput = true;
        }

        /// <summary>
        /// Runs the in background.
        /// </summary>
        internal void RunInBackground()
        {
            _captureOutput = false;
            Run();
        }

        /// <summary>
        /// Gets the output.
        /// </summary>
        /// <value>The output.</value>
        internal string Output { get; private set; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>The error.</value>
        internal string Error { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MonoDevelop.WakaTime.RunProcess"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        internal bool Success
        {
            get { return Exception == null; }
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        internal Exception Exception { get; private set; }        

        /// <summary>
        /// Run this instance.
        /// </summary>
        internal void Run()
        {
            try
            {
                var procInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardError = _captureOutput,
                    RedirectStandardOutput = _captureOutput,
                    FileName = _program,
                    CreateNoWindow = true,
                    Arguments = GetArgumentString()
                };

                using (var process = Process.Start(procInfo))
                {
                    if (_captureOutput)
                    {

                        var stdOut = new StringBuilder();
                        var stdErr = new StringBuilder();

                        while (process != null && !process.HasExited)
                        {
                            stdOut.Append(process.StandardOutput.ReadToEnd());
                            stdErr.Append(process.StandardError.ReadToEnd());
                        }

                        if (process != null)
                        {
                            stdOut.Append(process.StandardOutput.ReadToEnd());
                            stdErr.Append(process.StandardError.ReadToEnd());
                        }

                        Output = stdOut.ToString().Trim(Environment.NewLine.ToCharArray()).Trim('\r', '\n');
                        Error = stdErr.ToString().Trim(Environment.NewLine.ToCharArray()).Trim('\r', '\n');
                    }

                    Exception = null;
                }
            }
            catch (Exception ex)
            {
                Output = null;
                Error = null;
                Exception = ex;
            }
        }

        /// <summary>
        /// Gets the argument string.
        /// </summary>
        /// <returns>The argument string.</returns>
        private string GetArgumentString()
        {
            var args = _arguments.Aggregate("", (current, arg) => current + "\"" + arg + "\" ");
            return args.TrimEnd(' ');
        }
    }
}