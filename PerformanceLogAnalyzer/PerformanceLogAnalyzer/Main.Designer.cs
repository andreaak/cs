using DevExpress.Utils;
namespace PerformanceLogAnalyzer
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
            this.toolStripButtonTimeSpan = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowService = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowLegend = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowAxysYLabels = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxFrom = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxTo = new System.Windows.Forms.ToolStripTextBox();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
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
            this.toolStripButtonShowService,
            this.toolStripButtonShowAxysYLabels,
            this.toolStripButtonShowLegend,
            this.toolStripLabel1,
            this.toolStripTextBoxFrom,
            this.toolStripLabel2,
            this.toolStripTextBoxTo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1035, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = global::PerformanceLogAnalyzer.Properties.Resources.openHS;
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
            this.toolStripButtonShowLabels.Image = global::PerformanceLogAnalyzer.Properties.Resources.wi0037;
            this.toolStripButtonShowLabels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowLabels.Name = "toolStripButtonShowLabels";
            this.toolStripButtonShowLabels.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowLabels.Text = "Show Labels";
            this.toolStripButtonShowLabels.CheckedChanged += new System.EventHandler(this.toolStripButtonShowLabels_CheckedChanged);
            // 
            // toolStripButtonTimeSpan
            // 
            this.toolStripButtonTimeSpan.CheckOnClick = true;
            this.toolStripButtonTimeSpan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTimeSpan.Image = global::PerformanceLogAnalyzer.Properties.Resources.wi1051;
            this.toolStripButtonTimeSpan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTimeSpan.Name = "toolStripButtonTimeSpan";
            this.toolStripButtonTimeSpan.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonTimeSpan.Text = "Time Span";
            // 
            // toolStripButtonShowService
            // 
            this.toolStripButtonShowService.CheckOnClick = true;
            this.toolStripButtonShowService.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowService.Image = global::PerformanceLogAnalyzer.Properties.Resources.wi0035;
            this.toolStripButtonShowService.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowService.Name = "toolStripButtonShowService";
            this.toolStripButtonShowService.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowService.Text = "Show Service";
            // 
            // toolStripButtonShowLegend
            // 
            this.toolStripButtonShowLegend.Checked = true;
            this.toolStripButtonShowLegend.CheckOnClick = true;
            this.toolStripButtonShowLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonShowLegend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowLegend.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowLegend.Image")));
            this.toolStripButtonShowLegend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowLegend.Name = "toolStripButtonShowLegend";
            this.toolStripButtonShowLegend.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowLegend.Text = "Show Legend";
            this.toolStripButtonShowLegend.Click += new System.EventHandler(this.toolStripButtonShowLegend_Click);
            // 
            // toolStripButtonShowAxysYLabels
            // 
            this.toolStripButtonShowAxysYLabels.Checked = true;
            this.toolStripButtonShowAxysYLabels.CheckOnClick = true;
            this.toolStripButtonShowAxysYLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonShowAxysYLabels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowAxysYLabels.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowAxysYLabels.Image")));
            this.toolStripButtonShowAxysYLabels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowAxysYLabels.Name = "toolStripButtonShowAxysYLabels";
            this.toolStripButtonShowAxysYLabels.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowAxysYLabels.Text = "Show AxysY Labels";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(38, 22);
            this.toolStripLabel1.Text = "From:";
            // 
            // toolStripTextBoxFrom
            // 
            this.toolStripTextBoxFrom.Name = "toolStripTextBoxFrom";
            this.toolStripTextBoxFrom.Size = new System.Drawing.Size(50, 25);
            this.toolStripTextBoxFrom.Validated += new System.EventHandler(this.toolStripTextBoxFrom_Validated);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(24, 22);
            this.toolStripLabel2.Text = "To:";
            // 
            // toolStripTextBoxTo
            // 
            this.toolStripTextBoxTo.Name = "toolStripTextBoxTo";
            this.toolStripTextBoxTo.Size = new System.Drawing.Size(50, 25);
            this.toolStripTextBoxTo.Validated += new System.EventHandler(this.toolStripTextBoxFrom_Validated);
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
            ganttDiagram1.EnableAxisXScrolling = true;
            ganttDiagram1.EnableAxisYScrolling = true;
            ganttDiagram1.EnableAxisYZooming = true;
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
            overlappedGanttSeriesView1.MaxValueMarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
            overlappedGanttSeriesView1.MinValueMarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.View = overlappedGanttSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.SeriesTemplate.Label = rangeBarSeriesLabel2;
            overlappedGanttSeriesView2.MaxValueMarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
            overlappedGanttSeriesView2.MinValueMarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.SeriesTemplate.View = overlappedGanttSeriesView2;
            this.chartControl1.Size = new System.Drawing.Size(1035, 769);
            this.chartControl1.TabIndex = 1;
            this.chartControl1.CustomDrawCrosshair += new DevExpress.XtraCharts.CustomDrawCrosshairEventHandler(this.chartControl1_CustomDrawCrosshair);
            this.chartControl1.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chartControl1_CustomDrawSeriesPoint);
            this.chartControl1.CustomDrawAxisLabel += new DevExpress.XtraCharts.CustomDrawAxisLabelEventHandler(this.chartControl1_CustomDrawAxisLabel);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Performance Log Analyzer";
            this.notifyIcon1.BalloonTipTitle = "Performance Log Analyzer";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Performance Log Analyzer";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 794);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Performance Log Analyzer";
            this.SizeChanged += new System.EventHandler(this.Form_SizeChanged);
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
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowLegend;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowAxysYLabels;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxFrom;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxTo;

    }
}

