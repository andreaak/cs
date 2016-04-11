using System.Drawing;
using System.Windows.Forms;

namespace Patterns.Creational.AbstractFactory._005_Buttons
{
    // AbstractProductA
    abstract class AbstractButton : Button
    {
        public AbstractButton()
        {
            this.Location = new Point(75, 70);
            this.Size = new Size(125, 25);
        }
    }
}
