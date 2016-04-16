using System.Drawing;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Windows
{
    class LinuxForm : AbstractWindow
    {
        public LinuxForm()
            : base()
        {
            InitializeComponent();
        }

        public override void AddToCollection(AbstractButton button)
        {
            this.Controls.Add(button);
        }

        public void InitializeComponent()
        {
            this.Name = "linuxForm";
            this.Text = "Linux - Gnom";
            this.BackColor = Color.Yellow;
        }
    }
}
