using System.Drawing;
using System.Windows.Forms;

namespace Patterns.Structural.Bridge._004_WinFormShapesAndLines
{
    // "Client"
    class Figure
    {
        public static void Draw(Form form, Shape shape, LineStyle line, Color color)
        {
            shape.implementor = line;
            shape.Draw(form, color);
        }
    }
}
