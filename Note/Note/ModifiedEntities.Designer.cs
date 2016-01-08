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
            this.treeList = new DevExpress.XtraTreeList.TreeList();
            this.colDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageListState = new System.Windows.Forms.ImageList(this.components);
            this.colOrderPosition = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList
            // 
            this.treeList.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList.Appearance.FocusedCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList.Appearance.FocusedCell.Options.UseFont = true;
            this.treeList.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.treeList.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeList.Appearance.FocusedRow.Options.UseFont = true;
            this.treeList.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 11F);
            this.treeList.Appearance.Row.Options.UseFont = true;
            this.treeList.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.treeList.Appearance.SelectedRow.Options.UseFont = true;
            this.treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colDescription,
            this.colState,
            this.colOrderPosition});
            this.treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeList.ImageIndexFieldName = "Type";
            this.treeList.Location = new System.Drawing.Point(0, 0);
            this.treeList.Name = "treeList";
            this.treeList.OptionsBehavior.KeepSelectedOnClick = false;
            this.treeList.OptionsSelection.MultiSelect = true;
            this.treeList.OptionsSelection.UseIndicatorForSelection = true;
            this.treeList.OptionsView.ShowHorzLines = false;
            this.treeList.OptionsView.ShowIndicator = false;
            this.treeList.OptionsView.ShowVertLines = false;
            this.treeList.SelectImageList = this.imageListState;
            this.treeList.Size = new System.Drawing.Size(661, 304);
            this.treeList.TabIndex = 0;
            // 
            // colDescription
            // 
            this.colDescription.Caption = " Node";
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 33;
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 0;
            this.colDescription.Width = 299;
            // 
            // colState
            // 
            this.colState.Caption = " State";
            this.colState.FieldName = "DataStatus";
            this.colState.MinWidth = 33;
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 1;
            this.colState.Width = 100;
            // 
            // imageListState
            // 
            this.imageListState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListState.ImageStream")));
            this.imageListState.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListState.Images.SetKeyName(0, "folder.png");
            this.imageListState.Images.SetKeyName(1, "paste.png");
            // 
            // colOrderPosition
            // 
            this.colOrderPosition.Caption = "OrderPosition";
            this.colOrderPosition.FieldName = "OrderPosition";
            this.colOrderPosition.Name = "colOrderPosition";
            this.colOrderPosition.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            // 
            // ItemsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 304);
            this.Controls.Add(this.treeList);
            this.Name = "ItemsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Changed Items";
            ((System.ComponentModel.ISupportInitialize)(this.treeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList;
        private System.Windows.Forms.ImageList imageListState;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDescription;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colState;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colOrderPosition;
    }
}