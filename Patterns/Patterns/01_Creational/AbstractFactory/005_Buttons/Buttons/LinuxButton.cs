using System.Drawing;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons
{
    class LinuxButton : AbstractButton
    {
        public LinuxButton()
            : base()
        {
            this.Text = "Linux";
            this.ForeColor = Color.DarkGreen;
            this.BackColor = Color.LightGreen;
        }
    }
}
