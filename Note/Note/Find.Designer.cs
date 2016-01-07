namespace Note
{
    partial class Find
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Find));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colText = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colOrderPosition = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.simpleButtonFind = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemFindText = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemFindButton = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTree = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFindText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFindButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTree)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.treeList);
            this.layoutControl1.Controls.Add(this.simpleButtonFind);
            this.layoutControl1.Controls.Add(this.textEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(674, 569);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // treeList
            // 
            this.treeList.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.treeList.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.White;
            this.treeList.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList.Appearance.FocusedCell.Options.UseFont = true;
            this.treeList.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeList.Appearance.FocusedRow.Options.UseFont = true;
            this.treeList.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 11F);
            this.treeList.Appearance.Row.Options.UseFont = true;
            this.treeList.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeList.Appearance.SelectedRow.Options.UseFont = true;
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colDescription,
            this.colText,
            this.colOrderPosition});
            this.treeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeList.ImageIndexFieldName = "Type";
            this.treeList.Location = new System.Drawing.Point(12, 38);
            this.treeList.Name = "treeList";
            this.treeList.OptionsBehavior.KeepSelectedOnClick = false;
            this.treeList.OptionsSelection.MultiSelect = true;
            this.treeList.OptionsSelection.UseIndicatorForSelection = true;
            this.treeList.OptionsView.ShowHorzLines = false;
            this.treeList.OptionsView.ShowIndicator = false;
            this.treeList.OptionsView.ShowVertLines = false;
            this.treeList.SelectImageList = this.imageList;
            this.treeList.Size = new System.Drawing.Size(650, 519);
            this.treeList.TabIndex = 6;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 33;
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 0;
            this.colDescription.Width = 217;
            // 
            // colText
            // 
            this.colText.Caption = "Text";
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 1;
            this.colText.Width = 431;
            // 
            // colOrderPosition
            // 
            this.colOrderPosition.Caption = "OrderPosition";
            this.colOrderPosition.FieldName = "OrderPosition";
            this.colOrderPosition.Name = "colOrderPosition";
            this.colOrderPosition.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder.png");
            this.imageList.Images.SetKeyName(1, "paste.png");
            // 
            // simpleButtonFind
            // 
            this.simpleButtonFind.Location = new System.Drawing.Point(593, 12);
            this.simpleButtonFind.Name = "simpleButtonFind";
            this.simpleButtonFind.Size = new System.Drawing.Size(69, 22);
            this.simpleButtonFind.StyleController = this.layoutControl1;
            this.simpleButtonFind.TabIndex = 5;
            this.simpleButtonFind.Text = "Find";
            this.simpleButtonFind.Click += new System.EventHandler(this.simpleButtonFind_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(12, 12);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(577, 20);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemFindText,
            this.layoutControlItemFindButton,
            this.layoutControlItemTree});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(674, 569);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemFindText
            // 
            this.layoutControlItemFindText.Control = this.textEdit1;
            this.layoutControlItemFindText.CustomizationFormText = "layoutControlItemFindText";
            this.layoutControlItemFindText.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemFindText.Name = "layoutControlItemFindText";
            this.layoutControlItemFindText.Size = new System.Drawing.Size(581, 26);
            this.layoutControlItemFindText.Text = "layoutControlItemFindText";
            this.layoutControlItemFindText.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemFindText.TextToControlDistance = 0;
            this.layoutControlItemFindText.TextVisible = false;
            // 
            // layoutControlItemFindButton
            // 
            this.layoutControlItemFindButton.Control = this.simpleButtonFind;
            this.layoutControlItemFindButton.CustomizationFormText = "layoutControlItemFindButton";
            this.layoutControlItemFindButton.Location = new System.Drawing.Point(581, 0);
            this.layoutControlItemFindButton.MaxSize = new System.Drawing.Size(73, 26);
            this.layoutControlItemFindButton.MinSize = new System.Drawing.Size(73, 26);
            this.layoutControlItemFindButton.Name = "layoutControlItemFindButton";
            this.layoutControlItemFindButton.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItemFindButton.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemFindButton.Text = "layoutControlItemFindButton";
            this.layoutControlItemFindButton.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemFindButton.TextToControlDistance = 0;
            this.layoutControlItemFindButton.TextVisible = false;
            // 
            // layoutControlItemTree
            // 
            this.layoutControlItemTree.Control = this.treeList;
            this.layoutControlItemTree.CustomizationFormText = "layoutControlItemTree";
            this.layoutControlItemTree.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemTree.Name = "layoutControlItemTree";
            this.layoutControlItemTree.Size = new System.Drawing.Size(654, 523);
            this.layoutControlItemTree.Text = "layoutControlItemTree";
            this.layoutControlItemTree.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTree.TextToControlDistance = 0;
            this.layoutControlItemTree.TextVisible = false;
            // 
            // Find
            // 
            this.AcceptButton = this.simpleButtonFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 569);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Find";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFindText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFindButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraTreeList.TreeList treeList;
        private DevExpress.XtraEditors.SimpleButton simpleButtonFind;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFindText;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFindButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTree;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDescription;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colText;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colOrderPosition;
    }
}