namespace WorkWithSvn
{
    partial class SwitchLocation
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSwitch = new System.Windows.Forms.Button();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRepository = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTargetLocation = new System.Windows.Forms.TextBox();
            this.checkBoxRestoreFile = new System.Windows.Forms.CheckBox();
            this.checkBoxBackUpFile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base Location:";
            // 
            // buttonSwitch
            // 
            this.buttonSwitch.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSwitch.Location = new System.Drawing.Point(259, 135);
            this.buttonSwitch.Name = "buttonSwitch";
            this.buttonSwitch.Size = new System.Drawing.Size(75, 23);
            this.buttonSwitch.TabIndex = 7;
            this.buttonSwitch.Text = "Switch";
            this.buttonSwitch.UseVisualStyleBackColor = true;
            this.buttonSwitch.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Location = new System.Drawing.Point(96, 38);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(454, 20);
            this.textBoxLocation.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Repository:";
            // 
            // textBoxRepository
            // 
            this.textBoxRepository.Location = new System.Drawing.Point(96, 12);
            this.textBoxRepository.Name = "textBoxRepository";
            this.textBoxRepository.Size = new System.Drawing.Size(454, 20);
            this.textBoxRepository.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Target Location:";
            // 
            // textBoxTargetLocation
            // 
            this.textBoxTargetLocation.Location = new System.Drawing.Point(96, 64);
            this.textBoxTargetLocation.Name = "textBoxTargetLocation";
            this.textBoxTargetLocation.Size = new System.Drawing.Size(454, 20);
            this.textBoxTargetLocation.TabIndex = 5;
            // 
            // checkBoxRestoreFile
            // 
            this.checkBoxRestoreFile.AutoSize = true;
            this.checkBoxRestoreFile.Location = new System.Drawing.Point(8, 130);
            this.checkBoxRestoreFile.Name = "checkBoxRestoreFile";
            this.checkBoxRestoreFile.Size = new System.Drawing.Size(82, 17);
            this.checkBoxRestoreFile.TabIndex = 6;
            this.checkBoxRestoreFile.Text = "Restore File";
            this.checkBoxRestoreFile.UseVisualStyleBackColor = true;
            // 
            // checkBoxBackUpFile
            // 
            this.checkBoxBackUpFile.AutoSize = true;
            this.checkBoxBackUpFile.Location = new System.Drawing.Point(8, 107);
            this.checkBoxBackUpFile.Name = "checkBoxBackUpFile";
            this.checkBoxBackUpFile.Size = new System.Drawing.Size(82, 17);
            this.checkBoxBackUpFile.TabIndex = 6;
            this.checkBoxBackUpFile.Text = "Backup File";
            this.checkBoxBackUpFile.UseVisualStyleBackColor = true;
            // 
            // SwitchLocation
            // 
            this.AcceptButton = this.buttonSwitch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 170);
            this.Controls.Add(this.checkBoxBackUpFile);
            this.Controls.Add(this.checkBoxRestoreFile);
            this.Controls.Add(this.textBoxRepository);
            this.Controls.Add(this.textBoxTargetLocation);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSwitch);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SwitchLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Switch Location";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSwitch;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRepository;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTargetLocation;
        private System.Windows.Forms.CheckBox checkBoxRestoreFile;
        private System.Windows.Forms.CheckBox checkBoxBackUpFile;
    }
}