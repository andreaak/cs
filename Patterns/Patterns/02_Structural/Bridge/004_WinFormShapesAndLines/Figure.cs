using System.Drawing;
using System.Windows.Forms;
using Patterns._02_Structural.Bridge._004_WinFormShapesAndLines.Abstraction;
using Patterns._02_Structural.Bridge._004_WinFormShapesAndLines.Implementation;

namespace Patterns._02_Structural.Bridge._004_WinFormShapesAndLines
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
