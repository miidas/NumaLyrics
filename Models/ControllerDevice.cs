using NumaLyrics.Configs;
using NumaLyrics.Lyrics;
using NumaLyrics.Players;
using System;
using System.Configuration;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

using static NumaLyrics.Utils.Native.DBT;

namespace NumaLyrics.Models
{
    public class ControllerDevice
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly iTunes itunes = iTunes.GetInstance();

        private SerialPort port;

        public string comName;
        public string name;
        public string version;
        public string uuid;
        public int numberOfButtons;

        private string serialBuf = "";

        private volatile int notifyCounter = 0;
        private LayeredLyricsWindow notifyWindow = new LayeredLyricsWindow(0.5f, 0.5f, 32f);

        public void HandleWndProc(ref Message m)
        {
            if (m.Msg == WM_DEVICECHANGE)
            {
                if (m.WParam.ToInt32() == DBT_DEVICEARRIVAL)
                {
                    int devType = Marshal.ReadInt32(m.LParam, 4);
                    if (devType == DBT_DEVTYP_PORT)
                    {
                        string portName = Marshal.PtrToStringUni((IntPtr)(m.LParam.ToInt32() + Marshal.SizeOf(typeof(DEV_BROADCAST_PORT))));
                        if (portName.Equals(AppConfig.ControllerCOMName))
                        {
                            InitializeSerialController();
                            Logger.Debug("CONNECT {0}", portName);
                        }
                    }
                }
                else if (m.WParam.ToInt32() == DBT_DEVICEREMOVECOMPLETE)
                {
                    // DBT_DEVICEREMOVECOMPLETE event
                    // https://docs.microsoft.com/en-us/windows/win32/devio/dbt-deviceremovecomplete
                    //
                    int devType = Marshal.ReadInt32(m.LParam, 4);
                    if (devType == DBT_DEVTYP_PORT)
                    {
                        Logger.Debug("DISCONNECTED");
                        NotifyToUser("Controller disconnected");
                    }
                }
            }
        }

        public void InitializeSerialController()
        {
            if (port != null && port.IsOpen) port.Close();
            
            var comName = AppConfig.ControllerCOMName;
            if (String.IsNullOrEmpty(comName)) return;
            this.comName = comName;
            
            serialBuf = "";

            port = new SerialPort(comName);
            port.DataReceived += new SerialDataReceivedEventHandler(SerialReceivedHandler);
            port.DtrEnable = true;

            try
            {
                port.Open();

                // Send initialize commands
                port.WriteLine("IDN");
                port.WriteLine("NOB");
                port.WriteLine("SBA");
            }
            catch (Exception e)
            {
                //MessageBox.Show("Failed to open a controller", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(e, "Failed to open a controler");
            }
        }

        private void SerialReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var raw = serialBuf + port.ReadExisting().Replace(Environment.NewLine, "\n");
            //Logger.Debug("Raw: {0}", raw);
            //Logger.Debug("Buf: {0}", buf);
            int pos;
            while ((pos = raw.IndexOf('\n')) > 0)
            {
                var res = raw.Substring(0, pos);
                raw = raw.Substring(pos + 1, raw.Length - (pos + 1));

                var tmp = res.Split('+');
                if (tmp.Length != 2) return;

                //Logger.Debug("Res: {0}", res);

                var cmd = tmp[0];
                var data = tmp[1];

                if (cmd.Equals("BTN")) // Button
                {
                    // BTN+[DATA]
                    // DATA
                    // string - button number
                    // ,      - separator
                    // string - button action

                    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var deviceSettings = (ControllerDeviceSection)configFile.GetSection("controllerDeviceSettings");
                    var deviceSetting = deviceSettings.Devices.Get(this.uuid);
                    var buttonAction = deviceSetting.ButtonActions.Get(data);

                    var tmp2 = buttonAction.action.IndexOf(' ');
                    // TODO: Support multiple args?
                    var arg = (tmp2 != -1) ? buttonAction.action.Substring(tmp2 + 2, buttonAction.action.Length - tmp2 - 3) : "";
                    if (tmp2 == -1) tmp2 = buttonAction.action.Length;
                    var action = buttonAction.action.Substring(0, tmp2);

                    Logger.Debug("Action: " + action);
                    Logger.Debug("Arg: " + arg);

                    switch (action)
                    {
                        case "TOGGLE_PLAY":
                            NotifyToUser(itunes.TogglePlay() ? "Play" : "Pause");
                            break;
                        case "TOGGLE_SHUFFLE":
                            NotifyToUser("Shuffle: " + (itunes.ToggleShuffle() ? "ON" : "OFF"));
                            break;
                        case "PLAY_NEXT":
                            NotifyToUser("Next");
                            itunes.NextTrack();
                            break;
                        case "PLAY_PLAYLIST_BY_NAME":
                            NotifyToUser("Play: " + arg);
                            itunes.PlayPlaylistByName(arg);
                            break;
                        case "SHOW_SONG_TITLE":
                            var track = (dynamic)itunes.getCurrentTrack();
                            NotifyToUser(track.Name);
                            break;
                        case "DEBUG":
                            itunes.Test2();
                            break;
                        default:
                            break;
                    }
                }
                else if (cmd.Equals("IDN")) // Identification
                {
                    // IDN+[DATA]
                    // DATA
                    // string - device name
                    // ,      - separator
                    // string - device version
                    // ,      - separator
                    // string - device uuid

                    tmp = data.Split(',');
                    this.name = tmp[0];
                    this.version = tmp[1];
                    this.uuid = tmp[2];

                    NotifyToUser("Connect: " + name + " " + version);
                    Logger.Debug("Connect: {0} {1} {2}", name, version, uuid);
                }
                else if (cmd.Equals("NOB")) // Number of buttons
                {
                    // NOB+[DATA]
                    // DATA
                    // int - number of buttons
                    this.numberOfButtons = int.Parse(data);
                    Logger.Debug("NOB: {0}", data);
                }
                else if (cmd.Equals("SBA")) // Supported button action
                {
                    // SBA+[DATA]
                    // DATA
                    // int - supported button action
                    //controllerDevice.supportedButtonAction = int.Parse(data);
                    Logger.Debug("SBA: {0}", data);
                }
            }
            serialBuf = raw;
        }

        private void NotifyToUser(string str)
        {
            _ = Task.Run(() =>
            {
                var counter = System.Threading.Interlocked.Increment(ref notifyCounter);
                notifyWindow.Invoke((MethodInvoker)(() => {
                    notifyWindow.LyricsText = str;
                    notifyWindow.Redraw();
                    notifyWindow.Show();
                }));
                System.Threading.Thread.Sleep(1000);
                notifyWindow.Invoke((MethodInvoker)(() => {
                    if (notifyCounter != counter) return;
                    notifyWindow.Hide();
                }));
            });
        }
    }
}
