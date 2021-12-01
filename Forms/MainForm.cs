using Kfstorm.LrcParser;
using NumaLyrics.Lyrics;
using NumaLyrics.Players;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace NumaLyrics.Forms
{
    public partial class MainForm : Form
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private iTunes itunes = iTunes.GetInstance();

        private LyricsDisplay lyricsDisplay = LyricsDisplay.GetInstance();

        private Timer timer;

        private string lyricsDirectory;

        private string lrcPath;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = titleLabel;
            this.notifyIcon.Icon = this.Icon;

            lyricsDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Lyrics";
            Logger.Trace("lyricsDirectory: {dir}", lyricsDirectory);
            if (!Directory.Exists(lyricsDirectory))
            {
                try
                {
                    Directory.CreateDirectory(lyricsDirectory);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Failed to create `Lyrics` directory");
                }
            }

            itunes.OnPlayerPlay += OnPlayerPlay;
            itunes.OnPlayerStop += OnPlayerStop;
            itunes.OnPlayerTrackChanged += OnPlayerTrackChanged;

            OnPlayerTrackChanged(itunes.getCurrentTrack());

            updateMenuStrip();

            InitializeTimer();
        }

        private void OnPlayerPlay(object trackObj)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                lyricsDisplay.ClearLyrics(); // clear cache
                if (showLyricsCheckBox.Checked) lyricsDisplay.ShowLyrics();
            }));
        }

        private void OnPlayerStop(object trackObj)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                lyricsDisplay.HideLyrics();
            }));
        }

        private void OnPlayerTrackChanged(object trackObj)
        {
            lyricsDisplay.OnTrackChanged(trackObj);

            if (trackObj == null)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    titleTextBox.Text = "";
                    albumTextBox.Text = "";
                    artistTextBox.Text = "";
                }));
                return;
            }

            dynamic track = trackObj;

#if DEBUG
            Logger.Debug("============== Track ==============");
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(track))
            {
                Logger.Debug("{key}: {value}", prop.Name, prop.GetValue(track));
            }
#endif

            this.Invoke(new MethodInvoker(delegate
            {
                titleTextBox.Text = track.Name;
                albumTextBox.Text = track.Album;
                artistTextBox.Text = track.Artist;
            }));

            try
            {
                // Use escaped filename instead of TrackDatabaseID for readability
                this.lrcPath = $"{lyricsDirectory}\\"
                    + String.Join(
                        "_",
                        $"{track.Name} - {track.Artist}.lrc"
                        .Split(System.IO.Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)
                        )
                    .TrimEnd('.');
                Logger.Debug("Lrc path: {path}", this.lrcPath);
                var lrcText = File.ReadAllText(this.lrcPath);
                var lrcFile = LrcFile.FromText(lrcText);
                lyricsDisplay.SetLrcFile(lrcFile);
            }
            catch (FileNotFoundException ex)
            {
                Logger.Error("Failed to load a lrc file: {path}", ex.FileName);
                lyricsDisplay.SetLrcFile(null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                lyricsDisplay.SetLrcFile(null);
            }
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 55;
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (!itunes.IsPlaying) return;

            var positionObj = itunes.getPlayerPositionMS();
            if (positionObj != null)
            {
                int position = (int)positionObj;

                lyricsDisplay.OnChangePlayerPosition(position);

                //Logger.Trace(position);
            }
        }

        private void aboutNumaLyricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"NumaLyrics ({ThisAssembly.AssemblyConfiguration}) v{ThisAssembly.AssemblyInformationalVersion}\niTunes Version {itunes.getVersion()}", "About NumaLyrics");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editLrcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itunes.CurrentTrackObj != null)
            {
                var form = new LyricsEditForm(this.lrcPath);
                form.FormClosed += (cs, ce) =>
                {
                    OnPlayerTrackChanged(itunes.CurrentTrackObj);
                };
                form.ShowDialog(this);
            }
        }

        private void preferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lyricsDisplay.HideLyrics();
            var form = new PreferenceForm();
            form.FormClosed += (cs, ce) => {
                lyricsDisplay.LoadConfig();
                lyricsDisplay.ClearLyrics(); // Clear cache
                updateMenuStrip();
                if (showLyricsCheckBox.Checked) lyricsDisplay.ShowLyrics();
            };
            form.ShowDialog(this);
        }

        private void showLyricsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showLyricsCheckBox.Checked)
            {
                lyricsDisplay.ShowLyrics();
            }
            else
            {
                lyricsDisplay.HideLyrics();
            }
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.ActiveControl = titleLabel;
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon.Visible = true;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.notifyIcon.Visible = false;
            }
        }

        private void updateMenuStrip()
        {
            ToolStripMenuItem npMenuItem = null;
            foreach (ToolStripMenuItem i in menuStrip.Items)
            {
                if (i.Text == "NowPlaying")
                {
                    npMenuItem = i;
                }
            }

            if (AppConfig.EnableNowPlayingFeature)
            {
                if (npMenuItem == null)
                {
                    var item = new ToolStripMenuItem("NowPlaying");
                    item.Click += (es, ee) =>
                    {
                        var tweetText = $"#Nowplaying {this.titleTextBox.Text} - {this.artistTextBox.Text} ({this.albumTextBox.Text})";
                        System.Diagnostics.Process.Start("https://twitter.com/intent/tweet?text=" + System.Net.WebUtility.UrlEncode(tweetText));
                    };
                    menuStrip.Items.Insert(1, item);
                }
            }
            else
            {
                if (npMenuItem != null)
                {
                    menuStrip.Items.Remove(npMenuItem);
                }
            }
        }


    }
}
