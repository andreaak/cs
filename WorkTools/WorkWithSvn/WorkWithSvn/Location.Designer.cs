namespace WorkWithSvn
{
    partial class Location
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
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.buttonGetLocation = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRepository = new System.Windows.Forms.TextBox();
            this.buttonGetRepository = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTargetLocation = new System.Windows.Forms.TextBox();
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
            // buttonAdd
            // 
            this.buttonAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAdd.Location = new System.Drawing.Point(259, 135);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Location = new System.Drawing.Point(96, 38);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(454, 20);
            this.textBoxLocation.TabIndex = 1;
            this.textBoxLocation.TextChanged += new System.EventHandler(this.textBoxRepository_TextChanged);
            // 
            // buttonGetLocation
            // 
            this.buttonGetLocation.Location = new System.Drawing.Point(556, 36);
            this.buttonGetLocation.Name = "buttonGetLocation";
            this.buttonGetLocation.Size = new System.Drawing.Size(25, 23);
            this.buttonGetLocation.TabIndex = 3;
            this.buttonGetLocation.Text = "...";
            this.buttonGetLocation.UseVisualStyleBackColor = true;
            this.buttonGetLocation.Click += new System.EventHandler(this.buttonGetLocation_Click);
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
            this.textBoxRepository.TextChanged += new System.EventHandler(this.textBoxRepository_TextChanged);
            // 
            // buttonGetRepository
            // 
            this.buttonGetRepository.Location = new System.Drawing.Point(556, 11);
            this.buttonGetRepository.Name = "buttonGetRepository";
            this.buttonGetRepository.Size = new System.Drawing.Size(25, 23);
            this.buttonGetRepository.TabIndex = 3;
            this.buttonGetRepository.Text = "...";
            this.buttonGetRepository.UseVisualStyleBackColor = true;
            this.buttonGetRepository.Click += new System.EventHandler(this.buttonGetRepository_Click);
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
            this.textBoxTargetLocation.TabIndex = 1;
            // 
            // Location
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 170);
            this.Controls.Add(this.buttonGetRepository);
            this.Controls.Add(this.buttonGetLocation);
            this.Controls.Add(this.textBoxRepository);
            this.Controls.Add(this.textBoxTargetLocation);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Location";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Location";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Button buttonGetLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRepository;
        private System.Windows.Forms.Button buttonGetRepository;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTargetLocation;
    }
}