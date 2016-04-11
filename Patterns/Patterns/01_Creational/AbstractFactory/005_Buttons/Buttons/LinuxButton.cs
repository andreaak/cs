using System.Drawing;

namespace Patterns.Creational.AbstractFactory._005_Buttons
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
