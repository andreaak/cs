using System.Drawing;
using System.Windows.Forms;

namespace Patterns.Structural.Bridge.Example4
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
