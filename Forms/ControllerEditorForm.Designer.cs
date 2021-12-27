
namespace NumaLyrics.Forms
{
    partial class ControllerEditorForm
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
            this.buttonListBox = new System.Windows.Forms.ListBox();
            this.buttonInfoBox = new System.Windows.Forms.GroupBox();
            this.doubleClickActionLabel = new System.Windows.Forms.Label();
            this.clickActionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.editButton = new System.Windows.Forms.Button();
            this.deviceNameLabel = new System.Windows.Forms.Label();
            this.buttonInfoBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonListBox
            // 
            this.buttonListBox.FormattingEnabled = true;
            this.buttonListBox.Location = new System.Drawing.Point(12, 25);
            this.buttonListBox.Name = "buttonListBox";
            this.buttonListBox.Size = new System.Drawing.Size(133, 199);
            this.buttonListBox.TabIndex = 0;
            this.buttonListBox.SelectedIndexChanged += new System.EventHandler(this.buttonListBox_SelectedIndexChanged);
            // 
            // buttonInfoBox
            // 
            this.buttonInfoBox.Controls.Add(this.doubleClickActionLabel);
            this.buttonInfoBox.Controls.Add(this.clickActionLabel);
            this.buttonInfoBox.Controls.Add(this.label2);
            this.buttonInfoBox.Controls.Add(this.label1);
            this.buttonInfoBox.Location = new System.Drawing.Point(151, 25);
            this.buttonInfoBox.Name = "buttonInfoBox";
            this.buttonInfoBox.Size = new System.Drawing.Size(402, 59);
            this.buttonInfoBox.TabIndex = 1;
            this.buttonInfoBox.TabStop = false;
            this.buttonInfoBox.Text = "Button Info";
            // 
            // doubleClickActionLabel
            // 
            this.doubleClickActionLabel.AutoSize = true;
            this.doubleClickActionLabel.Location = new System.Drawing.Point(75, 36);
            this.doubleClickActionLabel.Name = "doubleClickActionLabel";
            this.doubleClickActionLabel.Size = new System.Drawing.Size(40, 13);
            this.doubleClickActionLabel.TabIndex = 4;
            this.doubleClickActionLabel.Text = "dummy";
            // 
            // clickActionLabel
            // 
            this.clickActionLabel.AutoSize = true;
            this.clickActionLabel.Location = new System.Drawing.Point(75, 16);
            this.clickActionLabel.Name = "clickActionLabel";
            this.clickActionLabel.Size = new System.Drawing.Size(40, 13);
            this.clickActionLabel.TabIndex = 3;
            this.clickActionLabel.Text = "dummy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Double Click:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Click:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(478, 201);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deviceNameLabel
            // 
            this.deviceNameLabel.AutoSize = true;
            this.deviceNameLabel.Location = new System.Drawing.Point(9, 6);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(40, 13);
            this.deviceNameLabel.TabIndex = 3;
            this.deviceNameLabel.Text = "dummy";
            // 
            // ControllerEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 235);
            this.Controls.Add(this.deviceNameLabel);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.buttonInfoBox);
            this.Controls.Add(this.buttonListBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControllerEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Controller Editor";
            this.Load += new System.EventHandler(this.ControllerEditorForm_Load);
            this.buttonInfoBox.ResumeLayout(false);
            this.buttonInfoBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox buttonListBox;
        private System.Windows.Forms.GroupBox buttonInfoBox;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Label deviceNameLabel;
        private System.Windows.Forms.Label doubleClickActionLabel;
        private System.Windows.Forms.Label clickActionLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}