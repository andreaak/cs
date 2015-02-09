namespace GrabDatabase.Grab
{
    partial class SqlCommand
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlDbEntity = new DevExpress.XtraGrid.GridControl();
            this.gridViewDbEntity = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStripToolBox = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpenSql = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveSql = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSelectQueryHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInsertQueryHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUpdateQueryHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteQueryHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxMaxItemsCount = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonUpdateDbData = new System.Windows.Forms.ToolStripButton();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.gridControlResult = new DevExpress.XtraGrid.GridControl();
            this.gridViewResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemSqlQuery_ = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemToolStrip = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDbEntity = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDbEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDbEntity)).BeginInit();
            this.toolStripToolBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSqlQuery_)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToolStrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDbEntity)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControlDbEntity);
            this.layoutControl1.Controls.Add(this.toolStripToolBox);
            this.layoutControl1.Controls.Add(this.memoEdit1);
            this.layoutControl1.Controls.Add(this.gridControlResult);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(540, 186, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(850, 441);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControlDbEntity
            // 
            this.gridControlDbEntity.Location = new System.Drawing.Point(471, 36);
            this.gridControlDbEntity.MainView = this.gridViewDbEntity;
            this.gridControlDbEntity.Name = "gridControlDbEntity";
            this.gridControlDbEntity.Size = new System.Drawing.Size(367, 113);
            this.gridControlDbEntity.TabIndex = 5;
            this.gridControlDbEntity.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDbEntity});
            this.gridControlDbEntity.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.gridControl1_ViewRegistered);
            // 
            // gridViewDbEntity
            // 
            this.gridViewDbEntity.GridControl = this.gridControlDbEntity;
            this.gridViewDbEntity.Name = "gridViewDbEntity";
            this.gridViewDbEntity.OptionsBehavior.Editable = false;
            this.gridViewDbEntity.OptionsBehavior.ReadOnly = true;
            this.gridViewDbEntity.OptionsSelection.MultiSelect = true;
            this.gridViewDbEntity.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewDbEntity.OptionsView.ShowGroupPanel = false;
            this.gridViewDbEntity.OptionsView.ShowIndicator = false;
            this.gridViewDbEntity.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.view_PopupMenuShowing);
            // 
            // toolStripToolBox
            // 
            this.toolStripToolBox.AutoSize = false;
            this.toolStripToolBox.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripToolBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenSql,
            this.toolStripButtonSaveSql,
            this.toolStripSeparator2,
            this.toolStripButtonSelectQueryHelp,
            this.toolStripButtonInsertQueryHelp,
            this.toolStripButtonUpdateQueryHelp,
            this.toolStripButtonDeleteQueryHelp,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripTextBoxMaxItemsCount,
            this.toolStripComboBox1,
            this.toolStripButtonUpdateDbData});
            this.toolStripToolBox.Location = new System.Drawing.Point(12, 12);
            this.toolStripToolBox.Name = "toolStripToolBox";
            this.toolStripToolBox.Size = new System.Drawing.Size(826, 20);
            this.toolStripToolBox.TabIndex = 3;
            this.toolStripToolBox.Text = "ToolBox";
            // 
            // toolStripButtonOpenSql
            // 
            this.toolStripButtonOpenSql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenSql.Image = global::GrabDatabase.Properties.Resources.openHS;
            this.toolStripButtonOpenSql.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenSql.Name = "toolStripButtonOpenSql";
            this.toolStripButtonOpenSql.Size = new System.Drawing.Size(23, 17);
            this.toolStripButtonOpenSql.Text = "Open Sql";
            this.toolStripButtonOpenSql.Click += new System.EventHandler(this.toolStripButtonOpenSql_Click);
            // 
            // toolStripButtonSaveSql
            // 
            this.toolStripButtonSaveSql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveSql.Image = global::GrabDatabase.Properties.Resources.saveHS;
            this.toolStripButtonSaveSql.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveSql.Name = "toolStripButtonSaveSql";
            this.toolStripButtonSaveSql.Size = new System.Drawing.Size(23, 17);
            this.toolStripButtonSaveSql.Text = "Save Sql";
            this.toolStripButtonSaveSql.Click += new System.EventHandler(this.toolStripButtonSaveSql_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 20);
            // 
            // toolStripButtonSelectQueryHelp
            // 
            this.toolStripButtonSelectQueryHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelectQueryHelp.Image = global::GrabDatabase.Properties.Resources.filter1;
            this.toolStripButtonSelectQueryHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectQueryHelp.Name = "toolStripButtonSelectQueryHelp";
            this.toolStripButtonSelectQueryHelp.Size = new System.Drawing.Size(23, 17);
            this.toolStripButtonSelectQueryHelp.Text = "Select Query Help";
            this.toolStripButtonSelectQueryHelp.Click += new System.EventHandler(this.toolStripButtonSelectQueryHelp_Click);
            // 
            // toolStripButtonInsertQueryHelp
            // 
            this.toolStripButtonInsertQueryHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInsertQueryHelp.Image = global::GrabDatabase.Properties.Resources.add_1;
            this.toolStripButtonInsertQueryHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInsertQueryHelp.Name = "toolStripButtonInsertQueryHelp";
            this.toolStripButtonInsertQueryHelp.Size = new System.Drawing.Size(23, 17);
            this.toolStripButtonInsertQueryHelp.Text = "Insert Query Help";
            this.toolStripButtonInsertQueryHelp.Click += new System.EventHandler(this.toolStripButtonInsertQueryHelp_Click);
            // 
            // toolStripButtonUpdateQueryHelp
            // 
            this.toolStripButtonUpdateQueryHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUpdateQueryHelp.Image = global::GrabDatabase.Properties.Resources.refresh1;
            this.toolStripButtonUpdateQueryHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpdateQueryHelp.Name = "toolStripButtonUpdateQueryHelp";
            this.toolStripButtonUpdateQueryHelp.Size = new System.Drawing.Size(23, 17);
            this.toolStripButtonUpdateQueryHelp.Text = "Update Query Help";
            this.toolStripButtonUpdateQueryHelp.Click += new System.EventHandler(this.toolStripButtonUpdateQueryHelp_Click);
            // 
            // toolStripButtonDeleteQueryHelp
            // 
            this.toolStripButtonDeleteQueryHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDeleteQueryHelp.Image = global::GrabDatabase.Properties.Resources.Delete;
            this.toolStripButtonDeleteQueryHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteQueryHelp.Name = "toolStripButtonDeleteQueryHelp";
            this.toolStripButtonDeleteQueryHelp.Size = new System.Drawing.Size(23, 17);
            this.toolStripButtonDeleteQueryHelp.Text = "Delete Query Help";
            this.toolStripButtonDeleteQueryHelp.Click += new System.EventHandler(this.toolStripButtonDeleteQueryHelp_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 20);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(67, 17);
            this.toolStripLabel1.Text = "Max. Items:";
            // 
            // toolStripTextBoxMaxItemsCount
            // 
            this.toolStripTextBoxMaxItemsCount.Name = "toolStripTextBoxMaxItemsCount";
            this.toolStripTextBoxMaxItemsCount.Size = new System.Drawing.Size(50, 20);
            this.toolStripTextBoxMaxItemsCount.TextChanged += new System.EventHandler(this.toolStripTextBoxMaxItemsCount_TextChanged);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 20);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripButtonUpdateDbData
            // 
            this.toolStripButtonUpdateDbData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUpdateDbData.Image = global::GrabDatabase.Properties.Resources.refresh;
            this.toolStripButtonUpdateDbData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpdateDbData.Name = "toolStripButtonUpdateDbData";
            this.toolStripButtonUpdateDbData.Size = new System.Drawing.Size(23, 17);
            this.toolStripButtonUpdateDbData.Text = "Read Db Data";
            this.toolStripButtonUpdateDbData.Click += new System.EventHandler(this.toolStripButtonUpdateDbData_Click);
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(71, 36);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.BeforeShowMenu += new DevExpress.XtraEditors.Controls.BeforeShowMenuEventHandler(this.memoEdit1_BeforeShowMenu);
            this.memoEdit1.Size = new System.Drawing.Size(396, 113);
            this.memoEdit1.StyleController = this.layoutControl1;
            this.memoEdit1.TabIndex = 1;
            this.memoEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.memoEdit1_KeyPress);
            // 
            // gridControlResult
            // 
            this.gridControlResult.Location = new System.Drawing.Point(12, 153);
            this.gridControlResult.MainView = this.gridViewResult;
            this.gridControlResult.Name = "gridControlResult";
            this.gridControlResult.Size = new System.Drawing.Size(826, 276);
            this.gridControlResult.TabIndex = 2;
            this.gridControlResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewResult});
            // 
            // gridViewResult
            // 
            this.gridViewResult.GridControl = this.gridControlResult;
            this.gridViewResult.Name = "gridViewResult";
            this.gridViewResult.OptionsView.ColumnAutoWidth = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemSqlQuery_,
            this.layoutControlItemGrid,
            this.layoutControlItemToolStrip,
            this.layoutControlItemDbEntity});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(850, 441);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemSqlQuery_
            // 
            this.layoutControlItemSqlQuery_.Control = this.memoEdit1;
            this.layoutControlItemSqlQuery_.CustomizationFormText = "Sql Query:";
            this.layoutControlItemSqlQuery_.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemSqlQuery_.Name = "layoutControlItemSqlQuery_";
            this.layoutControlItemSqlQuery_.Size = new System.Drawing.Size(459, 117);
            this.layoutControlItemSqlQuery_.Text = "SQL Query:";
            this.layoutControlItemSqlQuery_.TextSize = new System.Drawing.Size(56, 13);
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.gridControlResult;
            this.layoutControlItemGrid.CustomizationFormText = "layoutControlItemGrid";
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 141);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(830, 280);
            this.layoutControlItemGrid.Text = "layoutControlItemGrid";
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextToControlDistance = 0;
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // layoutControlItemToolStrip
            // 
            this.layoutControlItemToolStrip.Control = this.toolStripToolBox;
            this.layoutControlItemToolStrip.CustomizationFormText = "layoutControlItemToolStrip";
            this.layoutControlItemToolStrip.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemToolStrip.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItemToolStrip.MinSize = new System.Drawing.Size(201, 24);
            this.layoutControlItemToolStrip.Name = "layoutControlItemToolStrip";
            this.layoutControlItemToolStrip.Size = new System.Drawing.Size(830, 24);
            this.layoutControlItemToolStrip.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemToolStrip.Text = "layoutControlItemToolStrip";
            this.layoutControlItemToolStrip.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemToolStrip.TextToControlDistance = 0;
            this.layoutControlItemToolStrip.TextVisible = false;
            // 
            // layoutControlItemDbEntity
            // 
            this.layoutControlItemDbEntity.Control = this.gridControlDbEntity;
            this.layoutControlItemDbEntity.CustomizationFormText = "layoutControlItemDbEntity";
            this.layoutControlItemDbEntity.Location = new System.Drawing.Point(459, 24);
            this.layoutControlItemDbEntity.Name = "layoutControlItemDbEntity";
            this.layoutControlItemDbEntity.Size = new System.Drawing.Size(371, 117);
            this.layoutControlItemDbEntity.Text = "layoutControlItemDbEntity";
            this.layoutControlItemDbEntity.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemDbEntity.TextToControlDistance = 0;
            this.layoutControlItemDbEntity.TextVisible = false;
            // 
            // SqlCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 441);
            this.Controls.Add(this.layoutControl1);
            this.Name = "SqlCommand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Query";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDbEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDbEntity)).EndInit();
            this.toolStripToolBox.ResumeLayout(false);
            this.toolStripToolBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSqlQuery_)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToolStrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDbEntity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSqlQuery_;
        private DevExpress.XtraGrid.GridControl gridControlResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewResult;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private System.Windows.Forms.ToolStrip toolStripToolBox;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveSql;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectQueryHelp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonInsertQueryHelp;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpdateQueryHelp;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteQueryHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMaxItemsCount;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenSql;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpdateDbData;
        private DevExpress.XtraGrid.GridControl gridControlDbEntity;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDbEntity;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDbEntity;
    }
}