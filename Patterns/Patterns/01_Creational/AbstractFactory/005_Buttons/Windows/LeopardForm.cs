using System.Drawing;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Windows
{
    // ConcreteProductB2
    class LeopardForm : AbstractWindow
    {
        public LeopardForm()          
        {
            InitializeComponent();
        }

        public override void AddToCollection(AbstractButton button)
        {
            this.Controls.Add(button);
        }

        private void InitializeComponent()
        {
            this.Name = "leopardForm";
            this.Text = "Mac OS - Snow Leopard";
            this.BackColor = Color.White;
        }
    }
}
