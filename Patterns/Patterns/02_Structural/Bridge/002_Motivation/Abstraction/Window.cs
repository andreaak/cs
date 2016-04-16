using System.Windows.Forms;
using Patterns._02_Structural.Bridge._002_Motivation.Implementors;

namespace Patterns._02_Structural.Bridge._002_Motivation.Abstraction
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
