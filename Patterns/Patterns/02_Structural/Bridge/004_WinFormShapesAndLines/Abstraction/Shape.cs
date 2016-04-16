using System.Drawing;
using System.Windows.Forms;
using Patterns._02_Structural.Bridge._004_WinFormShapesAndLines.Implementation;

namespace Patterns._02_Structural.Bridge._004_WinFormShapesAndLines.Abstraction
{
    // "Abstraction"                                                                                     
    abstract class Shape
    {
        protected Graphics graphics = null;
        protected Pen pen = null;

        public LineStyle implementor = null;

        public virtual void Draw(Form form, Color color)
        {
            this.graphics = form.CreateGraphics();
            this.pen = implementor.Draw(color);
        }
    }
}
