namespace Note
{
    partial class Logs
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.logDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEntityID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEntityDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.logDataBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(697, 365);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // logDataBindingSource
            // 
            this.logDataBindingSource.DataSource = typeof(Note.Domain.Repository.LogData);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEntityID,
            this.colOperation,
            this.colEntityDescription,
            this.colModDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colEntityID
            // 
            this.colEntityID.FieldName = "EntityID";
            this.colEntityID.Name = "colEntityID";
            this.colEntityID.OptionsColumn.ReadOnly = true;
            this.colEntityID.Visible = true;
            this.colEntityID.VisibleIndex = 0;
            // 
            // colOperation
            // 
            this.colOperation.FieldName = "Operation";
            this.colOperation.Name = "colOperation";
            this.colOperation.OptionsColumn.ReadOnly = true;
            this.colOperation.Visible = true;
            this.colOperation.VisibleIndex = 1;
            // 
            // colEntityDescription
            // 
            this.colEntityDescription.FieldName = "EntityDescription";
            this.colEntityDescription.Name = "colEntityDescription";
            this.colEntityDescription.Visible = true;
            this.colEntityDescription.VisibleIndex = 2;
            // 
            // colModDate
            // 
            this.colModDate.DisplayFormat.FormatString = "g";
            this.colModDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colModDate.FieldName = "ModDate";
            this.colModDate.Name = "colModDate";
            this.colModDate.OptionsColumn.ReadOnly = true;
            this.colModDate.Visible = true;
            this.colModDate.VisibleIndex = 3;
            // 
            // Logs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 365);
            this.Controls.Add(this.gridControl1);
            this.Name = "Logs";
            this.Text = "Logs";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource logDataBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colEntityID;
        private DevExpress.XtraGrid.Columns.GridColumn colOperation;
        private DevExpress.XtraGrid.Columns.GridColumn colEntityDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colModDate;
    }
}