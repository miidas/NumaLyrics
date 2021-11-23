using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumaLyrics.Forms
{
    public partial class LyricsEditForm : Form
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private string lrcFilePath;

        public LyricsEditForm(string lrcFilePath)
        {
            this.lrcFilePath = lrcFilePath;
            InitializeComponent();
        }

        private void LyricsEditForm_Load(object sender, EventArgs e)
        {
            this.Text = this.lrcFilePath;

            try
            {
                this.lrcTextBox.Text = System.IO.File.ReadAllText(this.lrcFilePath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void SaveLrcFile()
        {
            File.WriteAllText(this.lrcFilePath, this.lrcTextBox.Text);
            lrcTextBox.Modified = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace(MethodBase.GetCurrentMethod().Name);
            SaveLrcFile();
            this.Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Trace(MethodBase.GetCurrentMethod().Name);
            this.Close();
        }

        private void LyricsEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.Debug("Modified?: {0}", lrcTextBox.Modified);
            if (lrcTextBox.Modified)
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to save changes?",
                    "NumaLyrics",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    SaveLrcFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
