using System.Drawing;

namespace Patterns.Creational.AbstractFactory.Example4
{
    // ConcreteProductA1
    class WindowsButton : AbstractButton
    {
        public WindowsButton()
        {
            this.Text = "Windows";
            this.ForeColor = Color.Aqua;
            this.BackColor = Color.DarkBlue;
        }
    }
}
