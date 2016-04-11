using System.Drawing;

namespace Patterns.Creational.AbstractFactory._005_Buttons
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
