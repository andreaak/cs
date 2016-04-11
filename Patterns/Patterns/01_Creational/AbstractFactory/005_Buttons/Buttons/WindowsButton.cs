using System.Drawing;

namespace Patterns.Creational.AbstractFactory._005_Buttons
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
