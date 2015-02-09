using System.Windows.Forms;

namespace Patterns.Structural.Bridge.Example2
{
    // Abstraction
    abstract class Window
    {
        protected WindowImp implementor;

        protected Form form;
        protected Button button;

        // Operation
        public virtual void Draw()
        {
            this.form.Controls.Add(button);

            Application.EnableVisualStyles();
            Application.Run(this.form);
        }       
    }
}
