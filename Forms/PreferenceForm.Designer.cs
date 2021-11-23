
namespace NumaLyrics.Forms
{
    partial class PreferenceForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.fontTabPage = new System.Windows.Forms.TabPage();
            this.displayTabPage = new System.Windows.Forms.TabPage();
            this.lyricsTabPage = new System.Windows.Forms.TabPage();
            this.fontColorPicBox = new System.Windows.Forms.PictureBox();
            this.fontOutlineColorPicBox = new System.Windows.Forms.PictureBox();
            this.fontColorLabel = new System.Windows.Forms.Label();
            this.fontOutlineColorLabel = new System.Windows.Forms.Label();
            this.fontOutlineWidth = new System.Windows.Forms.NumericUpDown();
            this.fontOutlineWidthLabel = new System.Windows.Forms.Label();
            this.fontSelectButton = new System.Windows.Forms.Button();
            this.displayLabel = new System.Windows.Forms.Label();
            this.displayIndex = new System.Windows.Forms.NumericUpDown();
            this.positionXLabel = new System.Windows.Forms.Label();
            this.positionYLabel = new System.Windows.Forms.Label();
            this.displayXPos = new System.Windows.Forms.NumericUpDown();
            this.displayYPos = new System.Windows.Forms.NumericUpDown();
            this.timeOffsetLabel = new System.Windows.Forms.Label();
            this.timeOffset = new System.Windows.Forms.NumericUpDown();
            this.tabControl.SuspendLayout();
            this.fontTabPage.SuspendLayout();
            this.displayTabPage.SuspendLayout();
            this.lyricsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontColorPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontOutlineColorPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontOutlineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayXPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayYPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.fontTabPage);
            this.tabControl.Controls.Add(this.displayTabPage);
            this.tabControl.Controls.Add(this.lyricsTabPage);
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(282, 83);
            this.tabControl.TabIndex = 0;
            // 
            // fontTabPage
            // 
            this.fontTabPage.Controls.Add(this.fontSelectButton);
            this.fontTabPage.Controls.Add(this.fontOutlineWidthLabel);
            this.fontTabPage.Controls.Add(this.fontOutlineWidth);
            this.fontTabPage.Controls.Add(this.fontOutlineColorLabel);
            this.fontTabPage.Controls.Add(this.fontColorLabel);
            this.fontTabPage.Controls.Add(this.fontOutlineColorPicBox);
            this.fontTabPage.Controls.Add(this.fontColorPicBox);
            this.fontTabPage.Location = new System.Drawing.Point(4, 22);
            this.fontTabPage.Name = "fontTabPage";
            this.fontTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.fontTabPage.Size = new System.Drawing.Size(274, 57);
            this.fontTabPage.TabIndex = 0;
            this.fontTabPage.Text = "Font";
            this.fontTabPage.UseVisualStyleBackColor = true;
            // 
            // displayTabPage
            // 
            this.displayTabPage.Controls.Add(this.displayYPos);
            this.displayTabPage.Controls.Add(this.displayXPos);
            this.displayTabPage.Controls.Add(this.positionYLabel);
            this.displayTabPage.Controls.Add(this.positionXLabel);
            this.displayTabPage.Controls.Add(this.displayIndex);
            this.displayTabPage.Controls.Add(this.displayLabel);
            this.displayTabPage.Location = new System.Drawing.Point(4, 22);
            this.displayTabPage.Name = "displayTabPage";
            this.displayTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.displayTabPage.Size = new System.Drawing.Size(274, 57);
            this.displayTabPage.TabIndex = 1;
            this.displayTabPage.Text = "Display";
            this.displayTabPage.UseVisualStyleBackColor = true;
            // 
            // lyricsTabPage
            // 
            this.lyricsTabPage.Controls.Add(this.timeOffset);
            this.lyricsTabPage.Controls.Add(this.timeOffsetLabel);
            this.lyricsTabPage.Location = new System.Drawing.Point(4, 22);
            this.lyricsTabPage.Name = "lyricsTabPage";
            this.lyricsTabPage.Size = new System.Drawing.Size(274, 57);
            this.lyricsTabPage.TabIndex = 2;
            this.lyricsTabPage.Text = "Lyrics";
            this.lyricsTabPage.UseVisualStyleBackColor = true;
            // 
            // fontColorPicBox
            // 
            this.fontColorPicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fontColorPicBox.Location = new System.Drawing.Point(8, 21);
            this.fontColorPicBox.Name = "fontColorPicBox";
            this.fontColorPicBox.Size = new System.Drawing.Size(25, 25);
            this.fontColorPicBox.TabIndex = 0;
            this.fontColorPicBox.TabStop = false;
            this.fontColorPicBox.Click += new System.EventHandler(this.fontColorPicBox_Click);
            // 
            // fontOutlineColorPicBox
            // 
            this.fontOutlineColorPicBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fontOutlineColorPicBox.Location = new System.Drawing.Point(47, 21);
            this.fontOutlineColorPicBox.Name = "fontOutlineColorPicBox";
            this.fontOutlineColorPicBox.Size = new System.Drawing.Size(25, 25);
            this.fontOutlineColorPicBox.TabIndex = 1;
            this.fontOutlineColorPicBox.TabStop = false;
            this.fontOutlineColorPicBox.Click += new System.EventHandler(this.fontOutlineColorPicBox_Click);
            // 
            // fontColorLabel
            // 
            this.fontColorLabel.AutoSize = true;
            this.fontColorLabel.Location = new System.Drawing.Point(5, 5);
            this.fontColorLabel.Name = "fontColorLabel";
            this.fontColorLabel.Size = new System.Drawing.Size(31, 13);
            this.fontColorLabel.TabIndex = 2;
            this.fontColorLabel.Text = "Color";
            // 
            // fontOutlineColorLabel
            // 
            this.fontOutlineColorLabel.AutoSize = true;
            this.fontOutlineColorLabel.Location = new System.Drawing.Point(44, 5);
            this.fontOutlineColorLabel.Name = "fontOutlineColorLabel";
            this.fontOutlineColorLabel.Size = new System.Drawing.Size(66, 13);
            this.fontOutlineColorLabel.TabIndex = 3;
            this.fontOutlineColorLabel.Text = "Outline color";
            // 
            // fontOutlineWidth
            // 
            this.fontOutlineWidth.DecimalPlaces = 1;
            this.fontOutlineWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.fontOutlineWidth.Location = new System.Drawing.Point(119, 21);
            this.fontOutlineWidth.Name = "fontOutlineWidth";
            this.fontOutlineWidth.Size = new System.Drawing.Size(46, 20);
            this.fontOutlineWidth.TabIndex = 4;
            // 
            // fontOutlineWidthLabel
            // 
            this.fontOutlineWidthLabel.AutoSize = true;
            this.fontOutlineWidthLabel.Location = new System.Drawing.Point(116, 5);
            this.fontOutlineWidthLabel.Name = "fontOutlineWidthLabel";
            this.fontOutlineWidthLabel.Size = new System.Drawing.Size(68, 13);
            this.fontOutlineWidthLabel.TabIndex = 5;
            this.fontOutlineWidthLabel.Text = "Outline width";
            // 
            // fontSelectButton
            // 
            this.fontSelectButton.Location = new System.Drawing.Point(190, 18);
            this.fontSelectButton.Name = "fontSelectButton";
            this.fontSelectButton.Size = new System.Drawing.Size(75, 23);
            this.fontSelectButton.TabIndex = 6;
            this.fontSelectButton.Text = "Font Select";
            this.fontSelectButton.UseVisualStyleBackColor = true;
            this.fontSelectButton.Click += new System.EventHandler(this.fontSelectButton_Click);
            // 
            // displayLabel
            // 
            this.displayLabel.AutoSize = true;
            this.displayLabel.Location = new System.Drawing.Point(5, 5);
            this.displayLabel.Name = "displayLabel";
            this.displayLabel.Size = new System.Drawing.Size(79, 13);
            this.displayLabel.TabIndex = 0;
            this.displayLabel.Text = "Display number";
            // 
            // displayIndex
            // 
            this.displayIndex.Location = new System.Drawing.Point(8, 21);
            this.displayIndex.Name = "displayIndex";
            this.displayIndex.Size = new System.Drawing.Size(48, 20);
            this.displayIndex.TabIndex = 1;
            // 
            // positionXLabel
            // 
            this.positionXLabel.AutoSize = true;
            this.positionXLabel.Location = new System.Drawing.Point(90, 5);
            this.positionXLabel.Name = "positionXLabel";
            this.positionXLabel.Size = new System.Drawing.Size(54, 13);
            this.positionXLabel.TabIndex = 2;
            this.positionXLabel.Text = "Position X";
            // 
            // positionYLabel
            // 
            this.positionYLabel.AutoSize = true;
            this.positionYLabel.Location = new System.Drawing.Point(150, 5);
            this.positionYLabel.Name = "positionYLabel";
            this.positionYLabel.Size = new System.Drawing.Size(51, 13);
            this.positionYLabel.TabIndex = 3;
            this.positionYLabel.Text = "PositionY";
            // 
            // displayXPos
            // 
            this.displayXPos.DecimalPlaces = 3;
            this.displayXPos.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.displayXPos.Location = new System.Drawing.Point(93, 21);
            this.displayXPos.Name = "displayXPos";
            this.displayXPos.Size = new System.Drawing.Size(51, 20);
            this.displayXPos.TabIndex = 4;
            // 
            // displayYPos
            // 
            this.displayYPos.DecimalPlaces = 3;
            this.displayYPos.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.displayYPos.Location = new System.Drawing.Point(153, 21);
            this.displayYPos.Name = "displayYPos";
            this.displayYPos.Size = new System.Drawing.Size(51, 20);
            this.displayYPos.TabIndex = 5;
            // 
            // timeOffsetLabel
            // 
            this.timeOffsetLabel.AutoSize = true;
            this.timeOffsetLabel.Location = new System.Drawing.Point(5, 5);
            this.timeOffsetLabel.Name = "timeOffsetLabel";
            this.timeOffsetLabel.Size = new System.Drawing.Size(81, 13);
            this.timeOffsetLabel.TabIndex = 0;
            this.timeOffsetLabel.Text = "Time offset (ms)";
            // 
            // timeOffset
            // 
            this.timeOffset.Location = new System.Drawing.Point(8, 21);
            this.timeOffset.Name = "timeOffset";
            this.timeOffset.Size = new System.Drawing.Size(78, 20);
            this.timeOffset.TabIndex = 1;
            // 
            // PreferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 91);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferenceForm";
            this.Text = "Preference";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreferenceForm_FormClosing);
            this.Load += new System.EventHandler(this.PreferenceForm_Load);
            this.tabControl.ResumeLayout(false);
            this.fontTabPage.ResumeLayout(false);
            this.fontTabPage.PerformLayout();
            this.displayTabPage.ResumeLayout(false);
            this.displayTabPage.PerformLayout();
            this.lyricsTabPage.ResumeLayout(false);
            this.lyricsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontColorPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontOutlineColorPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontOutlineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayXPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayYPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeOffset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage fontTabPage;
        private System.Windows.Forms.TabPage displayTabPage;
        private System.Windows.Forms.PictureBox fontOutlineColorPicBox;
        private System.Windows.Forms.PictureBox fontColorPicBox;
        private System.Windows.Forms.TabPage lyricsTabPage;
        private System.Windows.Forms.Label fontColorLabel;
        private System.Windows.Forms.NumericUpDown fontOutlineWidth;
        private System.Windows.Forms.Label fontOutlineColorLabel;
        private System.Windows.Forms.Label fontOutlineWidthLabel;
        private System.Windows.Forms.Button fontSelectButton;
        private System.Windows.Forms.Label displayLabel;
        private System.Windows.Forms.NumericUpDown displayIndex;
        private System.Windows.Forms.Label positionYLabel;
        private System.Windows.Forms.Label positionXLabel;
        private System.Windows.Forms.NumericUpDown displayYPos;
        private System.Windows.Forms.NumericUpDown displayXPos;
        private System.Windows.Forms.NumericUpDown timeOffset;
        private System.Windows.Forms.Label timeOffsetLabel;
    }
}