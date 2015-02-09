using System.Drawing;
using System.Drawing.Drawing2D;

namespace Patterns.Structural.Bridge.Example4
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
