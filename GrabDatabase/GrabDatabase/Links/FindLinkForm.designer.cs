namespace DBLinks
{
    partial class FindLinkForm
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
            this.FindLink = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.comboBoxTable1 = new System.Windows.Forms.ComboBox();
            this.comboBoxField1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxTable2 = new System.Windows.Forms.ComboBox();
            this.comboBoxField2 = new System.Windows.Forms.ComboBox();
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
            // FindLink
            // 
            this.FindLink.Location = new System.Drawing.Point(177, 131);
            this.FindLink.Name = "FindLink";
            this.FindLink.Size = new System.Drawing.Size(75, 23);
            this.FindLink.TabIndex = 5;
            this.FindLink.Text = "Find Link";
            this.FindLink.UseVisualStyleBackColor = true;
            this.FindLink.Click += new System.EventHandler(this.FindLink_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxField1);
            this.groupBox1.Controls.Add(this.comboBoxTable1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxField2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBoxTable2);
            this.groupBox2.Controls.Add(this.label5);
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
            // comboBoxTable1
            // 
            this.comboBoxTable1.FormattingEnabled = true;
            this.comboBoxTable1.Location = new System.Drawing.Point(46, 22);
            this.comboBoxTable1.Name = "comboBoxTable1";
            this.comboBoxTable1.Size = new System.Drawing.Size(188, 21);
            this.comboBoxTable1.TabIndex = 6;
            this.comboBoxTable1.SelectedIndexChanged += new System.EventHandler(this.comboBoxTable1_SelectedIndexChanged);
            // 
            // comboBoxField1
            // 
            this.comboBoxField1.FormattingEnabled = true;
            this.comboBoxField1.Location = new System.Drawing.Point(46, 48);
            this.comboBoxField1.Name = "comboBoxField1";
            this.comboBoxField1.Size = new System.Drawing.Size(188, 21);
            this.comboBoxField1.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Table:";
            // 
            // comboBoxTable2
            // 
            this.comboBoxTable2.FormattingEnabled = true;
            this.comboBoxTable2.Location = new System.Drawing.Point(49, 22);
            this.comboBoxTable2.Name = "comboBoxTable2";
            this.comboBoxTable2.Size = new System.Drawing.Size(185, 21);
            this.comboBoxTable2.TabIndex = 6;
            this.comboBoxTable2.SelectedIndexChanged += new System.EventHandler(this.comboBoxTable2_SelectedIndexChanged);
            // 
            // comboBoxField2
            // 
            this.comboBoxField2.FormattingEnabled = true;
            this.comboBoxField2.Location = new System.Drawing.Point(49, 48);
            this.comboBoxField2.Name = "comboBoxField2";
            this.comboBoxField2.Size = new System.Drawing.Size(185, 21);
            this.comboBoxField2.TabIndex = 6;
            // 
            // FindLinkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 168);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonSaveData);
            this.Controls.Add(this.FindLink);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindLinkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Link";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FindLink;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSaveData;
        private System.Windows.Forms.ComboBox comboBoxField1;
        private System.Windows.Forms.ComboBox comboBoxTable1;
        private System.Windows.Forms.ComboBox comboBoxField2;
        private System.Windows.Forms.ComboBox comboBoxTable2;
        private System.Windows.Forms.Label label5;
    }
}

