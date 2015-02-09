using System;
using System.Drawing;

namespace Patterns.Creational.AbstractFactory.Example4
{
    // ConcreteProductB1
    class WindowsForm : AbstractWindow
    {
        public WindowsForm()
        {
            InitializeComponent();
        }

        public override void AddToCollection(AbstractButton button)
        {
            this.Controls.Add(button);
        }

        private void InitializeComponent()
        {
            this.Name = "windowsForm";
            this.Text = "Windows Explorer";
            this.BackColor = Color.LightBlue;
        }
    }
}
