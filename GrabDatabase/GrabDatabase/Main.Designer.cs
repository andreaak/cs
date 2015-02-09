namespace GrabDatabase
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemSelectConnection = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonConnect = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSubItem7 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem9 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemCreateTables = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGrabMetaData = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemGrabText = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem10 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemImportTables = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemReadTables = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSaveTables = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLoadLinks = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCreateLinksFromKeys = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDeleteLink = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemPrintLinks = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFindField = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFindLink = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemReadTablesData = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSqlCommand = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemParseQuery = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemHelp = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Books";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 53);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1047, 610);
            this.gridControl1.TabIndex = 9;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Visible = false;
            this.gridControl1.ViewRegistered += new DevExpress.XtraGrid.ViewOperationEventHandler(this.gridControl1_ViewRegistered);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.barSubItem7,
            this.barButtonConnect,
            this.barButtonItemGrabText,
            this.barSubItem9,
            this.barButtonItemCreateTables,
            this.barSubItem10,
            this.barButtonItemImportTables,
            this.barButtonItemGrabMetaData,
            this.barButtonItemDeleteLink,
            this.barButtonItemSaveTables,
            this.barButtonItemReadTables,
            this.barButtonItemFindField,
            this.barButtonItemCreateLinksFromKeys,
            this.barButtonItemFindLink,
            this.barButtonItemLoadLinks,
            this.barButtonItemPrintLinks,
            this.barButtonItemSqlCommand,
            this.barButtonItemParseQuery,
            this.barSubItem2,
            this.barSubItem3,
            this.barButtonItemHelp,
            this.barButtonItemReadTablesData,
            this.barButtonItemSelectConnection});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 85;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSelectConnection, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonConnect)});
            this.bar1.Text = "Tools";
            // 
            // barButtonItemSelectConnection
            // 
            this.barButtonItemSelectConnection.Caption = "Select Connection";
            this.barButtonItemSelectConnection.Id = 84;
            this.barButtonItemSelectConnection.ImageIndex = 9;
            this.barButtonItemSelectConnection.ImageIndexDisabled = 9;
            this.barButtonItemSelectConnection.Name = "barButtonItemSelectConnection";
            toolTipItem1.Text = "Select Connection";
            superToolTip1.Items.Add(toolTipItem1);
            this.barButtonItemSelectConnection.SuperTip = superToolTip1;
            this.barButtonItemSelectConnection.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSelectConnection_ItemClick);
            // 
            // barButtonConnect
            // 
            this.barButtonConnect.Caption = "Connect";
            this.barButtonConnect.Id = 44;
            this.barButtonConnect.ImageIndex = 7;
            this.barButtonConnect.ImageIndexDisabled = 7;
            this.barButtonConnect.Name = "barButtonConnect";
            toolTipItem2.Text = "Connect";
            superToolTip2.Items.Add(toolTipItem2);
            this.barButtonConnect.SuperTip = superToolTip2;
            this.barButtonConnect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonConnect_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem7),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem9),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem10),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSubItem7
            // 
            this.barSubItem7.Caption = "Service";
            this.barSubItem7.Id = 43;
            this.barSubItem7.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSelectConnection),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonConnect)});
            this.barSubItem7.Name = "barSubItem7";
            // 
            // barSubItem9
            // 
            this.barSubItem9.Caption = "Grab Database";
            this.barSubItem9.Id = 55;
            this.barSubItem9.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCreateTables),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGrabMetaData),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemGrabText)});
            this.barSubItem9.Name = "barSubItem9";
            // 
            // barButtonItemCreateTables
            // 
            this.barButtonItemCreateTables.Caption = "Create Tables...";
            this.barButtonItemCreateTables.Id = 56;
            this.barButtonItemCreateTables.Name = "barButtonItemCreateTables";
            this.barButtonItemCreateTables.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCreateTables_ItemClick);
            // 
            // barButtonItemGrabMetaData
            // 
            this.barButtonItemGrabMetaData.Caption = "Grab Metadata...";
            this.barButtonItemGrabMetaData.Id = 60;
            this.barButtonItemGrabMetaData.Name = "barButtonItemGrabMetaData";
            toolTipItem3.Text = "Grab data from metadata collection";
            superToolTip3.Items.Add(toolTipItem3);
            this.barButtonItemGrabMetaData.SuperTip = superToolTip3;
            this.barButtonItemGrabMetaData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGrabData_ItemClick);
            // 
            // barButtonItemGrabText
            // 
            this.barButtonItemGrabText.Caption = "Grab Text...";
            this.barButtonItemGrabText.Id = 54;
            this.barButtonItemGrabText.Name = "barButtonItemGrabText";
            this.barButtonItemGrabText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemGrabText_ItemClick);
            // 
            // barSubItem10
            // 
            this.barSubItem10.Caption = "Link";
            this.barSubItem10.Id = 57;
            this.barSubItem10.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemImportTables),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemReadTables, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSaveTables),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemLoadLinks, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemCreateLinksFromKeys),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemDeleteLink),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemPrintLinks),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemFindField, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemFindLink)});
            this.barSubItem10.Name = "barSubItem10";
            // 
            // barButtonItemImportTables
            // 
            this.barButtonItemImportTables.Caption = "Import Tables...";
            this.barButtonItemImportTables.Id = 58;
            this.barButtonItemImportTables.Name = "barButtonItemImportTables";
            this.barButtonItemImportTables.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemImportTables_ItemClick);
            // 
            // barButtonItemReadTables
            // 
            this.barButtonItemReadTables.Caption = "Read Tables";
            this.barButtonItemReadTables.Id = 63;
            this.barButtonItemReadTables.Name = "barButtonItemReadTables";
            this.barButtonItemReadTables.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemReadLinks_ItemClick);
            // 
            // barButtonItemSaveTables
            // 
            this.barButtonItemSaveTables.Caption = "Save Tables";
            this.barButtonItemSaveTables.Id = 62;
            this.barButtonItemSaveTables.Name = "barButtonItemSaveTables";
            this.barButtonItemSaveTables.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSaveLink_ItemClick);
            // 
            // barButtonItemLoadLinks
            // 
            this.barButtonItemLoadLinks.Caption = "Load Links";
            this.barButtonItemLoadLinks.Id = 67;
            this.barButtonItemLoadLinks.Name = "barButtonItemLoadLinks";
            this.barButtonItemLoadLinks.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemLoadLinks_ItemClick);
            // 
            // barButtonItemCreateLinksFromKeys
            // 
            this.barButtonItemCreateLinksFromKeys.Caption = "Create Links From Keys...";
            this.barButtonItemCreateLinksFromKeys.Id = 65;
            this.barButtonItemCreateLinksFromKeys.Name = "barButtonItemCreateLinksFromKeys";
            this.barButtonItemCreateLinksFromKeys.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemCreateLinksFromKeys_ItemClick);
            // 
            // barButtonItemDeleteLink
            // 
            this.barButtonItemDeleteLink.Caption = "Delete Link...";
            this.barButtonItemDeleteLink.Id = 61;
            this.barButtonItemDeleteLink.Name = "barButtonItemDeleteLink";
            this.barButtonItemDeleteLink.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemDeleteLink_ItemClick);
            // 
            // barButtonItemPrintLinks
            // 
            this.barButtonItemPrintLinks.Caption = "Print Links";
            this.barButtonItemPrintLinks.Id = 68;
            this.barButtonItemPrintLinks.Name = "barButtonItemPrintLinks";
            this.barButtonItemPrintLinks.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemPrintLinks_ItemClick);
            // 
            // barButtonItemFindField
            // 
            this.barButtonItemFindField.Caption = "Find Field...";
            this.barButtonItemFindField.Id = 64;
            this.barButtonItemFindField.Name = "barButtonItemFindField";
            this.barButtonItemFindField.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemFindField_ItemClick);
            // 
            // barButtonItemFindLink
            // 
            this.barButtonItemFindLink.Caption = "Find Link...";
            this.barButtonItemFindLink.Id = 66;
            this.barButtonItemFindLink.Name = "barButtonItemFindLink";
            this.barButtonItemFindLink.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemFindLink_ItemClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "Query";
            this.barSubItem2.Id = 80;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemReadTablesData),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSqlCommand),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemParseQuery)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barButtonItemReadTablesData
            // 
            this.barButtonItemReadTablesData.Caption = "Read Table\'s Data";
            this.barButtonItemReadTablesData.Id = 83;
            this.barButtonItemReadTablesData.Name = "barButtonItemReadTablesData";
            this.barButtonItemReadTablesData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemReadTablesData_ItemClick);
            // 
            // barButtonItemSqlCommand
            // 
            this.barButtonItemSqlCommand.Caption = "SQL Command...";
            this.barButtonItemSqlCommand.Id = 77;
            this.barButtonItemSqlCommand.Name = "barButtonItemSqlCommand";
            this.barButtonItemSqlCommand.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSqlCommand_ItemClick);
            // 
            // barButtonItemParseQuery
            // 
            this.barButtonItemParseQuery.Caption = "Parse Query...";
            this.barButtonItemParseQuery.Id = 79;
            this.barButtonItemParseQuery.Name = "barButtonItemParseQuery";
            this.barButtonItemParseQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemParseQuery_ItemClick);
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "Help";
            this.barSubItem3.Id = 81;
            this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemHelp)});
            this.barSubItem3.Name = "barSubItem3";
            // 
            // barButtonItemHelp
            // 
            this.barButtonItemHelp.Caption = "Edit...";
            this.barButtonItemHelp.Id = 82;
            this.barButtonItemHelp.Name = "barButtonItemHelp";
            this.barButtonItemHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemHelp_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1047, 53);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 663);
            this.barDockControlBottom.Size = new System.Drawing.Size(1047, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 53);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 610);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1047, 53);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 610);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "refresh.png");
            this.imageList1.Images.SetKeyName(1, "add_.png");
            this.imageList1.Images.SetKeyName(2, "Delete.png");
            this.imageList1.Images.SetKeyName(3, "paste.png");
            this.imageList1.Images.SetKeyName(4, "write.png");
            this.imageList1.Images.SetKeyName(5, "display.png");
            this.imageList1.Images.SetKeyName(6, "black_list_1.png");
            this.imageList1.Images.SetKeyName(7, "Settings.png");
            this.imageList1.Images.SetKeyName(8, "saveHS.png");
            this.imageList1.Images.SetKeyName(9, "database-refresh.png");
            this.imageList1.Images.SetKeyName(10, "database-add.png");
            // 
            // barSubItem1
            // 
            this.barSubItem1.Id = 78;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Grab Database";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 686);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grab Database";
            this.SizeChanged += new System.EventHandler(this.Form_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarSubItem barSubItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonConnect;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGrabText;
        private DevExpress.XtraBars.BarSubItem barSubItem9;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCreateTables;
        private DevExpress.XtraBars.BarSubItem barSubItem10;
        private DevExpress.XtraBars.BarButtonItem barButtonItemImportTables;
        private DevExpress.XtraBars.BarButtonItem barButtonItemGrabMetaData;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDeleteLink;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSaveTables;
        private DevExpress.XtraBars.BarButtonItem barButtonItemReadTables;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFindField;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCreateLinksFromKeys;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFindLink;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLoadLinks;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPrintLinks;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSqlCommand;
        private DevExpress.XtraBars.BarButtonItem barButtonItemParseQuery;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItemHelp;
        private DevExpress.XtraBars.BarButtonItem barButtonItemReadTablesData;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSelectConnection;
    }
}