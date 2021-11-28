using Kfstorm.LrcParser;
using System;
using System.Windows.Forms;

namespace NumaLyrics.Lyrics
{
    class LyricsDisplay
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly LyricsDisplay instance = new LyricsDisplay();

        private int LyricsTimeOffset;

        private ILrcFile lrcFile = null;

        private LayeredLyricsWindow currentLyricsWindow = null;
        private LayeredLyricsWindow nextLyricsWindow = null;

        private IOneLineLyric currentLyric = null;

        private bool EnableLyrics = true;

        public LyricsDisplay()
        {
            this.LoadConfig();
        }

        public void LoadConfig()
        {
            this.LyricsTimeOffset = AppConfig.LyricsTimeOffset;
        }

        public void OnChangePlayerPosition(int position)
        {
            if (lrcFile == null) return;

            // Check if the mouse pointer is in the Window
            if (this.currentLyricsWindow != null)
            {
                if (this.EnableLyrics)
                {
                    if (this.currentLyricsWindow.ClientRectangle.Contains(currentLyricsWindow.PointToClient(Control.MousePosition)))
                    {
                        this.currentLyricsWindow.Hide();
                        return;
                    }
                    if (!this.currentLyricsWindow.Visible)
                    {
                        this.currentLyricsWindow.Show();
                    }
                }
                else
                {
                    this.currentLyricsWindow.Hide();
                }
            }

            position -= LyricsTimeOffset;

            IOneLineLyric lineLyric = lrcFile.Before(TimeSpan.FromMilliseconds(position));

            if (lineLyric == null)
            {
                if (this.currentLyricsWindow != null)
                {
                    ClearLyrics();
                }
                return;
            }
            else
            {
                if (this.currentLyric != null && 
                    lineLyric.Timestamp == this.currentLyric.Timestamp)
                {
                    return;
                }
            }

            Logger.Debug("lineLyric: {time} : {text}", lineLyric.Timestamp, lineLyric.Content);

            IOneLineLyric lineLyric2 = lrcFile.After(lineLyric.Timestamp);

            this.currentLyric = lineLyric;

            var tmpWindow = this.currentLyricsWindow;

            if (String.IsNullOrEmpty(lineLyric.Content))
            {
                if (this.currentLyricsWindow != null)
                {
                    this.currentLyricsWindow.Hide();
                    this.currentLyricsWindow.Dispose();
                    this.currentLyricsWindow = null;
                }

                if (this.nextLyricsWindow == null &&
                    lineLyric2 != null && !String.IsNullOrEmpty(lineLyric2.Content))
                {
                    this.nextLyricsWindow = new LayeredLyricsWindow(lineLyric2.Content);
                }
            }
            else
            {
                if (this.nextLyricsWindow != null)
                {
                    this.currentLyricsWindow = this.nextLyricsWindow;
                    this.nextLyricsWindow = null;
                }
                else
                {
                    this.currentLyricsWindow = new LayeredLyricsWindow(currentLyric.Content);
                }

                if (this.EnableLyrics)
                {
                    this.currentLyricsWindow.Show();
                    if (tmpWindow != null)
                    {
                        tmpWindow.Hide();
                        tmpWindow.Dispose();
                    }
                }

                if (lineLyric2 != null && !String.IsNullOrEmpty(lineLyric2.Content) &&
                    !String.Equals(lineLyric.Content, lineLyric2.Content))
                {
                    this.nextLyricsWindow = new LayeredLyricsWindow(lineLyric2.Content);
                }
            }
        }

        public void HideLyrics()
        {
            this.EnableLyrics = false;
            if (this.currentLyricsWindow != null) this.currentLyricsWindow.Hide();
        }

        public void ShowLyrics()
        {
            this.EnableLyrics = true;
        }

        public void SetLrcFile(ILrcFile lrcFile)
        {
            this.lrcFile = lrcFile;
        }

        public void OnTrackChanged(object trackObj)
        {
            this.ClearLyrics();
        }

        public void ClearLyrics()
        {
            if (this.currentLyricsWindow != null)
            {
                this.currentLyricsWindow.Hide();
                this.currentLyricsWindow.Dispose();
                this.currentLyricsWindow = null;
            }
            this.nextLyricsWindow = null;
            this.currentLyric = null;
        }

        public static LyricsDisplay GetInstance()
        {
            return instance;
        }
    }
}
