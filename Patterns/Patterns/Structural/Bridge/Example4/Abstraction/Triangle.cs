using System.Drawing;
using System.Windows.Forms;

namespace Patterns.Structural.Bridge.Example4
{
    class Triangle : Shape
    {
        // Создать массив точек
        Point[] points = new Point[]
        { 
            new Point(50, 50), 
            new Point(50, 200), 
            new Point(200, 200)            
        };

        public override void Draw(Form form, Color color)
        {
            base.Draw(form, color);

            // Нарисовать фигуру.            
            this.graphics.DrawPolygon(pen, points);
        }
    }
}
