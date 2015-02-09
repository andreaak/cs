namespace ParseQuery
{
    partial class ParseQueryForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParseQueryForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxDatabases = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonSyncLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSyncRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBoxBase = new System.Windows.Forms.RichTextBox();
            this.textBoxParse = new System.Windows.Forms.RichTextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxDatabases,
            this.toolStripButtonSyncLeft,
            this.toolStripButtonSyncRight,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1054, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripComboBoxDatabases
            // 
            this.toolStripComboBoxDatabases.Items.AddRange(new object[] {
            "Oracle",
            "SQL Server",
            "DB2",
            "MySQL",
            "MS Access"});
            this.toolStripComboBoxDatabases.Name = "toolStripComboBoxDatabases";
            this.toolStripComboBoxDatabases.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripButtonSyncLeft
            // 
            this.toolStripButtonSyncLeft.CheckOnClick = true;
            this.toolStripButtonSyncLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSyncLeft.Image = global::GrabDatabase.Properties.Resources.loopnone2;
            this.toolStripButtonSyncLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSyncLeft.Name = "toolStripButtonSyncLeft";
            this.toolStripButtonSyncLeft.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSyncLeft.Text = "Sync Left";
            this.toolStripButtonSyncLeft.CheckedChanged += new System.EventHandler(this.toolStripButtonSyncLeft_CheckedChanged);
            // 
            // toolStripButtonSyncRight
            // 
            this.toolStripButtonSyncRight.CheckOnClick = true;
            this.toolStripButtonSyncRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSyncRight.Image = global::GrabDatabase.Properties.Resources.loopnone;
            this.toolStripButtonSyncRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSyncRight.Name = "toolStripButtonSyncRight";
            this.toolStripButtonSyncRight.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSyncRight.Text = "Sync Right";
            this.toolStripButtonSyncRight.CheckedChanged += new System.EventHandler(this.toolStripButtonSyncRight_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(473, 29);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 692);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedValueChanged += new System.EventHandler(this.toolStripButtonParseQueryParameters_Click);
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // textBoxBase
            // 
            this.textBoxBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBase.Location = new System.Drawing.Point(0, 28);
            this.textBoxBase.Name = "textBoxBase";
            this.textBoxBase.Size = new System.Drawing.Size(467, 694);
            this.textBoxBase.TabIndex = 3;
            this.textBoxBase.Text = "";
            this.textBoxBase.VScroll += new System.EventHandler(this.textBoxBase_VScroll);
            this.textBoxBase.TextChanged += new System.EventHandler(this.toolStripButtonGetParameters_Click);
            // 
            // textBoxParse
            // 
            this.textBoxParse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxParse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxParse.Location = new System.Drawing.Point(600, 29);
            this.textBoxParse.Name = "textBoxParse";
            this.textBoxParse.Size = new System.Drawing.Size(454, 693);
            this.textBoxParse.TabIndex = 4;
            this.textBoxParse.Text = "";
            this.textBoxParse.VScroll += new System.EventHandler(this.textBoxParse_VScroll);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Parse Query";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // ParseQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 740);
            this.Controls.Add(this.textBoxParse);
            this.Controls.Add(this.textBoxBase);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ParseQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parse Query";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RichTextBox textBoxBase;
        private System.Windows.Forms.RichTextBox textBoxParse;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxDatabases;
        private System.Windows.Forms.ToolStripButton toolStripButtonSyncLeft;
        private System.Windows.Forms.ToolStripButton toolStripButtonSyncRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

