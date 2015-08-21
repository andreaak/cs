namespace Note.ExportData
{
    partial class ExportOptions
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
            this.comboBoxExportType = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxIndexNumeration = new System.Windows.Forms.CheckBox();
            this.checkBoxThroughNumeration = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateFolders = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type:";
            // 
            // comboBoxExportType
            // 
            this.comboBoxExportType.FormattingEnabled = true;
            this.comboBoxExportType.Location = new System.Drawing.Point(54, 24);
            this.comboBoxExportType.Name = "comboBoxExportType";
            this.comboBoxExportType.Size = new System.Drawing.Size(236, 21);
            this.comboBoxExportType.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(73, 139);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(154, 139);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxIndexNumeration
            // 
            this.checkBoxIndexNumeration.AutoSize = true;
            this.checkBoxIndexNumeration.Location = new System.Drawing.Point(16, 61);
            this.checkBoxIndexNumeration.Name = "checkBoxIndexNumeration";
            this.checkBoxIndexNumeration.Size = new System.Drawing.Size(106, 17);
            this.checkBoxIndexNumeration.TabIndex = 3;
            this.checkBoxIndexNumeration.Text = "IndexNumeration";
            this.checkBoxIndexNumeration.UseVisualStyleBackColor = true;
            this.checkBoxIndexNumeration.CheckedChanged += new System.EventHandler(this.checkBoxIndexNumeration_CheckedChanged);
            // 
            // checkBoxThroughNumeration
            // 
            this.checkBoxThroughNumeration.AutoSize = true;
            this.checkBoxThroughNumeration.Enabled = false;
            this.checkBoxThroughNumeration.Location = new System.Drawing.Point(16, 84);
            this.checkBoxThroughNumeration.Name = "checkBoxThroughNumeration";
            this.checkBoxThroughNumeration.Size = new System.Drawing.Size(123, 17);
            this.checkBoxThroughNumeration.TabIndex = 3;
            this.checkBoxThroughNumeration.Text = "Through Numeration";
            this.checkBoxThroughNumeration.UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateFolders
            // 
            this.checkBoxCreateFolders.AutoSize = true;
            this.checkBoxCreateFolders.Checked = true;
            this.checkBoxCreateFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateFolders.Location = new System.Drawing.Point(16, 107);
            this.checkBoxCreateFolders.Name = "checkBoxCreateFolders";
            this.checkBoxCreateFolders.Size = new System.Drawing.Size(94, 17);
            this.checkBoxCreateFolders.TabIndex = 3;
            this.checkBoxCreateFolders.Text = "Create Folders";
            this.checkBoxCreateFolders.UseVisualStyleBackColor = true;
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 174);
            this.Controls.Add(this.checkBoxCreateFolders);
            this.Controls.Add(this.checkBoxThroughNumeration);
            this.Controls.Add(this.checkBoxIndexNumeration);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxExportType);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Export";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Export_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxExportType;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxIndexNumeration;
        private System.Windows.Forms.CheckBox checkBoxThroughNumeration;
        private System.Windows.Forms.CheckBox checkBoxCreateFolders;
    }
}