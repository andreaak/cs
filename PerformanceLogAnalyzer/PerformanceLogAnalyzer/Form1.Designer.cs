using DevExpress.Utils;
namespace EventDisplay
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DevExpress.XtraCharts.GanttDiagram ganttDiagram1 = new DevExpress.XtraCharts.GanttDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.RangeBarSeriesLabel rangeBarSeriesLabel1 = new DevExpress.XtraCharts.RangeBarSeriesLabel();
            DevExpress.XtraCharts.RangeBarPointOptions rangeBarPointOptions1 = new DevExpress.XtraCharts.RangeBarPointOptions();
            DevExpress.XtraCharts.OverlappedGanttSeriesView overlappedGanttSeriesView1 = new DevExpress.XtraCharts.OverlappedGanttSeriesView();
            DevExpress.XtraCharts.RangeBarSeriesLabel rangeBarSeriesLabel2 = new DevExpress.XtraCharts.RangeBarSeriesLabel();
            DevExpress.XtraCharts.OverlappedGanttSeriesView overlappedGanttSeriesView2 = new DevExpress.XtraCharts.OverlappedGanttSeriesView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowLabels = new System.Windows.Forms.ToolStripButton();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.toolStripButtonTimeSpan = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowService = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(ganttDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView2)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen,
            this.toolStripButtonShowLabels,
            this.toolStripButtonTimeSpan,
            this.toolStripButtonShowService});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(586, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.Text = "Open";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripButtonShowLabels
            // 
            this.toolStripButtonShowLabels.CheckOnClick = true;
            this.toolStripButtonShowLabels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowLabels.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowLabels.Image")));
            this.toolStripButtonShowLabels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowLabels.Name = "toolStripButtonShowLabels";
            this.toolStripButtonShowLabels.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowLabels.Text = "Show Labels";
            this.toolStripButtonShowLabels.CheckedChanged += new System.EventHandler(this.toolStripButtonShowLabels_CheckedChanged);
            // 
            // chartControl1
            // 
            this.chartControl1.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            ganttDiagram1.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
            ganttDiagram1.AxisX.Range.SideMarginsEnabled = true;
            ganttDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            ganttDiagram1.AxisY.CrosshairAxisLabelOptions.Pattern = "{A:F0}";
            ganttDiagram1.AxisY.DateTimeGridAlignment = DevExpress.XtraCharts.DateTimeMeasurementUnit.Millisecond;
            ganttDiagram1.AxisY.DateTimeMeasureUnit = DevExpress.XtraCharts.DateTimeMeasurementUnit.Millisecond;
            ganttDiagram1.AxisY.DateTimeOptions.Format = DevExpress.XtraCharts.DateTimeFormat.Custom;
            ganttDiagram1.AxisY.GridSpacing = 100D;
            ganttDiagram1.AxisY.GridSpacingAuto = false;
            ganttDiagram1.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
            ganttDiagram1.AxisY.Range.SideMarginsEnabled = true;
            ganttDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            //ganttDiagram1.EnableAxisXZooming = true;
            ganttDiagram1.EnableAxisXScrolling = true;
            ganttDiagram1.EnableAxisYZooming = true;
            ganttDiagram1.EnableAxisYScrolling = true;
            this.chartControl1.Diagram = ganttDiagram1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Location = new System.Drawing.Point(0, 25);
            this.chartControl1.Name = "chartControl1";
            series1.CrosshairLabelPattern = "{V1:T} {V2:T}";
            series1.Label = rangeBarSeriesLabel1;
            rangeBarPointOptions1.ValueDateTimeOptions.Format = DevExpress.XtraCharts.DateTimeFormat.LongTime;
            series1.LegendPointOptions = rangeBarPointOptions1;
            series1.Name = "Series 1";
            series1.SynchronizePointOptions = false;
            series1.ValueScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            series1.View = overlappedGanttSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.SeriesTemplate.Label = rangeBarSeriesLabel2;
            this.chartControl1.SeriesTemplate.View = overlappedGanttSeriesView2;
            this.chartControl1.Size = new System.Drawing.Size(586, 345);
            this.chartControl1.TabIndex = 1;
            this.chartControl1.CustomDrawCrosshair += new DevExpress.XtraCharts.CustomDrawCrosshairEventHandler(this.chartControl1_CustomDrawCrosshair);
            this.chartControl1.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chartControl1_CustomDrawSeriesPoint);
            this.chartControl1.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chartControl1_CustomDrawAxisLabel);
            // 
            // toolStripButtonTimeSpan
            // 
            this.toolStripButtonTimeSpan.CheckOnClick = true;
            this.toolStripButtonTimeSpan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTimeSpan.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTimeSpan.Image")));
            this.toolStripButtonTimeSpan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTimeSpan.Name = "toolStripButtonTimeSpan";
            this.toolStripButtonTimeSpan.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonTimeSpan.Text = "Time Span";
            // 
            // toolStripButtonShowService
            // 
            this.toolStripButtonShowService.CheckOnClick = true;
            this.toolStripButtonShowService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowService.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowService.Image")));
            this.toolStripButtonShowService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowService.Name = "toolStripButtonShowService";
            this.toolStripButtonShowService.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowService.Text = "Show Service";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 370);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(ganttDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(rangeBarSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(overlappedGanttSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowLabels;
        private System.Windows.Forms.ToolStripButton toolStripButtonTimeSpan;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowService;

    }
}

