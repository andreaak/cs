namespace Assist
{
    partial class RTFTextControlOptionPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.checkBoxHighlightText = new System.Windows.Forms.CheckBox();
            this.checkBoxSetTextInControl = new System.Windows.Forms.CheckBox();
            this.comboBoxGuidR = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFontR = new System.Windows.Forms.TextBox();
            this.buttonChooseFontR = new System.Windows.Forms.Button();
            this.checkBoxSetLogger = new System.Windows.Forms.CheckBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.textBoxLoggerPath = new System.Windows.Forms.TextBox();
            this.buttonChoosePath = new System.Windows.Forms.Button();
            this.numericUpDownSubRow = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxGuidL = new System.Windows.Forms.ComboBox();
            this.textBoxFontL = new System.Windows.Forms.TextBox();
            this.buttonChooseFontL = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.FuncName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label1.Location = new System.Drawing.Point(4, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Font R:";
            // 
            // checkBoxHighlightText
            // 
            this.checkBoxHighlightText.AutoSize = true;
            this.checkBoxHighlightText.Location = new System.Drawing.Point(3, 3);
            this.checkBoxHighlightText.Name = "checkBoxHighlightText";
            this.checkBoxHighlightText.Size = new System.Drawing.Size(91, 17);
            this.checkBoxHighlightText.TabIndex = 1;
            this.checkBoxHighlightText.Text = "Highlight Text";
            this.checkBoxHighlightText.UseVisualStyleBackColor = true;
            this.checkBoxHighlightText.CheckedChanged += new System.EventHandler(this.checkBoxSetTextInControl_CheckedChanged);
            // 
            // checkBoxSetTextInControl
            // 
            this.checkBoxSetTextInControl.AutoSize = true;
            this.checkBoxSetTextInControl.Location = new System.Drawing.Point(100, 3);
            this.checkBoxSetTextInControl.Name = "checkBoxSetTextInControl";
            this.checkBoxSetTextInControl.Size = new System.Drawing.Size(114, 17);
            this.checkBoxSetTextInControl.TabIndex = 1;
            this.checkBoxSetTextInControl.Text = "Set Text In Control";
            this.checkBoxSetTextInControl.UseVisualStyleBackColor = true;
            this.checkBoxSetTextInControl.CheckedChanged += new System.EventHandler(this.checkBoxSetTextInControl_CheckedChanged);
            // 
            // comboBoxGuidR
            // 
            this.comboBoxGuidR.FormattingEnabled = true;
            this.comboBoxGuidR.Location = new System.Drawing.Point(54, 78);
            this.comboBoxGuidR.Name = "comboBoxGuidR";
            this.comboBoxGuidR.Size = new System.Drawing.Size(335, 21);
            this.comboBoxGuidR.TabIndex = 2;
            this.comboBoxGuidR.SelectedIndexChanged += new System.EventHandler(this.comboBoxGuidR_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Location = new System.Drawing.Point(4, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Subrow:";
            // 
            // textBoxFontR
            // 
            this.textBoxFontR.Enabled = false;
            this.textBoxFontR.Location = new System.Drawing.Point(54, 105);
            this.textBoxFontR.Name = "textBoxFontR";
            this.textBoxFontR.Size = new System.Drawing.Size(311, 20);
            this.textBoxFontR.TabIndex = 3;
            // 
            // buttonChooseFontR
            // 
            this.buttonChooseFontR.Location = new System.Drawing.Point(365, 105);
            this.buttonChooseFontR.Name = "buttonChooseFontR";
            this.buttonChooseFontR.Size = new System.Drawing.Size(24, 20);
            this.buttonChooseFontR.TabIndex = 4;
            this.buttonChooseFontR.Text = "...";
            this.buttonChooseFontR.UseVisualStyleBackColor = true;
            this.buttonChooseFontR.Click += new System.EventHandler(this.buttonChooseFontR_Click);
            // 
            // checkBoxSetLogger
            // 
            this.checkBoxSetLogger.AutoSize = true;
            this.checkBoxSetLogger.Location = new System.Drawing.Point(3, 28);
            this.checkBoxSetLogger.Name = "checkBoxSetLogger";
            this.checkBoxSetLogger.Size = new System.Drawing.Size(59, 17);
            this.checkBoxSetLogger.TabIndex = 1;
            this.checkBoxSetLogger.Text = "Logger";
            this.checkBoxSetLogger.UseVisualStyleBackColor = true;
            this.checkBoxSetLogger.CheckedChanged += new System.EventHandler(this.checkBoxSetLogger_CheckedChanged);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelPath.Location = new System.Drawing.Point(62, 29);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(32, 13);
            this.labelPath.TabIndex = 0;
            this.labelPath.Text = "Path:";
            // 
            // textBoxLoggerPath
            // 
            this.textBoxLoggerPath.AcceptsReturn = true;
            this.textBoxLoggerPath.Enabled = false;
            this.textBoxLoggerPath.Location = new System.Drawing.Point(100, 26);
            this.textBoxLoggerPath.Name = "textBoxLoggerPath";
            this.textBoxLoggerPath.Size = new System.Drawing.Size(265, 20);
            this.textBoxLoggerPath.TabIndex = 3;
            this.textBoxLoggerPath.TextChanged += new System.EventHandler(this.textBoxLoggerPath_TextChanged);
            // 
            // buttonChoosePath
            // 
            this.buttonChoosePath.Location = new System.Drawing.Point(365, 26);
            this.buttonChoosePath.Name = "buttonChoosePath";
            this.buttonChoosePath.Size = new System.Drawing.Size(24, 20);
            this.buttonChoosePath.TabIndex = 4;
            this.buttonChoosePath.Text = "...";
            this.buttonChoosePath.UseVisualStyleBackColor = true;
            this.buttonChoosePath.Click += new System.EventHandler(this.buttonChoosePath_Click);
            // 
            // numericUpDownSubRow
            // 
            this.numericUpDownSubRow.Location = new System.Drawing.Point(54, 52);
            this.numericUpDownSubRow.Name = "numericUpDownSubRow";
            this.numericUpDownSubRow.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownSubRow.TabIndex = 5;
            this.numericUpDownSubRow.ValueChanged += new System.EventHandler(this.numericUpDownSubRow_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label3.Location = new System.Drawing.Point(3, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Marker R:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label4.Location = new System.Drawing.Point(4, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Font L:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label5.Location = new System.Drawing.Point(3, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Marker L:";
            // 
            // comboBoxGuidL
            // 
            this.comboBoxGuidL.FormattingEnabled = true;
            this.comboBoxGuidL.Location = new System.Drawing.Point(54, 135);
            this.comboBoxGuidL.Name = "comboBoxGuidL";
            this.comboBoxGuidL.Size = new System.Drawing.Size(335, 21);
            this.comboBoxGuidL.TabIndex = 2;
            this.comboBoxGuidL.SelectedIndexChanged += new System.EventHandler(this.comboBoxGuidL_SelectedIndexChanged);
            // 
            // textBoxFontL
            // 
            this.textBoxFontL.AcceptsReturn = true;
            this.textBoxFontL.Enabled = false;
            this.textBoxFontL.Location = new System.Drawing.Point(54, 162);
            this.textBoxFontL.Name = "textBoxFontL";
            this.textBoxFontL.Size = new System.Drawing.Size(311, 20);
            this.textBoxFontL.TabIndex = 3;
            // 
            // buttonChooseFontL
            // 
            this.buttonChooseFontL.Location = new System.Drawing.Point(365, 162);
            this.buttonChooseFontL.Name = "buttonChooseFontL";
            this.buttonChooseFontL.Size = new System.Drawing.Size(24, 20);
            this.buttonChooseFontL.TabIndex = 4;
            this.buttonChooseFontL.Text = "...";
            this.buttonChooseFontL.UseVisualStyleBackColor = true;
            this.buttonChooseFontL.Click += new System.EventHandler(this.buttonChooseFontL_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FuncName});
            this.dataGridView1.Location = new System.Drawing.Point(54, 188);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(335, 95);
            this.dataGridView1.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label6.Location = new System.Drawing.Point(4, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 26);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ignore \r\nDebug:";
            // 
            // FuncName
            // 
            this.FuncName.HeaderText = "Name";
            this.FuncName.Name = "FuncName";
            this.FuncName.Width = 275;
            // 
            // RTFTextControlOptionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.numericUpDownSubRow);
            this.Controls.Add(this.buttonChoosePath);
            this.Controls.Add(this.buttonChooseFontL);
            this.Controls.Add(this.buttonChooseFontR);
            this.Controls.Add(this.textBoxLoggerPath);
            this.Controls.Add(this.textBoxFontL);
            this.Controls.Add(this.comboBoxGuidL);
            this.Controls.Add(this.textBoxFontR);
            this.Controls.Add(this.comboBoxGuidR);
            this.Controls.Add(this.checkBoxSetLogger);
            this.Controls.Add(this.checkBoxSetTextInControl);
            this.Controls.Add(this.checkBoxHighlightText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RTFTextControlOptionPage";
            this.Size = new System.Drawing.Size(397, 311);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.CheckBox checkBoxHighlightText;
        private System.Windows.Forms.CheckBox checkBoxSetTextInControl;
        private System.Windows.Forms.ComboBox comboBoxGuidR;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFontR;
        private System.Windows.Forms.Button buttonChooseFontR;
        private System.Windows.Forms.CheckBox checkBoxSetLogger;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.TextBox textBoxLoggerPath;
        private System.Windows.Forms.Button buttonChoosePath;
        private System.Windows.Forms.NumericUpDown numericUpDownSubRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxGuidL;
        private System.Windows.Forms.TextBox textBoxFontL;
        private System.Windows.Forms.Button buttonChooseFontL;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn FuncName;

    }
}
