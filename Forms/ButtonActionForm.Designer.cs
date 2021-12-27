
namespace NumaLyrics.Forms
{
    partial class ButtonActionForm
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
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.clickTextBox = new System.Windows.Forms.TextBox();
            this.doubleClickTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(244, 73);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(325, 73);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Click:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // clickTextBox
            // 
            this.clickTextBox.Location = new System.Drawing.Point(81, 10);
            this.clickTextBox.Name = "clickTextBox";
            this.clickTextBox.Size = new System.Drawing.Size(319, 20);
            this.clickTextBox.TabIndex = 5;
            // 
            // doubleClickTextBox
            // 
            this.doubleClickTextBox.Location = new System.Drawing.Point(81, 37);
            this.doubleClickTextBox.Name = "doubleClickTextBox";
            this.doubleClickTextBox.Size = new System.Drawing.Size(319, 20);
            this.doubleClickTextBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Double Click:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ButtonActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 108);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.doubleClickTextBox);
            this.Controls.Add(this.clickTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ButtonActionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ButtonActionForm";
            this.Load += new System.EventHandler(this.ButtonActionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox clickTextBox;
        private System.Windows.Forms.TextBox doubleClickTextBox;
        private System.Windows.Forms.Label label2;
    }
}