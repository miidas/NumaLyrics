using Microsoft.Win32.SafeHandles;
using NLog;
using NumaLyrics.Forms;
using NumaLyrics.Players;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static NumaLyrics.Utils.Native;
using static NumaLyrics.Utils.Native.KERNEL32;
using static NumaLyrics.Utils.Native.USER32;

namespace NumaLyrics
{
    static class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeLogger();

#if DEBUG
            CreateConsole();
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
            Logger.Debug("*** DEBUG MODE ***");
#endif

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            Application.Run(new MainForm());
        }

        private static void InitializeLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var consoleTarget = new NLog.Targets.ConsoleTarget
            {
                Layout = @"${longdate}|${level}|${logger}|${message} ${exception:format=tostring}"
            };
            var consoleRule = new NLog.Config.LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.AddTarget("console", consoleTarget);
            config.LoggingRules.Add(consoleRule);
            NLog.LogManager.Configuration = config;
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            iTunes itunes = iTunes.GetInstance();
            if (!itunes.IsDisposed)
            {
                itunes.Dispose();
            }
        }

        private static void CreateConsole()
        {
            AllocConsole();
            var handle = CreateFileW("CONOUT$", GENERIC_WRITE, FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
            var safeHandle = new SafeFileHandle(handle, true);
            SetStdHandle(STD_OUTPUT_HANDLE, safeHandle);
            var fileStream = new FileStream(safeHandle, FileAccess.Write);
            var streamWriter = new StreamWriter(fileStream, Console.OutputEncoding) { AutoFlush = true };
            Console.SetOut(streamWriter);
        }
    }
}
