namespace Algoritms
{
    partial class EditMethod
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
            this.label2 = new System.Windows.Forms.Label();
            this.labelParameters = new System.Windows.Forms.Label();
            this.textBoxNamespace = new System.Windows.Forms.TextBox();
            this.textBoxClass = new System.Windows.Forms.TextBox();
            this.textBoxParameters = new System.Windows.Forms.TextBox();
            this.checkBoxStatic = new System.Windows.Forms.CheckBox();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.buttonEditBranchBlock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Namespace:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Class:";
            // 
            // labelParameters
            // 
            this.labelParameters.AutoSize = true;
            this.labelParameters.Location = new System.Drawing.Point(12, 104);
            this.labelParameters.Name = "labelParameters";
            this.labelParameters.Size = new System.Drawing.Size(63, 13);
            this.labelParameters.TabIndex = 0;
            this.labelParameters.Text = "Parameters:";
            // 
            // textBoxNamespace
            // 
            this.textBoxNamespace.Location = new System.Drawing.Point(87, 23);
            this.textBoxNamespace.Name = "textBoxNamespace";
            this.textBoxNamespace.Size = new System.Drawing.Size(381, 20);
            this.textBoxNamespace.TabIndex = 1;
            // 
            // textBoxClass
            // 
            this.textBoxClass.Location = new System.Drawing.Point(87, 49);
            this.textBoxClass.Name = "textBoxClass";
            this.textBoxClass.Size = new System.Drawing.Size(381, 20);
            this.textBoxClass.TabIndex = 2;
            // 
            // textBoxParameters
            // 
            this.textBoxParameters.Location = new System.Drawing.Point(87, 101);
            this.textBoxParameters.Name = "textBoxParameters";
            this.textBoxParameters.Size = new System.Drawing.Size(381, 20);
            this.textBoxParameters.TabIndex = 4;
            // 
            // checkBoxStatic
            // 
            this.checkBoxStatic.AutoSize = true;
            this.checkBoxStatic.Location = new System.Drawing.Point(16, 161);
            this.checkBoxStatic.Name = "checkBoxStatic";
            this.checkBoxStatic.Size = new System.Drawing.Size(53, 17);
            this.checkBoxStatic.TabIndex = 5;
            this.checkBoxStatic.Text = "Static";
            this.checkBoxStatic.UseVisualStyleBackColor = true;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(210, 161);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 6;
            this.buttonEdit.Text = "Save";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Method Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(87, 75);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(381, 20);
            this.textBoxName.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Language:";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(87, 130);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(381, 21);
            this.comboBoxLanguage.TabIndex = 7;
            // 
            // buttonEditBranchBlock
            // 
            this.buttonEditBranchBlock.Location = new System.Drawing.Point(291, 161);
            this.buttonEditBranchBlock.Name = "buttonEditBranchBlock";
            this.buttonEditBranchBlock.Size = new System.Drawing.Size(75, 23);
            this.buttonEditBranchBlock.TabIndex = 8;
            this.buttonEditBranchBlock.Text = "Edit Blocks";
            this.buttonEditBranchBlock.UseVisualStyleBackColor = true;
            this.buttonEditBranchBlock.Visible = false;
            this.buttonEditBranchBlock.Click += new System.EventHandler(this.buttonEditBranchBlock_Click);
            // 
            // EditMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 196);
            this.Controls.Add(this.buttonEditBranchBlock);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.checkBoxStatic);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxParameters);
            this.Controls.Add(this.textBoxClass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxNamespace);
            this.Controls.Add(this.labelParameters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditMethod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Method";
            this.Shown += new System.EventHandler(this.EditMethod_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelParameters;
        private System.Windows.Forms.TextBox textBoxNamespace;
        private System.Windows.Forms.TextBox textBoxClass;
        private System.Windows.Forms.TextBox textBoxParameters;
        private System.Windows.Forms.CheckBox checkBoxStatic;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Button buttonEditBranchBlock;
    }
}

