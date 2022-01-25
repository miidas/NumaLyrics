using Microsoft.Win32.SafeHandles;
using NLog;
using NumaLyrics.Forms;
using NumaLyrics.Players;
using System;
using System.Diagnostics;
using System.IO;
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
#if DEBUG
            var consoleRule = new NLog.Config.LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.AddTarget("console", consoleTarget);
            config.LoggingRules.Add(consoleRule);
#endif
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

            var conOut = CreateFileW(
                "CONOUT$",
                GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_WRITE,
                IntPtr.Zero,
                OPEN_EXISTING,
                0,
                IntPtr.Zero
            );

            var conOutHnd = new SafeFileHandle(conOut, true);
            SetStdHandle(STD_OUTPUT_HANDLE, conOutHnd);

            Console.SetOut(
                new StreamWriter(
                    new FileStream(conOutHnd, FileAccess.Write), 
                    Console.OutputEncoding
                ) { AutoFlush = true }
            );

            var conIn = CreateFileW(
                "CONIN$",
                GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_READ,
                IntPtr.Zero,
                OPEN_EXISTING,
                0,
                IntPtr.Zero
            );

            var conInHnd = new SafeFileHandle(conIn, true);
            SetStdHandle(STD_INPUT_HANDLE, conInHnd);

            Console.SetIn(
                new StreamReader(
                    new FileStream(conInHnd, FileAccess.Read),
                    Console.OutputEncoding
                )
            );

            // Disable the close button
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);

            // Disable the quick edit mode
            /*uint conMode;
            GetConsoleMode(conIn, out conMode);
            conMode &= ~ENABLE_QUICK_EDIT_MODE;
            SetConsoleMode(conIn, conMode);*/
        }
    }
}
