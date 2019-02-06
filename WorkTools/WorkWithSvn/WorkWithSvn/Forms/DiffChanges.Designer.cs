namespace WorkWithSvn
{
    partial class DiffChanges
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
            this.labelWorkingCopy = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRemoteRepository = new System.Windows.Forms.TextBox();
            this.textBoxFiles = new System.Windows.Forms.TextBox();
            this.buttonDiff = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWorkingCopy = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelWorkingCopy
            // 
            this.labelWorkingCopy.AutoSize = true;
            this.labelWorkingCopy.Location = new System.Drawing.Point(9, 60);
            this.labelWorkingCopy.Name = "labelWorkingCopy";
            this.labelWorkingCopy.Size = new System.Drawing.Size(99, 13);
            this.labelWorkingCopy.TabIndex = 0;
            this.labelWorkingCopy.Text = "Удаленная копия:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Файлы:";
            // 
            // textBoxRemoteRepository
            // 
            this.textBoxRemoteRepository.Location = new System.Drawing.Point(106, 57);
            this.textBoxRemoteRepository.Name = "textBoxRemoteRepository";
            this.textBoxRemoteRepository.Size = new System.Drawing.Size(582, 20);
            this.textBoxRemoteRepository.TabIndex = 1;
            // 
            // textBoxFiles
            // 
            this.textBoxFiles.Location = new System.Drawing.Point(106, 88);
            this.textBoxFiles.Multiline = true;
            this.textBoxFiles.Name = "textBoxFiles";
            this.textBoxFiles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxFiles.Size = new System.Drawing.Size(582, 357);
            this.textBoxFiles.TabIndex = 1;
            // 
            // buttonDiff
            // 
            this.buttonDiff.Location = new System.Drawing.Point(12, 422);
            this.buttonDiff.Name = "buttonDiff";
            this.buttonDiff.Size = new System.Drawing.Size(75, 23);
            this.buttonDiff.TabIndex = 2;
            this.buttonDiff.Text = "Diff";
            this.buttonDiff.UseVisualStyleBackColor = true;
            this.buttonDiff.Click += new System.EventHandler(this.buttonDiff_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Рабочая копия:";
            // 
            // textBoxWorkingCopy
            // 
            this.textBoxWorkingCopy.Location = new System.Drawing.Point(106, 31);
            this.textBoxWorkingCopy.Name = "textBoxWorkingCopy";
            this.textBoxWorkingCopy.Size = new System.Drawing.Size(582, 20);
            this.textBoxWorkingCopy.TabIndex = 1;
            // 
            // DiffChanges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 457);
            this.Controls.Add(this.buttonDiff);
            this.Controls.Add(this.textBoxFiles);
            this.Controls.Add(this.textBoxWorkingCopy);
            this.Controls.Add(this.textBoxRemoteRepository);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelWorkingCopy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiffChanges";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DiffChanges";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWorkingCopy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRemoteRepository;
        private System.Windows.Forms.TextBox textBoxFiles;
        private System.Windows.Forms.Button buttonDiff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxWorkingCopy;
    }
}