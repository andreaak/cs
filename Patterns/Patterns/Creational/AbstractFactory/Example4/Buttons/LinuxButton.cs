using System.Drawing;

namespace Patterns.Creational.AbstractFactory.Example4
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
