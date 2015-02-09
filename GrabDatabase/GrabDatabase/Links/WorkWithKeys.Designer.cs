namespace GrabDatabase.Links
{
    partial class WorkWithKeys
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
            this.buttonIndexColumns = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIndexColumns = new System.Windows.Forms.TextBox();
            this.textBoxFK = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonFK = new System.Windows.Forms.Button();
            this.textBoxFKColumns = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonFKColumns = new System.Windows.Forms.Button();
            this.buttonCreateLinks = new System.Windows.Forms.Button();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonIndexColumns
            // 
            this.buttonIndexColumns.Location = new System.Drawing.Point(453, 4);
            this.buttonIndexColumns.Name = "buttonIndexColumns";
            this.buttonIndexColumns.Size = new System.Drawing.Size(27, 23);
            this.buttonIndexColumns.TabIndex = 2;
            this.buttonIndexColumns.Text = "...";
            this.buttonIndexColumns.UseVisualStyleBackColor = true;
            this.buttonIndexColumns.Click += new System.EventHandler(this.buttonIndexColumns_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Index Columns:";
            // 
            // textBoxIndexColumns
            // 
            this.textBoxIndexColumns.Location = new System.Drawing.Point(97, 6);
            this.textBoxIndexColumns.Name = "textBoxIndexColumns";
            this.textBoxIndexColumns.Size = new System.Drawing.Size(350, 20);
            this.textBoxIndexColumns.TabIndex = 1;
            // 
            // textBoxFK
            // 
            this.textBoxFK.Location = new System.Drawing.Point(97, 32);
            this.textBoxFK.Name = "textBoxFK";
            this.textBoxFK.Size = new System.Drawing.Size(350, 20);
            this.textBoxFK.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Foreign Keys:";
            // 
            // buttonFK
            // 
            this.buttonFK.Location = new System.Drawing.Point(453, 30);
            this.buttonFK.Name = "buttonFK";
            this.buttonFK.Size = new System.Drawing.Size(27, 23);
            this.buttonFK.TabIndex = 4;
            this.buttonFK.Text = "...";
            this.buttonFK.UseVisualStyleBackColor = true;
            this.buttonFK.Click += new System.EventHandler(this.buttonFK_Click);
            // 
            // textBoxFKColumns
            // 
            this.textBoxFKColumns.Location = new System.Drawing.Point(97, 58);
            this.textBoxFKColumns.Name = "textBoxFKColumns";
            this.textBoxFKColumns.Size = new System.Drawing.Size(350, 20);
            this.textBoxFKColumns.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "FK Columns:";
            // 
            // buttonFKColumns
            // 
            this.buttonFKColumns.Location = new System.Drawing.Point(453, 56);
            this.buttonFKColumns.Name = "buttonFKColumns";
            this.buttonFKColumns.Size = new System.Drawing.Size(27, 23);
            this.buttonFKColumns.TabIndex = 6;
            this.buttonFKColumns.Text = "...";
            this.buttonFKColumns.UseVisualStyleBackColor = true;
            this.buttonFKColumns.Click += new System.EventHandler(this.buttonFKColumns_Click);
            // 
            // buttonCreateLinks
            // 
            this.buttonCreateLinks.Location = new System.Drawing.Point(174, 110);
            this.buttonCreateLinks.Name = "buttonCreateLinks";
            this.buttonCreateLinks.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateLinks.TabIndex = 7;
            this.buttonCreateLinks.Text = "Create Links";
            this.buttonCreateLinks.UseVisualStyleBackColor = true;
            this.buttonCreateLinks.Click += new System.EventHandler(this.buttonCreateLinks_Click);
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.Location = new System.Drawing.Point(255, 110);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveData.TabIndex = 8;
            this.buttonSaveData.Text = "Save data";
            this.buttonSaveData.UseVisualStyleBackColor = true;
            this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // WorkWithKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 160);
            this.Controls.Add(this.buttonSaveData);
            this.Controls.Add(this.buttonCreateLinks);
            this.Controls.Add(this.buttonFKColumns);
            this.Controls.Add(this.buttonFK);
            this.Controls.Add(this.buttonIndexColumns);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFKColumns);
            this.Controls.Add(this.textBoxFK);
            this.Controls.Add(this.textBoxIndexColumns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkWithKeys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Work With Keys";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonIndexColumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIndexColumns;
        private System.Windows.Forms.TextBox textBoxFK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonFK;
        private System.Windows.Forms.TextBox textBoxFKColumns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonFKColumns;
        private System.Windows.Forms.Button buttonCreateLinks;
        private System.Windows.Forms.Button buttonSaveData;
    }
}