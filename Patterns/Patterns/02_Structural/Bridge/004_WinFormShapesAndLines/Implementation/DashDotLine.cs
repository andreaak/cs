using System.Drawing;
using System.Drawing.Drawing2D;

namespace Patterns._02_Structural.Bridge._004_WinFormShapesAndLines.Implementation
{
    class DashDotLine : LineStyle
    {
        public override Pen Draw(Color color)
        {
            Pen pen = new Pen(color, 7);
            pen.DashStyle = DashStyle.DashDot;

            // Специальные настройки (Снять комментарий)
            //pen.DashPattern = new float[] { 1.0f, 2.0f };
            //pen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);

            return pen;
        }
    }
}
