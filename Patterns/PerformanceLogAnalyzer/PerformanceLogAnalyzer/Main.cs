using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;

namespace PerformanceLogAnalyzer
{
    public partial class Main : Form
    {
        Dictionary<string, Color> threadColors;
        Color[] colors = { Color.Red, Color.Black, Color.Green, Color.Lime, Color.Yellow, Color.Violet, Color.DarkGray, Color.Brown, Color.DarkKhaki, Color.Cyan };
        int colorIndex;
        const string TEXT = "Event Display";
        bool isShowWithShift; 
        private DateTime startDate;

        public Main()
        {
            InitializeComponent();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFileDlg = new OpenFileDialog();
            oFileDlg.Filter = "Log Files (*.log)|*.log|" + "TXT Files (*.txt)|*.txt|" + "All Files (*.*)|*.*";
            if (oFileDlg.ShowDialog() == DialogResult.OK && oFileDlg.FileName.Length > 0)
            {
                Parser.Parse(oFileDlg.FileName);
                FillDiagram(Parser.Events, Parser.Points);
                Text = TEXT + " " + oFileDlg.FileName;
            }
        }

        private void toolStripButtonShowLabels_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Series series in chartControl1.Series)
            {
                series.LabelsVisibility = toolStripButtonShowLabels.Checked ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            }
        }
        
        private void toolStripButtonShowLegend_Click(object sender, EventArgs e)
        {
            chartControl1.Legend.Visible = toolStripButtonShowLegend.Checked;
        }

        private void toolStripTextBoxFrom_Validated(object sender, EventArgs e)
        {
            if (Parser.Events == null || Parser.Points == null)
            {
                return;
            }
            int from = 0;
            int to = Parser.Events.Count - 1;
            if ((TryGetRange(toolStripTextBoxFrom.Text, ref from)
                | TryGetRange(toolStripTextBoxTo.Text, ref to))
                && from < to)
            {
                IEnumerable<Event> tmp = Parser.Events.Skip(from).Take(to - from + 1).ToList();
                DateTime start = tmp.Min(item => item.StartDate);

                FillDiagram(tmp, Parser.Points.Where(item => item.Date >= start));
            }
            else
            {
                FillDiagram(Parser.Events, Parser.Points);
            }
        }

        private void chartControl1_CustomDrawAxisLabel(object sender, DevExpress.XtraCharts.CustomDrawAxisLabelEventArgs e)
        {
            if (e.Item.Axis.GetType() == typeof(GanttAxisX))
            {
                e.Item.Text = toolStripButtonShowAxysYLabels.Checked ? GetArgument(e.Item.Text) : "";
            }
            else
            {
                DateTime dt = (DateTime)e.Item.AxisValue;
                if (toolStripButtonTimeSpan.Checked)
                {
                    TimeSpan interval = dt - startDate;
                    e.Item.Text = interval.ToString("mm\\:ss\\.fff");
                }
                else
                {
                    e.Item.Text = string.Format("{0}:{1}.{2}", dt.Minute, dt.Second, dt.Millisecond);
                }
            }
        }

        private void chartControl1_CustomDrawSeriesPoint(object sender, DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs e)
        {
            e.LabelText = string.Empty;
            e.SecondLabelText = GetDescription(e.SeriesPoint, true);
            e.SeriesDrawOptions.Color = GetColor(e.SeriesPoint.Argument, e.SeriesDrawOptions.Color);
        }

        private void chartControl1_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            var element = e.CrosshairElements.FirstOrDefault();
            if(element != null)
            {
                element.LabelElement.Text = GetDescription(element.SeriesPoint, true);
                element.LabelElement.MarkerColor = GetColor(element.SeriesPoint.Argument, element.LabelElement.MarkerColor);
            }
        }

        private bool TryGetRange(string text, ref int value)
        {
            int temp;
            if (int.TryParse(text, out temp) && temp > 0)
            {
                value = temp;
                return true;
            }
            return false;
        }

        private void FillDiagram(IEnumerable<Event> list, IEnumerable<TimePoint> timePoints)
        {
            chartControl1.BeginInit();

            ClearSeries();
            ClearTimePoints();

            FillSeries(list);
            FillTimePoints(timePoints);

            chartControl1.Legend.Visible = toolStripButtonShowLegend.Checked;

            chartControl1.EndInit();
            
            Event firstEvent = list.FirstOrDefault();
            if(firstEvent != null)
            {
                startDate = firstEvent.StartDate;
            }
        }

        private void ClearSeries()
        {
            chartControl1.Series[0].Points.Clear();
        }

        private void FillSeries(IEnumerable<Event> list)
        {
            threadColors = new Dictionary<string, Color>();
            colorIndex = 0;
            int i = 0;
            foreach (Event item in list)
            {
                DevExpress.XtraCharts.SeriesPoint seriesPoint = new DevExpress.XtraCharts.SeriesPoint(item, new object[] {
                ((object)item.StartDate),
                ((object)item.EndDate)}, i++);
                if (!string.IsNullOrEmpty(item.ThreadId) && !threadColors.ContainsKey(item.ThreadId))
                {
                    threadColors[item.ThreadId] = GetNextColor();
                }
                chartControl1.Series[0].Points.Add(seriesPoint);
            }
        }

        private void ClearTimePoints()
        {
            XYDiagram diagram = chartControl1.Diagram as XYDiagram;
            diagram.AxisY.ConstantLines.Clear();
        }

        private void FillTimePoints(IEnumerable<TimePoint> timePoints)
        {
            XYDiagram diagram = chartControl1.Diagram as XYDiagram;
            isShowWithShift = true;
            foreach (TimePoint timePoint in timePoints)
            {
                ConstantLine constantLine1 = GetConstantLine(timePoint);
                diagram.AxisY.ConstantLines.Add(constantLine1);
            }
        }

        private ConstantLine GetConstantLine(TimePoint timePoint)
        {
            ConstantLine constantLine = new ConstantLine(timePoint.Message, timePoint.Date);

            //// Define its axis value.
            //constantLine1.AxisValue = 7;

            //// Customize the behavior of the constant line.
            constantLine.Visible = true;
            //constantLine1.ShowInLegend = true;
            //constantLine1.LegendText = "Some Threshold";
            //constantLine.ShowBehind = true;

            //// Customize the constant line's title.
            constantLine.Title.Visible = true;
            //constantLine1.Title.TextColor = Color.Black;
            constantLine.Title.Antialiasing = false;
            constantLine.Title.Font = new Font(this.Font.FontFamily.Name, 8, FontStyle.Regular);
            constantLine.Title.ShowBelowLine = isShowWithShift;
            isShowWithShift = !isShowWithShift;
            constantLine.Title.Alignment = ConstantLineTitleAlignment.Near;

            //// Customize the appearance of the constant line.
            if (timePoint.IsBackground)
            {
                constantLine.Color = Color.Blue;
            }
            else if (timePoint.IsCritical)
            {
                constantLine.Color = Color.Red;
            }
            else
            {
                constantLine.Color = Color.Green;
            }
            constantLine.LineStyle.DashStyle = DashStyle.Dash;
            //constantLine1.LineStyle.Thickness = 2;
            return constantLine;
        }

        private Color GetColor(string arg, Color color)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                string[] args = arg.Split(new char[] { Event.SEPARATOR });
                if (args.Length > 3 && threadColors.ContainsKey(args[3]))
                {
                    return threadColors[args[3]];
                }
            }
            return color;
        }

        private string GetDescription(SeriesPoint point, bool isAddThread = false)
        {
            DateTime dt = point.DateTimeValues[0];
            DateTime dt2 = point.DateTimeValues[1];
            return string.Format("{7}\n{0}:{1}.{2}-{3}:{4}.{5} = {6}ms", dt.Minute, dt.Second, dt.Millisecond
                , dt2.Minute, dt2.Second, dt2.Millisecond, (dt2 - dt).TotalMilliseconds, GetArgument(point.Argument, toolStripButtonShowService.Checked, isAddThread));
        }

        private string GetArgument(string arg, bool isAddService = false, bool isAddThread = false)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                string[] args = arg.Split(new char[] { Event.SEPARATOR });
                if (args.Length > 1)
                {
                    Uri uri;
                    string uriStr = "";
                    if (Uri.TryCreate(args[0], UriKind.Absolute, out uri))
                    {
                        uriStr = uri.LocalPath + " ";
                    }

                    string thread = ""; 
                    if (args.Length  > 3)
                    {
                        thread = " Th:" + args[3];
                        if (args.Length > 4)
                        {
                            thread += " Dom:" + args[4];
                        }
                    }
                    
                    return (isAddService ? uriStr : string.Empty) 
                        + args[1]
                        + (isAddThread ? thread : string.Empty)
                        ;
                }
            }
            return arg;
        }

        private Color GetNextColor()
        {
            if (colorIndex >= colors.Length)
            {
                colorIndex = 0;
            }
            return colors[colorIndex++];
        }

        #region DISPLAY

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            //int maxSize = 63;
            //if (this.WindowState == FormWindowState.Minimized && this.Visible)
            //{
            //    this.Visible = false;
            //    notifyIcon1.Visible = true;
            //    notifyIcon1.Text = this.Text.Length <= maxSize ? this.Text : this.Text.Substring(0, maxSize);
            //}
        }

        #endregion



    }
}
