namespace ReelsHelper
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonVerticalReel = new System.Windows.Forms.Button();
            this.buttonGorizontalReel = new System.Windows.Forms.Button();
            this.buttonRemoveIndex = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 26);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(776, 518);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Json Object";
            // 
            // buttonVerticalReel
            // 
            this.buttonVerticalReel.Location = new System.Drawing.Point(794, 26);
            this.buttonVerticalReel.Name = "buttonVerticalReel";
            this.buttonVerticalReel.Size = new System.Drawing.Size(89, 23);
            this.buttonVerticalReel.TabIndex = 2;
            this.buttonVerticalReel.Text = "Vertical Reels";
            this.buttonVerticalReel.UseVisualStyleBackColor = true;
            this.buttonVerticalReel.Click += new System.EventHandler(this.buttonVerticalReel_Click);
            // 
            // buttonGorizontalReel
            // 
            this.buttonGorizontalReel.Location = new System.Drawing.Point(794, 55);
            this.buttonGorizontalReel.Name = "buttonGorizontalReel";
            this.buttonGorizontalReel.Size = new System.Drawing.Size(89, 23);
            this.buttonGorizontalReel.TabIndex = 2;
            this.buttonGorizontalReel.Text = "Gorizontal Reels";
            this.buttonGorizontalReel.UseVisualStyleBackColor = true;
            this.buttonGorizontalReel.Click += new System.EventHandler(this.buttonGorizontalReel_Click);
            // 
            // buttonRemoveIndex
            // 
            this.buttonRemoveIndex.Location = new System.Drawing.Point(794, 84);
            this.buttonRemoveIndex.Name = "buttonRemoveIndex";
            this.buttonRemoveIndex.Size = new System.Drawing.Size(89, 23);
            this.buttonRemoveIndex.TabIndex = 2;
            this.buttonRemoveIndex.Text = "Remove Index";
            this.buttonRemoveIndex.UseVisualStyleBackColor = true;
            this.buttonRemoveIndex.Click += new System.EventHandler(this.buttonRemoveIndex_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 581);
            this.Controls.Add(this.buttonRemoveIndex);
            this.Controls.Add(this.buttonGorizontalReel);
            this.Controls.Add(this.buttonVerticalReel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonVerticalReel;
        private System.Windows.Forms.Button buttonGorizontalReel;
        private System.Windows.Forms.Button buttonRemoveIndex;
    }
}

