using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Algoritms
{
    public partial class Diagramm : Form
    {
        public int SpaceVertical
        {
            get { return (int)(Settings_.SpaceVertical * scaleFactor); }
        }

        public int SpaceHorizontal
        {
            get { return (int)(Settings_.SpaceHorizontal * scaleFactor); }
        }

        public int MethodBorderWidth
        {
            get { return (int)(Settings_.MethodBorderWidth * scaleFactor); }
        }

        public int ConnectorWidth
        {
            get { return (int)(Settings_.ConnectorWidth * scaleFactor); }
        } 
       
        List<BaseItem> itemsList = null;
        Point MouseMove_;
        Point DeltaMouseMove_;
        Boolean ButtonDown;
        int currX = 10;
        int currY = 10;
        double scaleFactor = 1;

        public Diagramm()
        {
            InitializeComponent();
            currY += toolStrip1.Height;
        }

        public Diagramm(List<BaseItem> itemsList)
        {
            InitializeComponent();
            this.itemsList = itemsList;
            MouseMove_.X = 0;
            MouseMove_.Y = 0;
            currY += toolStrip1.Height;
            
            itemsList[0].Left = currX;
            itemsList[0].Top = currY;
            currY += itemsList[0].Height + SpaceVertical;
            SetSubItemsCoordinates(itemsList[0], ref currX, ref currY);
        }

        private void SetSubItemsCoordinates(BaseItem parent, ref int xCoord, ref int yCoord)
        {
            foreach (BaseItem item in parent.Items)
            {
                item.Left = xCoord;
                item.Top = yCoord;
                if (item.Drawable)
                {
                    if (item.Items.Count == 0)
                    {
                        xCoord += item.Width + SpaceHorizontal;
                        continue;
                    }
                    yCoord += item.Height + SpaceVertical;
                }
                SetSubItemsCoordinates(item, ref xCoord, ref yCoord);
                if (item.Drawable)
                {
                    yCoord -= item.Height + SpaceVertical;
                }
            }
        }

        private void Diagramm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(DeltaMouseMove_.X, DeltaMouseMove_.Y);
            BaseItem.ScaleFactor = scaleFactor;
            DrawMethod(itemsList[0], g);
            DrawSubItems(itemsList[0], g);

        }

        private void DrawSubItems(BaseItem parent, Graphics g)
        {
            foreach (BaseItem item in parent.Items)
            {
                switch (item.Type_)
                {
                    case DataType.method:
                    case DataType.property:
                        DrawMethod(item, g);
                        break;
                    case DataType.branchCondition:
                    case DataType.branchElse:
                    case DataType.branch:
                        DrawBranch(item, g);
                        break;
                    case DataType.cycle:
                        DrawCycle(item, g);
                        break;
                }
            }
            if (parent.Items.Count == 0)
            {
                return;
            }
            DrawConnectors(parent, g);
        }

        private void DrawMethod(BaseItem method, Graphics g)
        {
            if (method.Type_ != DataType.branch)
            {
                DrawMethodFigure(method, g);
            }
            
            DrawSubItems(method, g);
        }

        private void DrawBranch(BaseItem branch, Graphics g)
        {
            DrawBranchFigure(branch, g);

            DrawSubItems(branch, g);
        }

        private void DrawCycle(BaseItem cycle, Graphics g)
        {
            DrawCycleFigure(cycle, g);

            DrawSubItems(cycle, g);
        }

        private static Color GetFigureColor(BaseItem method)
        {
            Color cl = Color.Aqua;

            if (!string.IsNullOrEmpty(method.Language))
            {
                if (method.Language.ToUpper().Equals(BaseItem.langCollection[Language.CPP]))
                {
                    if (!method.Static_)
                    {
                        cl = Color.Green;
                    }
                    else
                    {
                        cl = Color.SeaGreen;
                    }

                }
                else if (method.Language.ToUpper().Equals(BaseItem.langCollection[Language.CS]))
                {
                    if (!method.Static_)
                    {
                        cl = Color.Cyan;
                    }
                    else
                    {
                        cl = Color.Aquamarine;
                    }
                }
                else if (method.Language.ToUpper().Equals(BaseItem.langCollection[Language.Java]))
                {
                    if (!method.Static_)
                    {
                        cl = Color.Coral;
                    }
                    else
                    {
                        cl = Color.Chocolate;
                    }
                }
            }

            if (method.IsRoot())
            {
                cl = Color.PapayaWhip;
            }
            return cl;
        }

        #region FIGURES

        private void DrawMethodFigure(BaseItem method, Graphics g)
        {
            Color cl = GetFigureColor(method);

            if (method.IsRoot())
            {
                cl = Color.PapayaWhip;
            }
            Color cl2 = Color.FromArgb(0, 0, 0);
            Pen pn = new Pen(Color.Blue, MethodBorderWidth);
            SolidBrush br = new SolidBrush(cl);
            g.DrawRectangle(pn, method.Left, method.Top, method.Width, method.Height);
            g.FillRectangle(br, method.Left, method.Top, method.Width, method.Height);
            DrawMethodDescription(method, g);
        }

        private void DrawBranchFigure(BaseItem branch, Graphics g)
        {
            if (branch.Type_ == DataType.branchElse)
            {
                return;
            }
            Color cl = GetFigureColor(branch);

            Color cl2 = Color.FromArgb(0, 0, 0);
            Pen pn = new Pen(Color.Blue, MethodBorderWidth);
            SolidBrush br = new SolidBrush(cl);

            Point[] pts = new Point[4];
            pts[0] = new Point(branch.Left, branch.Top + branch.Height / 2);
            pts[1] = new Point(branch.Left + branch.Width / 2, branch.Top);
            pts[2] = new Point(branch.Left + branch.Width, branch.Top + branch.Height / 2);
            pts[3] = new Point(branch.Left + branch.Width / 2, branch.Top + branch.Height);
            g.DrawPolygon(pn, pts);
            g.FillPolygon(br, pts);
            DrawBranchDescription(branch, g);
        }

        private void DrawCycleFigure(BaseItem cycle, Graphics g)
        {
            Color cl = GetFigureColor(cycle);
            Color cl2 = Color.FromArgb(0, 0, 0);
            Pen pn = new Pen(Color.Blue, MethodBorderWidth);
            SolidBrush br = new SolidBrush(cl);

            Point[] pts = new Point[6];
            pts[0] = new Point(cycle.Left, cycle.Top + cycle.Height / 2);
            pts[1] = new Point(cycle.Left + Settings_.CycleAngle, cycle.Top);
            pts[2] = new Point(cycle.Left + cycle.Width - Settings_.CycleAngle, cycle.Top);
            pts[3] = new Point(cycle.Left + cycle.Width, cycle.Top + cycle.Height / 2);
            pts[4] = new Point(cycle.Left + cycle.Width - Settings_.CycleAngle, cycle.Top + cycle.Height);
            pts[5] = new Point(cycle.Left + Settings_.CycleAngle, cycle.Top + cycle.Height);
            g.DrawPolygon(pn, pts);
            g.FillPolygon(br, pts);
            DrawBranchDescription(cycle, g);
        }

        #endregion

        #region DESCRIPTION

        private void DrawMethodDescription(BaseItem item, Graphics g)
        {
            int fontHeight = (int)(Settings_.BaseFontHeight * scaleFactor);
            int shift = 0;
            float factor = 1.5f * fontHeight;

            if (item.IsRoot())
            {
                Font font = new Font("Times New Roman", fontHeight, FontStyle.Bold);
                string message = "START";
                SizeF stringSize = g.MeasureString(message, font);
                Point windowCenter = new Point(item.Left + item.Width / 2, item.Top + item.Height / 2);
                Point startPosition = new Point((int)(windowCenter.X - stringSize.Width / 2), windowCenter.Y - fontHeight / 2);
                g.DrawString(message, font, Brushes.Black, startPosition);
                return;
            }
            else
            {
                g.DrawString(item.Language == "Java" ? "PG: " : "NS: "
                    + item.NamespaceStr, new Font("Times New Roman", fontHeight), Brushes.Black, item.Left, item.Top + (factor * shift));
            }

            ++shift;
            g.DrawString("C: " + item.Class_, new Font("Times New Roman", fontHeight), Brushes.Black, item.Left, item.Top + (factor * shift));

            ++shift;
            if (toolStripButtonShowParameters.Checked)
            {
                MethodItem method = item as MethodItem;
                if (method != null)
                {
                    g.DrawString("M: "
                       + (item.Static_ ? "static " : "")
                       + method.GetDiagrammDescription(), new Font("Times New Roman", fontHeight, FontStyle.Bold), Brushes.Black, item.Left, item.Top + (factor * shift));
                }
                PropertyItem property = item as PropertyItem;
                if (property != null)
                {
                    g.DrawString("M: "
                       + (item.Static_ ? "static " : "")
                       + property.GetDescription(), new Font("Times New Roman", fontHeight, FontStyle.Bold), Brushes.Black, item.Left, item.Top + (factor * shift));
                }

            }
            else
            {
                g.DrawString("M: "
                   + (item.Static_ ? "static " : "")
                   + item.Name + "()", new Font("Times New Roman", fontHeight, FontStyle.Bold), Brushes.Black, item.Left, item.Top + (factor * shift));
            }
        }

        private void DrawBranchDescription(BaseItem branch, Graphics g)
        {
            int fontHeight = (int)(Settings_.BaseFontHeight * scaleFactor);
            float factor = 1.5f * fontHeight;

            Font font = new Font("Times New Roman", fontHeight, FontStyle.Bold);
            string message = branch.GetParameters();
            SizeF stringSize = g.MeasureString(message, font);
            Point windowCenter = new Point(branch.Left + branch.Width / 2, branch.Top + branch.Height / 2);
            Point startPosition = new Point((int)(windowCenter.X - stringSize.Width / 2), windowCenter.Y - fontHeight / 2);
            g.DrawString(message, font, Brushes.Black, startPosition);
        }

        #endregion

        #region CONNECTORS

        private void DrawConnectors(BaseItem item, Graphics g)
        {
            switch (item.Type_)
            {
                case DataType.method:
                case DataType.property:
                case DataType.cycle:
                case DataType.branch:
                case DataType.branchCondition:
                    DrawMethodConnectors(item, g);
                    break;
                case DataType.branchElse:
                    DrawMethodConnectors(item, g);
                    DrawBranchElseLine(item, g);
                    break;
            }
        }

        private void DrawMethodConnectors(BaseItem item, Graphics g)
        {
            List<BaseItem> subItems = new List<BaseItem>();
            foreach (BaseItem subItem in item.Items)
            {
                if (subItem.Type_ == DataType.branch)
                {
                    subItems.AddRange(subItem.Items);
                }
                else
                {
                    subItems.Add(subItem);
                }
            }

            if (subItems.Count == 1)
            {
                DrawVerticalMiddleLine(item, g);
                return;
            }

            for (int i = 0; i < subItems.Count - 1; ++i)
            {
                DrawItemHorizontalConnector(subItems[i], subItems[i + 1], g);
            }
            if (item.Type_ != DataType.branch)
            {
                DrawVerticalHalfMiddleLine(item, g);
            }
            DrawStartLine(subItems[0], g);
            DrawEndLine(subItems[subItems.Count - 1], g);
            DrawGroupHorizontalConnector(subItems[0], subItems[subItems.Count - 1], g);
        }

        private void DrawVerticalMiddleLine(BaseItem item, Graphics g)
        {
            Point ptMiddle = new Point(item.Left + item.Width / 2, item.Top + item.Height);
            Pen pn = new Pen(Color.Blue, ConnectorWidth);
            pn.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(pn, ptMiddle.X, ptMiddle.Y, ptMiddle.X, ptMiddle.Y + SpaceVertical);
        }

        private void DrawVerticalHalfMiddleLine(BaseItem item, Graphics g)
        {
            Point ptMiddle = new Point(item.Left + item.Width / 2, item.Top + item.Height);
            Pen pn = new Pen(Color.Blue, ConnectorWidth);
            pn.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(pn, ptMiddle.X, ptMiddle.Y, ptMiddle.X, ptMiddle.Y + SpaceVertical/2);
        }

        private void DrawStartLine(BaseItem item, Graphics g)
        {
            Point ptStart = new Point(item.Left, item.Top - SpaceVertical / 2);
            Point ptEnd = new Point(item.Left, item.Top);
            Pen pn = new Pen(Color.Blue, ConnectorWidth);
            pn.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(pn, ptStart.X, ptStart.Y, ptEnd.X, ptEnd.Y);
        }

        private void DrawEndLine(BaseItem item, Graphics g)
        {
            Point ptStart = new Point(item.Left + item.Width, item.Top - SpaceVertical / 2);
            Point ptEnd = new Point(item.Left + item.Width, item.Top);
            Pen pn = new Pen(Color.Blue, ConnectorWidth);
            pn.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(pn, ptStart.X, ptStart.Y, ptEnd.X, ptEnd.Y);
        }

        private void DrawGroupHorizontalConnector(BaseItem itemStart, BaseItem itemEnd, Graphics g)
        {
            Point ptStart = new Point(itemStart.Left, itemStart.Top - SpaceVertical / 2);
            Point ptEnd = new Point(itemEnd.Left + itemEnd.Width, itemEnd.Top - SpaceVertical / 2);
            Pen pn = new Pen(Color.Blue, ConnectorWidth);
            g.DrawLine(pn, ptStart.X, ptStart.Y, ptEnd.X, ptEnd.Y);
        }

        private void DrawItemHorizontalConnector(BaseItem itemStart, BaseItem itemEnd, Graphics g)
        {
            Point ptStart = new Point(itemStart.Left + itemStart.Width, itemStart.Top + itemStart.Height / 2);
            Point ptEnd = new Point(itemEnd.Left, itemEnd.Top + itemStart.Height / 2);
            Pen pn = new Pen(Color.Blue, ConnectorWidth);
            if (itemEnd.Type_ != DataType.branchElse)
            {
                pn.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            }
            g.DrawLine(pn, ptStart.X, ptStart.Y, ptEnd.X, ptEnd.Y);
        }

        private void DrawBranchElseLine(BaseItem item, Graphics g)
        {
            Point ptStart = new Point(item.Left - SpaceHorizontal, item.Top + item.Height / 2);
            Point ptEnd = new Point(item.Left + item.Width / 2, item.Top + item.Height / 2);
            Pen pn = new Pen(Color.Blue, ConnectorWidth);
            g.DrawLine(pn, ptStart.X, ptStart.Y, ptEnd.X, ptEnd.Y);
            ptStart = new Point(ptEnd.X, ptEnd.Y);
            ptEnd = new Point(item.Left + item.Width / 2, item.Top + item.Height);
            g.DrawLine(pn, ptStart.X, ptStart.Y, ptEnd.X, ptEnd.Y);
        }

        #endregion

        #region HANDLERS

        private void Diagramm_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonDown = true;
            MouseMove_.X = e.X - DeltaMouseMove_.X;
            MouseMove_.Y = e.Y - DeltaMouseMove_.Y;
            this.Cursor = Cursors.Hand;
        }

        private void Diagramm_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonDown = false;
            this.Cursor = Cursors.Default;
        }

        private void Diagramm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!ButtonDown)
                return;
            DeltaMouseMove_.X = e.X - MouseMove_.X;
            DeltaMouseMove_.Y = e.Y - MouseMove_.Y;
            this.Invalidate();
        }

        private void ShowMacrosPicture_MouseWheel(object sender, MouseEventArgs e)
        {
            scaleFactor += e.Delta > 0 ? 0.1 : -0.1;
            if ((int)(scaleFactor*100) % 10 != 0)
            {
                scaleFactor -= scaleFactor % 0.1;
            }
            if (scaleFactor * Settings_.BaseFontHeight < 1)
            {
                double temp = 1 / (double)Settings_.BaseFontHeight;
                scaleFactor = temp;
            }
            //width = (int)(width_ * scaleFactor);
            //height = (int)(height_ * scaleFactor);
            //space = (int)(space_ * scaleFactor);
            //connectorWidth = (int)(connectorWidth_ * scaleFactor);
            //methodWidth = (int)(methodWidth_ * scaleFactor);
            toolStripTextBoxScale.Text = (scaleFactor * 100).ToString();
            this.Invalidate();
        }

        private void toolStripButtonShowParameters_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate(true);
        }

        #endregion

    }
}
