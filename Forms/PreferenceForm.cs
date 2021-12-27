using Microsoft.Win32;
using NumaLyrics.Lyrics;
using System;
using System.Drawing;
using System.Management;
using System.Windows.Forms;

namespace NumaLyrics.Forms
{
    public partial class PreferenceForm : Form
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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
            timeOffset.Value = (decimal)AppConfig.LyricsTimeOffsetMS;
            timeOffset.ValueChanged += timeOffset_ValueChanged;
            nowPlayingCheckBox.Checked = AppConfig.EnableNowPlayingFeature;
            
            loadCOMDevices();

            this.previewLyricsWindow = new LayeredLyricsWindow("This is a sample text / サンプルテキストです");
            this.previewLyricsWindow.Show();
        }

        private void loadCOMDevices()
        {
            this.comComboBox.SelectedValueChanged -= new EventHandler(comComboBox_SelectedValueChanged);

            var controllerCOM = AppConfig.ControllerCOMName;

            using (ManagementClass i_Entity = new ManagementClass("Win32_PnPEntity"))
            {
                foreach (ManagementObject i_Inst in i_Entity.GetInstances())
                {
                    Object o_Guid = i_Inst.GetPropertyValue("ClassGuid");
                    if (o_Guid == null || o_Guid.ToString().ToUpper() != "{4D36E978-E325-11CE-BFC1-08002BE10318}")
                        continue;

                    String s_DeviceID = i_Inst.GetPropertyValue("PnpDeviceID").ToString();
                    String s_RegPath = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Enum\\" + s_DeviceID + "\\Device Parameters";
                    String s_PortName = Registry.GetValue(s_RegPath, "PortName", "").ToString();
                    comComboBox.Items.Add(s_PortName);
                    
                    if (controllerCOM.Equals(s_PortName))
                    {
                        comComboBox.SelectedItem = s_PortName;
                    }
                    Logger.Debug("Port: " + s_PortName);
                }
            }

            this.comComboBox.SelectedValueChanged += new EventHandler(comComboBox_SelectedValueChanged);
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
            AppConfig.LyricsTimeOffsetMS = (int)timeOffset.Value;
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

        private void nowPlayingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AppConfig.EnableNowPlayingFeature = nowPlayingCheckBox.Checked;
        }

        private void PreferenceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.previewLyricsWindow.Dispose();
        }

        private void comComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            AppConfig.ControllerCOMName = comComboBox.SelectedItem.ToString();
            ((MainForm)this.Owner).GetControllerDevice().InitializeSerialController();
        }

        private void controllerEditorButton_Click(object sender, EventArgs e)
        {
            var form = new ControllerEditorForm();
            form.Owner = this;
            form.ShowDialog();
        }
    }
}
