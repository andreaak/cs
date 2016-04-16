using System.Drawing;
using System.Windows.Forms;

namespace Patterns._02_Structural.Bridge._004_WinFormShapesAndLines.Abstraction
{
    class Square : Shape
    {       
        public override void Draw(Form form, Color color)
        {
            base.Draw(form, color);

            // Нарисовать фигуру.
            //this.graphics.DrawPolygon(pen, points);
            this.graphics.DrawRectangle(pen, new Rectangle(new Point(50, 50), new Size(150, 150)));
        }
    }
}
