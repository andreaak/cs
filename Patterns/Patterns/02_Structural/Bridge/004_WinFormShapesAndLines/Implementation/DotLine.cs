using System.Drawing;
using System.Drawing.Drawing2D;

namespace Patterns.Structural.Bridge._004_WinFormShapesAndLines
{
    class DotLine : LineStyle
    {
        public override Pen Draw(Color color)
        {
            Pen pen = new Pen(color, 7);
            pen.DashStyle = DashStyle.Dot;
            return pen;
        }
    }
}
