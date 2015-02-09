namespace DBLinks
{
    partial class AddLinkForm
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
            this.AddLink = new System.Windows.Forms.Button();
            this.textBoxTable1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSelectField1 = new System.Windows.Forms.Button();
            this.buttonSelectTable1 = new System.Windows.Forms.Button();
            this.textBoxField1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSelectField2 = new System.Windows.Forms.Button();
            this.textBoxField2 = new System.Windows.Forms.TextBox();
            this.buttonSelectTable2 = new System.Windows.Forms.Button();
            this.textBoxTable2 = new System.Windows.Forms.TextBox();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table:";
            // 
            // AddLink
            // 
            this.AddLink.Location = new System.Drawing.Point(177, 131);
            this.AddLink.Name = "AddLink";
            this.AddLink.Size = new System.Drawing.Size(75, 23);
            this.AddLink.TabIndex = 5;
            this.AddLink.Text = "Add Link";
            this.AddLink.UseVisualStyleBackColor = true;
            this.AddLink.Click += new System.EventHandler(this.AddLink_Click);
            // 
            // textBoxTable1
            // 
            this.textBoxTable1.Location = new System.Drawing.Point(46, 22);
            this.textBoxTable1.Name = "textBoxTable1";
            this.textBoxTable1.Size = new System.Drawing.Size(148, 20);
            this.textBoxTable1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonSelectField1);
            this.groupBox1.Controls.Add(this.buttonSelectTable1);
            this.groupBox1.Controls.Add(this.textBoxField1);
            this.groupBox1.Controls.Add(this.textBoxTable1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Table 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Field:";
            // 
            // buttonSelectField1
            // 
            this.buttonSelectField1.Location = new System.Drawing.Point(200, 45);
            this.buttonSelectField1.Name = "buttonSelectField1";
            this.buttonSelectField1.Size = new System.Drawing.Size(24, 23);
            this.buttonSelectField1.TabIndex = 5;
            this.buttonSelectField1.Text = "...";
            this.buttonSelectField1.UseVisualStyleBackColor = true;
            this.buttonSelectField1.Click += new System.EventHandler(this.buttonSelectField1_Click);
            // 
            // buttonSelectTable1
            // 
            this.buttonSelectTable1.Location = new System.Drawing.Point(200, 19);
            this.buttonSelectTable1.Name = "buttonSelectTable1";
            this.buttonSelectTable1.Size = new System.Drawing.Size(24, 23);
            this.buttonSelectTable1.TabIndex = 5;
            this.buttonSelectTable1.Text = "...";
            this.buttonSelectTable1.UseVisualStyleBackColor = true;
            this.buttonSelectTable1.Click += new System.EventHandler(this.buttonSelectTable1_Click);
            // 
            // textBoxField1
            // 
            this.textBoxField1.Location = new System.Drawing.Point(46, 48);
            this.textBoxField1.Name = "textBoxField1";
            this.textBoxField1.Size = new System.Drawing.Size(148, 20);
            this.textBoxField1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.buttonSelectField2);
            this.groupBox2.Controls.Add(this.textBoxField2);
            this.groupBox2.Controls.Add(this.buttonSelectTable2);
            this.groupBox2.Controls.Add(this.textBoxTable2);
            this.groupBox2.Location = new System.Drawing.Point(274, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Table 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Field:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Table:";
            // 
            // buttonSelectField2
            // 
            this.buttonSelectField2.Location = new System.Drawing.Point(200, 46);
            this.buttonSelectField2.Name = "buttonSelectField2";
            this.buttonSelectField2.Size = new System.Drawing.Size(24, 23);
            this.buttonSelectField2.TabIndex = 5;
            this.buttonSelectField2.Text = "...";
            this.buttonSelectField2.UseVisualStyleBackColor = true;
            this.buttonSelectField2.Click += new System.EventHandler(this.buttonSelectField2_Click);
            // 
            // textBoxField2
            // 
            this.textBoxField2.Location = new System.Drawing.Point(46, 48);
            this.textBoxField2.Name = "textBoxField2";
            this.textBoxField2.Size = new System.Drawing.Size(148, 20);
            this.textBoxField2.TabIndex = 4;
            // 
            // buttonSelectTable2
            // 
            this.buttonSelectTable2.Location = new System.Drawing.Point(200, 20);
            this.buttonSelectTable2.Name = "buttonSelectTable2";
            this.buttonSelectTable2.Size = new System.Drawing.Size(24, 23);
            this.buttonSelectTable2.TabIndex = 5;
            this.buttonSelectTable2.Text = "...";
            this.buttonSelectTable2.UseVisualStyleBackColor = true;
            this.buttonSelectTable2.Click += new System.EventHandler(this.buttonSelectTable2_Click);
            // 
            // textBoxTable2
            // 
            this.textBoxTable2.Location = new System.Drawing.Point(46, 22);
            this.textBoxTable2.Name = "textBoxTable2";
            this.textBoxTable2.Size = new System.Drawing.Size(148, 20);
            this.textBoxTable2.TabIndex = 3;
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.Location = new System.Drawing.Point(274, 131);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveData.TabIndex = 6;
            this.buttonSaveData.Text = "Save data";
            this.buttonSaveData.UseVisualStyleBackColor = true;
            this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // AddLinkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 168);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonSaveData);
            this.Controls.Add(this.AddLink);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLinkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Link";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddLink;
        private System.Windows.Forms.TextBox textBoxTable1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxField1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxField2;
        private System.Windows.Forms.TextBox textBoxTable2;
        private System.Windows.Forms.Button buttonSaveData;
        private System.Windows.Forms.Button buttonSelectField1;
        private System.Windows.Forms.Button buttonSelectTable1;
        private System.Windows.Forms.Button buttonSelectField2;
        private System.Windows.Forms.Button buttonSelectTable2;
    }
}

