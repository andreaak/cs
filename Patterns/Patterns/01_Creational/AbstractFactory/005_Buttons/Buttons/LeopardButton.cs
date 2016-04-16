using System.Drawing;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons
{
    // ConcreteProductA2
    class LeopardButton : AbstractButton
    {
        public LeopardButton()
        {
            this.Text = "Leopard";
            this.ForeColor = Color.White;
            this.BackColor = Color.LightGray;
        }
    }
}
