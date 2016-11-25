namespace ReelsHelper
{
    partial class CheckShiftReelsPrepareForm
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
            this.labelResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTallSymbols = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnReel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnShift = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonCheck = new System.Windows.Forms.Button();
            this.columnIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelResult.ForeColor = System.Drawing.Color.Black;
            this.labelResult.Location = new System.Drawing.Point(60, 65);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(21, 13);
            this.labelResult.TabIndex = 0;
            this.labelResult.Text = "Ok";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Result:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tall Symbols:";
            // 
            // textBoxTallSymbols
            // 
            this.textBoxTallSymbols.Location = new System.Drawing.Point(88, 9);
            this.textBoxTallSymbols.Name = "textBoxTallSymbols";
            this.textBoxTallSymbols.Size = new System.Drawing.Size(205, 20);
            this.textBoxTallSymbols.TabIndex = 2;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnReel,
            this.columnShift,
            this.columnIcon});
            this.listView1.Location = new System.Drawing.Point(17, 94);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(530, 253);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnReel
            // 
            this.columnReel.Text = "Reel";
            this.columnReel.Width = 83;
            // 
            // columnShift
            // 
            this.columnShift.Text = "Shift";
            this.columnShift.Width = 111;
            // 
            // buttonCheck
            // 
            this.buttonCheck.Location = new System.Drawing.Point(472, 7);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(75, 23);
            this.buttonCheck.TabIndex = 4;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // columnIcon
            // 
            this.columnIcon.Text = "Icon";
            this.columnIcon.Width = 107;
            // 
            // CheckShiftReelsPrepareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 359);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBoxTallSymbols);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelResult);
            this.Name = "CheckShiftReelsPrepareForm";
            this.Text = "CheckShiftReelsPrepareForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTallSymbols;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.ColumnHeader columnReel;
        private System.Windows.Forms.ColumnHeader columnShift;
        private System.Windows.Forms.ColumnHeader columnIcon;
    }
}