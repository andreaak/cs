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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxDatabases = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonSyncLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSyncRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.textBoxParse = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemParsed = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemParameters = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlItemBase = new DevExpress.XtraLayout.LayoutControlItem();
            this.textBoxBase = new System.Windows.Forms.RichTextBox();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemParsed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
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
            // textBoxParse
            // 
            this.textBoxParse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxParse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxParse.Location = new System.Drawing.Point(612, 30);
            this.textBoxParse.Name = "textBoxParse";
            this.textBoxParse.Size = new System.Drawing.Size(440, 708);
            this.textBoxParse.TabIndex = 4;
            this.textBoxParse.Text = "";
            this.textBoxParse.VScroll += new System.EventHandler(this.textBoxParse_VScroll);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(433, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(177, 708);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedValueChanged += new System.EventHandler(this.toolStripButtonParseQueryParameters_Click);
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemParsed,
            this.layoutControlItemParameters,
            this.layoutControlItemBase,
            this.emptySpaceItem1});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1054, 740);
            this.Root.Text = "Root";
            // 
            // layoutControlItemParsed
            // 
            this.layoutControlItemParsed.Control = this.textBoxParse;
            this.layoutControlItemParsed.CustomizationFormText = "layoutControlItemParsed";
            this.layoutControlItemParsed.Location = new System.Drawing.Point(610, 28);
            this.layoutControlItemParsed.Name = "layoutControlItemParsed";
            this.layoutControlItemParsed.Size = new System.Drawing.Size(444, 712);
            this.layoutControlItemParsed.Text = "layoutControlItemParsed";
            this.layoutControlItemParsed.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemParsed.TextToControlDistance = 0;
            this.layoutControlItemParsed.TextVisible = false;
            // 
            // layoutControlItemParameters
            // 
            this.layoutControlItemParameters.Control = this.listBox1;
            this.layoutControlItemParameters.CustomizationFormText = "layoutControlItemParameters";
            this.layoutControlItemParameters.Location = new System.Drawing.Point(433, 28);
            this.layoutControlItemParameters.MaxSize = new System.Drawing.Size(177, 0);
            this.layoutControlItemParameters.MinSize = new System.Drawing.Size(177, 24);
            this.layoutControlItemParameters.Name = "layoutControlItemParameters";
            this.layoutControlItemParameters.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItemParameters.Size = new System.Drawing.Size(177, 712);
            this.layoutControlItemParameters.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemParameters.Text = "layoutControlItemParameters";
            this.layoutControlItemParameters.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemParameters.TextToControlDistance = 0;
            this.layoutControlItemParameters.TextVisible = false;
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.listBox1);
            this.layoutControl.Controls.Add(this.textBoxParse);
            this.layoutControl.Controls.Add(this.textBoxBase);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(54, 151, 250, 350);
            this.layoutControl.Root = this.Root;
            this.layoutControl.Size = new System.Drawing.Size(1054, 740);
            this.layoutControl.TabIndex = 5;
            // 
            // layoutControlItemBase
            // 
            this.layoutControlItemBase.Control = this.textBoxBase;
            this.layoutControlItemBase.CustomizationFormText = "layoutControlItemBase";
            this.layoutControlItemBase.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItemBase.Name = "layoutControlItemBase";
            this.layoutControlItemBase.Size = new System.Drawing.Size(433, 712);
            this.layoutControlItemBase.Text = "layoutControlItemBase";
            this.layoutControlItemBase.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBase.TextToControlDistance = 0;
            this.layoutControlItemBase.TextVisible = false;
            // 
            // textBoxBase
            // 
            this.textBoxBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBase.Location = new System.Drawing.Point(2, 30);
            this.textBoxBase.Name = "textBoxBase";
            this.textBoxBase.Size = new System.Drawing.Size(429, 708);
            this.textBoxBase.TabIndex = 3;
            this.textBoxBase.Text = "";
            this.textBoxBase.VScroll += new System.EventHandler(this.textBoxBase_VScroll);
            this.textBoxBase.TextChanged += new System.EventHandler(this.toolStripButtonGetParameters_Click);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 28);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 28);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem1.Size = new System.Drawing.Size(1054, 28);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ParseQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 740);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.layoutControl);
            this.Name = "ParseQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parse Query";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemParsed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxDatabases;
        private System.Windows.Forms.ToolStripButton toolStripButtonSyncLeft;
        private System.Windows.Forms.ToolStripButton toolStripButtonSyncRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RichTextBox textBoxParse;
        private System.Windows.Forms.ListBox listBox1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemParsed;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemParameters;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBase;
        private System.Windows.Forms.RichTextBox textBoxBase;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
    }
}

