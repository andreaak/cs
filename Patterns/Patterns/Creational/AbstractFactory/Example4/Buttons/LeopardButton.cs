using System.Drawing;

namespace Patterns.Creational.AbstractFactory.Example4
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
