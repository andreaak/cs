namespace Localization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonReadDataFromPo = new System.Windows.Forms.Button();
            this.buttonSaveToTxtForExcel = new System.Windows.Forms.Button();
            this.buttonReadFromCsv = new System.Windows.Forms.Button();
            this.buttonAddFromCsv = new System.Windows.Forms.Button();
            this.buttonAddDataFromCsvCI = new System.Windows.Forms.Button();
            this.buttonReadTranslationCsv = new System.Windows.Forms.Button();
            this.buttonSavePoFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonReadDataFromPo
            // 
            this.buttonReadDataFromPo.Location = new System.Drawing.Point(12, 12);
            this.buttonReadDataFromPo.Name = "buttonReadDataFromPo";
            this.buttonReadDataFromPo.Size = new System.Drawing.Size(154, 23);
            this.buttonReadDataFromPo.TabIndex = 0;
            this.buttonReadDataFromPo.Text = "Read data from .po file";
            this.buttonReadDataFromPo.UseVisualStyleBackColor = true;
            this.buttonReadDataFromPo.Click += new System.EventHandler(this.buttonReadDataFromPo_Click);
            // 
            // buttonSaveToTxtForExcel
            // 
            this.buttonSaveToTxtForExcel.Location = new System.Drawing.Point(353, 12);
            this.buttonSaveToTxtForExcel.Name = "buttonSaveToTxtForExcel";
            this.buttonSaveToTxtForExcel.Size = new System.Drawing.Size(128, 23);
            this.buttonSaveToTxtForExcel.TabIndex = 0;
            this.buttonSaveToTxtForExcel.Text = "Save For Excel";
            this.buttonSaveToTxtForExcel.UseVisualStyleBackColor = true;
            this.buttonSaveToTxtForExcel.Click += new System.EventHandler(this.buttonSaveToTxtForExcel_Click);
            // 
            // buttonReadFromCsv
            // 
            this.buttonReadFromCsv.Location = new System.Drawing.Point(12, 41);
            this.buttonReadFromCsv.Name = "buttonReadFromCsv";
            this.buttonReadFromCsv.Size = new System.Drawing.Size(154, 23);
            this.buttonReadFromCsv.TabIndex = 0;
            this.buttonReadFromCsv.Text = "Read data from csv file";
            this.buttonReadFromCsv.UseVisualStyleBackColor = true;
            this.buttonReadFromCsv.Click += new System.EventHandler(this.buttonReadFromCsv_Click);
            // 
            // buttonAddFromCsv
            // 
            this.buttonAddFromCsv.Location = new System.Drawing.Point(184, 12);
            this.buttonAddFromCsv.Name = "buttonAddFromCsv";
            this.buttonAddFromCsv.Size = new System.Drawing.Size(150, 23);
            this.buttonAddFromCsv.TabIndex = 0;
            this.buttonAddFromCsv.Text = "Add data from csv file";
            this.buttonAddFromCsv.UseVisualStyleBackColor = true;
            this.buttonAddFromCsv.Click += new System.EventHandler(this.buttonAddFromCsv_Click);
            // 
            // buttonAddDataFromCsvCI
            // 
            this.buttonAddDataFromCsvCI.Location = new System.Drawing.Point(184, 41);
            this.buttonAddDataFromCsvCI.Name = "buttonAddDataFromCsvCI";
            this.buttonAddDataFromCsvCI.Size = new System.Drawing.Size(150, 23);
            this.buttonAddDataFromCsvCI.TabIndex = 0;
            this.buttonAddDataFromCsvCI.Text = "Add data from csv file(CaseI)";
            this.buttonAddDataFromCsvCI.UseVisualStyleBackColor = true;
            this.buttonAddDataFromCsvCI.Click += new System.EventHandler(this.buttonAddDataFromCsvCI_Click);
            // 
            // buttonReadTranslationCsv
            // 
            this.buttonReadTranslationCsv.Location = new System.Drawing.Point(12, 90);
            this.buttonReadTranslationCsv.Name = "buttonReadTranslationCsv";
            this.buttonReadTranslationCsv.Size = new System.Drawing.Size(154, 23);
            this.buttonReadTranslationCsv.TabIndex = 0;
            this.buttonReadTranslationCsv.Text = "Read translation from csv file";
            this.buttonReadTranslationCsv.UseVisualStyleBackColor = true;
            this.buttonReadTranslationCsv.Click += new System.EventHandler(this.buttonReadTranslationCsv_Click);
            // 
            // buttonSavePoFile
            // 
            this.buttonSavePoFile.Location = new System.Drawing.Point(353, 41);
            this.buttonSavePoFile.Name = "buttonSavePoFile";
            this.buttonSavePoFile.Size = new System.Drawing.Size(128, 23);
            this.buttonSavePoFile.TabIndex = 0;
            this.buttonSavePoFile.Text = "SavePo File";
            this.buttonSavePoFile.UseVisualStyleBackColor = true;
            this.buttonSavePoFile.Click += new System.EventHandler(this.buttonSavePoFile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 171);
            this.Controls.Add(this.buttonSavePoFile);
            this.Controls.Add(this.buttonSaveToTxtForExcel);
            this.Controls.Add(this.buttonAddDataFromCsvCI);
            this.Controls.Add(this.buttonAddFromCsv);
            this.Controls.Add(this.buttonReadTranslationCsv);
            this.Controls.Add(this.buttonReadFromCsv);
            this.Controls.Add(this.buttonReadDataFromPo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Localization";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonReadDataFromPo;
        private System.Windows.Forms.Button buttonSaveToTxtForExcel;
        private System.Windows.Forms.Button buttonReadFromCsv;
        private System.Windows.Forms.Button buttonAddFromCsv;
        private System.Windows.Forms.Button buttonAddDataFromCsvCI;
        private System.Windows.Forms.Button buttonReadTranslationCsv;
        private System.Windows.Forms.Button buttonSavePoFile;
    }
}

