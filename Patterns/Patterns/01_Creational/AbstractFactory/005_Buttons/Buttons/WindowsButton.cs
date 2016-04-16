using System.Drawing;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons
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
