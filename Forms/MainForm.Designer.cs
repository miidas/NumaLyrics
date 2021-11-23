
namespace NumaLyrics.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLrcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutNumaLyricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.albumTextBox = new System.Windows.Forms.TextBox();
            this.artistTextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.albumLabel = new System.Windows.Forms.Label();
            this.artistLabel = new System.Windows.Forms.Label();
            this.showLyricsCheckBox = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.notifyContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(497, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editLrcToolStripMenuItem,
            this.preferenceToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editLrcToolStripMenuItem
            // 
            this.editLrcToolStripMenuItem.Name = "editLrcToolStripMenuItem";
            this.editLrcToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.editLrcToolStripMenuItem.Text = "Edit lrc";
            this.editLrcToolStripMenuItem.Click += new System.EventHandler(this.editLrcToolStripMenuItem_Click);
            // 
            // preferenceToolStripMenuItem
            // 
            this.preferenceToolStripMenuItem.Name = "preferenceToolStripMenuItem";
            this.preferenceToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.preferenceToolStripMenuItem.Text = "Preference";
            this.preferenceToolStripMenuItem.Click += new System.EventHandler(this.preferenceToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutNumaLyricsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutNumaLyricsToolStripMenuItem
            // 
            this.aboutNumaLyricsToolStripMenuItem.Name = "aboutNumaLyricsToolStripMenuItem";
            this.aboutNumaLyricsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.aboutNumaLyricsToolStripMenuItem.Text = "About NumaLyrics";
            this.aboutNumaLyricsToolStripMenuItem.Click += new System.EventHandler(this.aboutNumaLyricsToolStripMenuItem_Click);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(12, 44);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.ReadOnly = true;
            this.titleTextBox.Size = new System.Drawing.Size(473, 20);
            this.titleTextBox.TabIndex = 1;
            // 
            // albumTextBox
            // 
            this.albumTextBox.Location = new System.Drawing.Point(12, 83);
            this.albumTextBox.Name = "albumTextBox";
            this.albumTextBox.ReadOnly = true;
            this.albumTextBox.Size = new System.Drawing.Size(473, 20);
            this.albumTextBox.TabIndex = 2;
            // 
            // artistTextBox
            // 
            this.artistTextBox.Location = new System.Drawing.Point(12, 122);
            this.artistTextBox.Name = "artistTextBox";
            this.artistTextBox.ReadOnly = true;
            this.artistTextBox.Size = new System.Drawing.Size(473, 20);
            this.artistTextBox.TabIndex = 3;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(9, 28);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(27, 13);
            this.titleLabel.TabIndex = 4;
            this.titleLabel.Text = "Title";
            // 
            // albumLabel
            // 
            this.albumLabel.AutoSize = true;
            this.albumLabel.Location = new System.Drawing.Point(9, 67);
            this.albumLabel.Name = "albumLabel";
            this.albumLabel.Size = new System.Drawing.Size(36, 13);
            this.albumLabel.TabIndex = 5;
            this.albumLabel.Text = "Album";
            // 
            // artistLabel
            // 
            this.artistLabel.AutoSize = true;
            this.artistLabel.Location = new System.Drawing.Point(9, 106);
            this.artistLabel.Name = "artistLabel";
            this.artistLabel.Size = new System.Drawing.Size(30, 13);
            this.artistLabel.TabIndex = 6;
            this.artistLabel.Text = "Artist";
            // 
            // showLyricsCheckBox
            // 
            this.showLyricsCheckBox.AutoSize = true;
            this.showLyricsCheckBox.Checked = true;
            this.showLyricsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showLyricsCheckBox.Location = new System.Drawing.Point(12, 149);
            this.showLyricsCheckBox.Name = "showLyricsCheckBox";
            this.showLyricsCheckBox.Size = new System.Drawing.Size(79, 17);
            this.showLyricsCheckBox.TabIndex = 7;
            this.showLyricsCheckBox.Text = "Show lyrics";
            this.showLyricsCheckBox.UseVisualStyleBackColor = true;
            this.showLyricsCheckBox.CheckedChanged += new System.EventHandler(this.showLyricsCheckBox_CheckedChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyContextMenuStrip;
            this.notifyIcon.Text = "NumaLyrics";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // notifyContextMenuStrip
            // 
            this.notifyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.notifyContextMenuStrip.Name = "notifyContextMenuStrip";
            this.notifyContextMenuStrip.Size = new System.Drawing.Size(104, 48);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 171);
            this.Controls.Add(this.showLyricsCheckBox);
            this.Controls.Add(this.artistLabel);
            this.Controls.Add(this.albumLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.artistTextBox);
            this.Controls.Add(this.albumTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "NumaLyrics";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.notifyContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLrcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutNumaLyricsToolStripMenuItem;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox albumTextBox;
        private System.Windows.Forms.TextBox artistTextBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label albumLabel;
        private System.Windows.Forms.Label artistLabel;
        private System.Windows.Forms.CheckBox showLyricsCheckBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
    }
}