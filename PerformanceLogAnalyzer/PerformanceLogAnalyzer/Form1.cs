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

namespace EventDisplay
{
    public partial class Form1 : Form
    {
        Dictionary<string, Color> threadColors;
        Color[] colors = { Color.Red, Color.Black, Color.Green, Color.Lime, Color.Yellow, Color.Violet, Color.DarkGray, Color.Brown, Color.DarkKhaki, Color.Cyan };
        int colorIndex;
        
        private DateTime startDate;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFileDlg = new OpenFileDialog();
            oFileDlg.Filter = "Log Files (*.log)|*.log|" + "All Files (*.*)|*.*";
            if (oFileDlg.ShowDialog() == DialogResult.OK && oFileDlg.FileName.Length > 0)
            {
                Parser.Parse(oFileDlg.FileName);
                FillSeries(Parser.Events);
            }
        }

        private void FillSeries(List<Event> list)
        {
            threadColors = new Dictionary<string, Color>();
            chartControl1.Series[0].Points.Clear();
            colorIndex = 0;
            int i = 0;
            chartControl1.BeginInit();
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
            chartControl1.EndInit();
            Event firstEvent = list.FirstOrDefault();
            if(firstEvent != null)
            {
                startDate = firstEvent.StartDate;
            }
        }

        private void chartControl1_CustomDrawAxisLabel(object sender, DevExpress.XtraCharts.CustomDrawAxisLabelEventArgs e)
        {
            if (e.Item.Axis.GetType() == typeof(GanttAxisX))
            {
                e.Item.Text = GetArgument(e.Item.Text);
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

        private void toolStripButtonShowLabels_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Series series in chartControl1.Series)
            {
                series.LabelsVisibility = toolStripButtonShowLabels.Checked ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            }
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
                    return (isAddService ? new Uri(args[0]).LocalPath + " ": string.Empty) 
                        + args[1]
                        + ( isAddThread && args.Length  > 3 ? " " + args[3] : string.Empty)
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
    }
}
