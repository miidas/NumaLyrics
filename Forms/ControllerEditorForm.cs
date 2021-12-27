using NumaLyrics.Configs;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace NumaLyrics.Forms
{
    public partial class ControllerEditorForm : Form
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ControllerEditorForm()
        {
            InitializeComponent();
        }

        private void ControllerEditorForm_Load(object sender, EventArgs e)
        {
            var mainForm = (MainForm)this.Owner.Owner;
            var device = mainForm.GetControllerDevice();
            this.deviceNameLabel.Text = String.Format("{0} {1} on {2}", device.name, device.version, device.comName);

            for (var i = 1; i <= device.numberOfButtons; i++)
            {
                this.buttonListBox.Items.Add("Button " + i);
            }

            this.buttonListBox.SelectedIndex = 0;
        }

        private void buttonListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonInfoBox.Text = "Button " + (buttonListBox.SelectedIndex + 1);

            var mainForm = (MainForm)this.Owner.Owner;
            var device = mainForm.GetControllerDevice();

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var deviceSettings = (ControllerDeviceSection)configFile.GetSection("controllerDeviceSettings");
            var deviceSetting = deviceSettings.Devices.Get(device.uuid);

            var btnNum = buttonListBox.SelectedIndex;
            var clickAction = deviceSetting.ButtonActions.Get(btnNum + ",0");
            var doubleClickAction = deviceSetting.ButtonActions.Get(btnNum + ",1");
            clickActionLabel.Text = (clickAction != null)? (!String.IsNullOrEmpty(clickAction.action))? clickAction.action : "(empty)" : "(empty)";
            doubleClickActionLabel.Text = (doubleClickAction != null) ? (!String.IsNullOrEmpty(doubleClickAction.action)) ? doubleClickAction.action : "(empty)" : "(empty)";
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            var btnNum = buttonListBox.SelectedIndex;
            var form = new ButtonActionForm(btnNum);
            form.Owner = this;
            form.Text = buttonInfoBox.Text;
            form.FormClosed += (cs, ce) => {
                buttonListBox_SelectedIndexChanged(sender, e);
            };
            form.ShowDialog();
        }
    }
}
