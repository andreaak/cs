namespace Algoritms
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpenData = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportFromXML = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveData = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportToXML = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAddMethod = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddBranch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddCycle = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteMethod = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInsertToMethod = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUpToHierarchy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonShowDiagramm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNew,
            this.toolStripButtonOpenData,
            this.toolStripButtonImportFromXML,
            this.toolStripButtonSaveData,
            this.toolStripButtonExportToXML,
            this.toolStripSeparator1,
            this.toolStripButtonAddMethod,
            this.toolStripButtonAddBranch,
            this.toolStripButtonAddCycle,
            this.toolStripButtonDeleteMethod,
            this.toolStripButtonEdit,
            this.toolStripButtonUpToHierarchy,
            this.toolStripButtonInsertToMethod,
            this.toolStripButtonUp,
            this.toolStripButtonDown,
            this.toolStripSeparator2,
            this.toolStripButtonShowDiagramm,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(598, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = global::Algoritms.Properties.Resources.file;
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNew.Text = "New";
            this.toolStripButtonNew.Click += new System.EventHandler(this.toolStripButtonNew_Click);
            // 
            // toolStripButtonOpenData
            // 
            this.toolStripButtonOpenData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenData.Image = global::Algoritms.Properties.Resources.openHS;
            this.toolStripButtonOpenData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenData.Name = "toolStripButtonOpenData";
            this.toolStripButtonOpenData.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpenData.Text = "Open Data";
            this.toolStripButtonOpenData.Click += new System.EventHandler(this.toolStripButtonOpenData_Click);
            // 
            // toolStripButtonImportFromXML
            // 
            this.toolStripButtonImportFromXML.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportFromXML.Image = global::Algoritms.Properties.Resources.Ankh_Icon_18_256x256_32;
            this.toolStripButtonImportFromXML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportFromXML.Name = "toolStripButtonImportFromXML";
            this.toolStripButtonImportFromXML.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonImportFromXML.Text = "Import From XML";
            this.toolStripButtonImportFromXML.Click += new System.EventHandler(this.toolStripButtonImportFromXML_Click);
            // 
            // toolStripButtonSaveData
            // 
            this.toolStripButtonSaveData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveData.Image = global::Algoritms.Properties.Resources.saveHS;
            this.toolStripButtonSaveData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveData.Name = "toolStripButtonSaveData";
            this.toolStripButtonSaveData.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSaveData.Text = "Save Data";
            this.toolStripButtonSaveData.Click += new System.EventHandler(this.toolStripButtonSaveData_Click);
            // 
            // toolStripButtonExportToXML
            // 
            this.toolStripButtonExportToXML.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExportToXML.Image = global::Algoritms.Properties.Resources.Ankh_Icon_36_256x256_32;
            this.toolStripButtonExportToXML.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportToXML.Name = "toolStripButtonExportToXML";
            this.toolStripButtonExportToXML.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExportToXML.Text = "Export to XML ";
            this.toolStripButtonExportToXML.Click += new System.EventHandler(this.toolStripButtonExportToXML_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAddMethod
            // 
            this.toolStripButtonAddMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddMethod.Image = global::Algoritms.Properties.Resources.add_;
            this.toolStripButtonAddMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddMethod.Name = "toolStripButtonAddMethod";
            this.toolStripButtonAddMethod.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddMethod.Text = "Add Method";
            this.toolStripButtonAddMethod.Click += new System.EventHandler(this.toolStripButtonAddMethod_Click);
            // 
            // toolStripButtonAddBranch
            // 
            this.toolStripButtonAddBranch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddBranch.Image = global::Algoritms.Properties.Resources.branch;
            this.toolStripButtonAddBranch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddBranch.Name = "toolStripButtonAddBranch";
            this.toolStripButtonAddBranch.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddBranch.Text = "Add Branch";
            this.toolStripButtonAddBranch.Click += new System.EventHandler(this.toolStripButtonAddBranch_Click);
            // 
            // toolStripButtonAddCycle
            // 
            this.toolStripButtonAddCycle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddCycle.Image = global::Algoritms.Properties.Resources.refresh;
            this.toolStripButtonAddCycle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddCycle.Name = "toolStripButtonAddCycle";
            this.toolStripButtonAddCycle.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddCycle.Text = "Add Cycle";
            this.toolStripButtonAddCycle.Click += new System.EventHandler(this.toolStripButtonAddCycle_Click);
            // 
            // toolStripButtonDeleteMethod
            // 
            this.toolStripButtonDeleteMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDeleteMethod.Image = global::Algoritms.Properties.Resources.Delete;
            this.toolStripButtonDeleteMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteMethod.Name = "toolStripButtonDeleteMethod";
            this.toolStripButtonDeleteMethod.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDeleteMethod.Text = "Delete Method";
            this.toolStripButtonDeleteMethod.Click += new System.EventHandler(this.toolStripButtonDeleteMethod_Click);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEdit.Image = global::Algoritms.Properties.Resources.Settings;
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonEdit.Text = "Edit";
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
            // 
            // toolStripButtonInsertToMethod
            // 
            this.toolStripButtonInsertToMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInsertToMethod.Image = global::Algoritms.Properties.Resources.PHANTOM_10;
            this.toolStripButtonInsertToMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInsertToMethod.Name = "toolStripButtonInsertToMethod";
            this.toolStripButtonInsertToMethod.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonInsertToMethod.Text = "Insert To Method";
            this.toolStripButtonInsertToMethod.Click += new System.EventHandler(this.toolStripButtonInsertToMethod_Click);
            // 
            // toolStripButtonUpToHierarchy
            // 
            this.toolStripButtonUpToHierarchy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUpToHierarchy.Image = global::Algoritms.Properties.Resources.PHANTOM_9;
            this.toolStripButtonUpToHierarchy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpToHierarchy.Name = "toolStripButtonUpToHierarchy";
            this.toolStripButtonUpToHierarchy.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUpToHierarchy.Text = "Up To Hierarchy";
            this.toolStripButtonUpToHierarchy.Click += new System.EventHandler(this.toolStripButtonUpToHierarchy_Click);
            // 
            // toolStripButtonUp
            // 
            this.toolStripButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUp.Image = global::Algoritms.Properties.Resources._2uparrow;
            this.toolStripButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUp.Name = "toolStripButtonUp";
            this.toolStripButtonUp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUp.Text = "Up";
            this.toolStripButtonUp.Click += new System.EventHandler(this.toolStripButtonUp_Click);
            // 
            // toolStripButtonDown
            // 
            this.toolStripButtonDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDown.Image = global::Algoritms.Properties.Resources._2downarrow;
            this.toolStripButtonDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDown.Name = "toolStripButtonDown";
            this.toolStripButtonDown.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDown.Text = "Down";
            this.toolStripButtonDown.Click += new System.EventHandler(this.toolStripButtonDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonShowDiagramm
            // 
            this.toolStripButtonShowDiagramm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowDiagramm.Image = global::Algoritms.Properties.Resources.dossier_photo;
            this.toolStripButtonShowDiagramm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowDiagramm.Name = "toolStripButtonShowDiagramm";
            this.toolStripButtonShowDiagramm.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowDiagramm.Text = "toolStripButtonShowDiagramm";
            this.toolStripButtonShowDiagramm.Click += new System.EventHandler(this.toolStripButtonShowDiagramm_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(598, 454);
            this.treeView1.TabIndex = 2;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "redled.png");
            this.imageList1.Images.SetKeyName(1, "build.png");
            this.imageList1.Images.SetKeyName(2, "CS.jpg");
            this.imageList1.Images.SetKeyName(3, "CS_S.jpg");
            this.imageList1.Images.SetKeyName(4, "Java.jpg");
            this.imageList1.Images.SetKeyName(5, "Java_S.jpg");
            this.imageList1.Images.SetKeyName(6, "C_.jpg");
            this.imageList1.Images.SetKeyName(7, "C__S.jpg");
            this.imageList1.Images.SetKeyName(8, "branch.png");
            this.imageList1.Images.SetKeyName(9, "bug_green.png");
            this.imageList1.Images.SetKeyName(10, "bug_red.png");
            this.imageList1.Images.SetKeyName(11, "logic_or.png");
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Algoritms";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Algoritms";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 478);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Algoritms";
            this.SizeChanged += new System.EventHandler(this.Form_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddMethod;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveData;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteMethod;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowDiagramm;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonInsertToMethod;
        private System.Windows.Forms.ToolStripButton toolStripButtonUp;
        private System.Windows.Forms.ToolStripButton toolStripButtonDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddBranch;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddCycle;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportToXML;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportFromXML;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpToHierarchy;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}