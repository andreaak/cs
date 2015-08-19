namespace Note
{
    partial class ItemsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemsList));
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.colDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDataStatus = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageListState = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedCell.Options.UseFont = true;
            this.treeList1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeList1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedRow.Options.UseFont = true;
            this.treeList1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 11F);
            this.treeList1.Appearance.Row.Options.UseFont = true;
            this.treeList1.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.treeList1.Appearance.SelectedRow.Options.UseFont = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colDescription,
            this.colDataStatus});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeList1.ImageIndexFieldName = "Type";
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.KeepSelectedOnClick = false;
            this.treeList1.OptionsSelection.MultiSelect = true;
            this.treeList1.OptionsSelection.UseIndicatorForSelection = true;
            this.treeList1.OptionsView.ShowHorzLines = false;
            this.treeList1.OptionsView.ShowIndicator = false;
            this.treeList1.OptionsView.ShowVertLines = false;
            this.treeList1.SelectImageList = this.imageListState;
            this.treeList1.Size = new System.Drawing.Size(661, 304);
            this.treeList1.TabIndex = 0;
            // 
            // colDescription
            // 
            this.colDescription.Caption = " ";
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 33;
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 0;
            this.colDescription.Width = 299;
			// 
            // colState
            // 
            this.colDataStatus.Caption = " ";
            this.colDataStatus.FieldName = "DataStatus";
            this.colDataStatus.MinWidth = 33;
            this.colDataStatus.Name = "colState";
            this.colDataStatus.Visible = true;
            this.colDataStatus.VisibleIndex = 0;
            this.colDataStatus.Width = 100;
            // 
            // imageListState
            // 
            this.imageListState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListState.ImageStream")));
            this.imageListState.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListState.Images.SetKeyName(0, "folder.png");
            this.imageListState.Images.SetKeyName(1, "paste.png");
            // 
            // ItemsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 304);
            this.Controls.Add(this.treeList1);
            this.Name = "ItemsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Changed Items";
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private System.Windows.Forms.ImageList imageListState;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDescription;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDataStatus;
    }
}