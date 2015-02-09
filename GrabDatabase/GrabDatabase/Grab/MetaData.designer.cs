namespace GrabDatabase
{
    partial class MetaData
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
            this.rtLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButtonSaveData = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textEditOwner = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonShow = new DevExpress.XtraEditors.SimpleButton();
            this.checkBoxGetScheme = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.rtLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOwner.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // rtLookUpEdit
            // 
            this.rtLookUpEdit.Location = new System.Drawing.Point(52, 12);
            this.rtLookUpEdit.Name = "rtLookUpEdit";
            this.rtLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rtLookUpEdit.Properties.NullText = "";
            this.rtLookUpEdit.Size = new System.Drawing.Size(279, 20);
            this.rtLookUpEdit.TabIndex = 16;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(26, 13);
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "Item:";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(824, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 468);
            this.barDockControlBottom.Size = new System.Drawing.Size(824, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 468);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(824, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 468);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(0, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(826, 430);
            this.gridControl1.TabIndex = 23;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // simpleButtonSaveData
            // 
            this.simpleButtonSaveData.Location = new System.Drawing.Point(569, 9);
            this.simpleButtonSaveData.Name = "simpleButtonSaveData";
            this.simpleButtonSaveData.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonSaveData.TabIndex = 22;
            this.simpleButtonSaveData.Text = "Save Data";
            this.simpleButtonSaveData.Click += new System.EventHandler(this.simpleButtonSaveData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(338, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Owner:";
            // 
            // textEditOwner
            // 
            this.textEditOwner.Location = new System.Drawing.Point(387, 12);
            this.textEditOwner.MenuManager = this.barManager1;
            this.textEditOwner.Name = "textEditOwner";
            this.textEditOwner.Size = new System.Drawing.Size(95, 20);
            this.textEditOwner.TabIndex = 29;
            // 
            // simpleButtonShow
            // 
            this.simpleButtonShow.Location = new System.Drawing.Point(488, 9);
            this.simpleButtonShow.Name = "simpleButtonShow";
            this.simpleButtonShow.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonShow.TabIndex = 22;
            this.simpleButtonShow.Text = "Show";
            this.simpleButtonShow.Click += new System.EventHandler(this.simpleButtonShow_Click);
            // 
            // checkBoxGetScheme
            // 
            this.checkBoxGetScheme.AutoSize = true;
            this.checkBoxGetScheme.Location = new System.Drawing.Point(651, 14);
            this.checkBoxGetScheme.Name = "checkBoxGetScheme";
            this.checkBoxGetScheme.Size = new System.Drawing.Size(83, 17);
            this.checkBoxGetScheme.TabIndex = 34;
            this.checkBoxGetScheme.Text = "Get Scheme";
            this.checkBoxGetScheme.UseVisualStyleBackColor = true;
            this.checkBoxGetScheme.Visible = false;
            // 
            // MetaData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 468);
            this.Controls.Add(this.checkBoxGetScheme);
            this.Controls.Add(this.textEditOwner);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.simpleButtonShow);
            this.Controls.Add(this.simpleButtonSaveData);
            this.Controls.Add(this.rtLookUpEdit);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MetaData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metadata";
            this.SizeChanged += new System.EventHandler(this.GetDbData_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.rtLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditOwner.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit rtLookUpEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSaveData;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEditOwner;
        private DevExpress.XtraEditors.SimpleButton simpleButtonShow;
        private System.Windows.Forms.CheckBox checkBoxGetScheme;
    }
}