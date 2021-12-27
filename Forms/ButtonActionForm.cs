using NumaLyrics.Configs;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace NumaLyrics.Forms
{
    public partial class ButtonActionForm : Form
    {
        private int btnNum;

        public ButtonActionForm(int btnNum)
        {
            this.btnNum = btnNum;

            InitializeComponent();
        }

        private void ButtonActionForm_Load(object sender, EventArgs e)
        {
            var mainForm = (MainForm)this.Owner.Owner.Owner;
            var device = mainForm.GetControllerDevice();

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var deviceSettings = (ControllerDeviceSection)configFile.GetSection("controllerDeviceSettings");
            var deviceSetting = deviceSettings.Devices.Get(device.uuid);

            var clickAction = deviceSetting.ButtonActions.Get(btnNum + ",0");
            var doubleClickAction = deviceSetting.ButtonActions.Get(btnNum + ",1");
            if (clickAction != null) clickTextBox.Text = clickAction.action;
            if (doubleClickAction != null) doubleClickTextBox.Text = doubleClickAction.action;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var mainForm = (MainForm)this.Owner.Owner.Owner;
            var device = mainForm.GetControllerDevice();

            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var deviceSettings = (ControllerDeviceSection)configFile.GetSection("controllerDeviceSettings");
            var deviceSetting = deviceSettings.Devices.Get(device.uuid);
            var clickAction = deviceSetting.ButtonActions.Get(btnNum + ",0");
            var doubleClickAction = deviceSetting.ButtonActions.Get(btnNum + ",1");

            if (clickAction != null)
            {
                clickAction.action = clickTextBox.Text;
            }
            else
            {
                var action = new ButtonActionConfig();
                action.id = btnNum + ",0";
                action.action = doubleClickTextBox.Text;
                deviceSetting.ButtonActions.Add(action);
            }

            if (doubleClickAction != null)
            {
                doubleClickAction.action = doubleClickTextBox.Text;
            }
            else
            {
                var action = new ButtonActionConfig();
                action.id = btnNum + ",1";
                action.action = doubleClickTextBox.Text;
                deviceSetting.ButtonActions.Add(action);
            }

            configFile.Save();

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
