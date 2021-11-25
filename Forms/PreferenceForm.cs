using NumaLyrics.Lyrics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumaLyrics.Forms
{
    public partial class PreferenceForm : Form
    {
        private LayeredLyricsWindow previewLyricsWindow;

        public PreferenceForm()
        {
            InitializeComponent();
        }

        private void PreferenceForm_Load(object sender, EventArgs e)
        {
            fontColorPicBox.BackColor = ColorTranslator.FromHtml(AppConfig.FontColor);
            fontOutlineColorPicBox.BackColor = ColorTranslator.FromHtml(AppConfig.FontOutlineColor);
            fontOutlineWidth.Value = (decimal)AppConfig.FontOutlineWidth;
            fontOutlineWidth.ValueChanged += fontOutlineWidth_ValueChanged;
            displayIndex.Value = AppConfig.DisplayIndex;
            displayIndex.Maximum = Screen.AllScreens.Length - 1;
            displayIndex.ValueChanged += displayIndex_ValueChanged;
            displayXPos.Value = (decimal)AppConfig.DisplayPositionX;
            displayXPos.ValueChanged += displayXPos_ValueChanged;
            displayYPos.Value = (decimal)AppConfig.DisplayPositionY;
            displayYPos.ValueChanged += displayYPos_ValueChanged;
            timeOffset.Value = (decimal)AppConfig.LyricsTimeOffset;
            timeOffset.ValueChanged += timeOffset_ValueChanged;

            this.previewLyricsWindow = new LayeredLyricsWindow("This is a sample text / サンプルテキストです");
            this.previewLyricsWindow.Show();
        }

        private void fontOutlineWidth_ValueChanged(object sender, EventArgs e)
        {
            AppConfig.FontOutlineWidth = (float)fontOutlineWidth.Value;
            this.previewLyricsWindow.Redraw();
        }

        private void displayIndex_ValueChanged(object sender, EventArgs e)
        {
            AppConfig.DisplayIndex = (int)displayIndex.Value;
            this.previewLyricsWindow.Redraw();
        }

        private void displayXPos_ValueChanged(object sender, EventArgs e)
        {
            AppConfig.DisplayPositionX = (float)displayXPos.Value;
            this.previewLyricsWindow.Redraw();
        }

        private void displayYPos_ValueChanged(object sender, EventArgs e)
        {
            AppConfig.DisplayPositionY = (float)displayYPos.Value;
            this.previewLyricsWindow.Redraw();
        }

        private void timeOffset_ValueChanged(object sender, EventArgs e)
        {
            AppConfig.LyricsTimeOffset = (int)timeOffset.Value;
        }

        private void fontSelectButton_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.ShowEffects = false;
            dialog.Font = new Font(new FontFamily(AppConfig.FontFamily), AppConfig.FontSize, (FontStyle)AppConfig.FontStyle);

            if (dialog.ShowDialog() != DialogResult.Cancel)
            {
                AppConfig.FontFamily = dialog.Font.FontFamily.Name;
                AppConfig.FontSize = dialog.Font.Size;
                AppConfig.FontStyle = (int)dialog.Font.Style;
                this.previewLyricsWindow.Redraw();
            }
        }

        private void fontColorPicBox_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.FullOpen = true;
            dialog.ShowHelp = true;
            dialog.Color = fontColorPicBox.BackColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fontColorPicBox.BackColor = dialog.Color;
                AppConfig.FontColor = ColorTranslator.ToHtml(dialog.Color);
                this.previewLyricsWindow.Redraw();
            }
        }

        private void fontOutlineColorPicBox_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.FullOpen = true;
            dialog.ShowHelp = true;
            dialog.Color = fontOutlineColorPicBox.BackColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fontOutlineColorPicBox.BackColor = dialog.Color;
                AppConfig.FontOutlineColor = ColorTranslator.ToHtml(dialog.Color);
                this.previewLyricsWindow.Redraw();
            }
        }

        private void PreferenceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.previewLyricsWindow.Dispose();
        }
    }
}
