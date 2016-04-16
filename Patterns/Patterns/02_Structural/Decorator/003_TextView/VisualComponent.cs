using System.Windows.Forms;

namespace Patterns._02_Structural.Decorator._003_TextView
{
    public abstract class VisualComponent
    {
        public Form Form { get; set; }
        public TextBox TextBox { get; set; }

        protected VisualComponent(Form form, TextBox textBox)
        {
            Form = form;
            this.TextBox = textBox;
        }

        public abstract void Draw();
    }
}
