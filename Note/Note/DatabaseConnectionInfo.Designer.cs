namespace Note
{
    partial class DBDataForm
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelProvider = new System.Windows.Forms.Label();
            this.textBoxProvider = new System.Windows.Forms.TextBox();
            this.labelConString = new System.Windows.Forms.Label();
            this.textBoxConStr = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(144, 71);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(225, 71);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelProvider
            // 
            this.labelProvider.AutoSize = true;
            this.labelProvider.Location = new System.Drawing.Point(-1, 9);
            this.labelProvider.Name = "labelProvider";
            this.labelProvider.Size = new System.Drawing.Size(49, 13);
            this.labelProvider.TabIndex = 2;
            this.labelProvider.Text = "Provider:";
            // 
            // textBoxProvider
            // 
            this.textBoxProvider.Location = new System.Drawing.Point(92, 6);
            this.textBoxProvider.Name = "textBoxProvider";
            this.textBoxProvider.Size = new System.Drawing.Size(343, 20);
            this.textBoxProvider.TabIndex = 2;
            // 
            // labelConString
            // 
            this.labelConString.AutoSize = true;
            this.labelConString.Location = new System.Drawing.Point(-1, 35);
            this.labelConString.Name = "labelConString";
            this.labelConString.Size = new System.Drawing.Size(94, 13);
            this.labelConString.TabIndex = 2;
            this.labelConString.Text = "Connection String:";
            // 
            // textBoxConStr
            // 
            this.textBoxConStr.Location = new System.Drawing.Point(92, 32);
            this.textBoxConStr.Name = "textBoxConStr";
            this.textBoxConStr.Size = new System.Drawing.Size(343, 20);
            this.textBoxConStr.TabIndex = 3;
            // 
            // DBDataForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(448, 104);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxConStr);
            this.Controls.Add(this.labelConString);
            this.Controls.Add(this.textBoxProvider);
            this.Controls.Add(this.labelProvider);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelProvider;
        private System.Windows.Forms.TextBox textBoxProvider;
        private System.Windows.Forms.Label labelConString;
        private System.Windows.Forms.TextBox textBoxConStr;
    }
}