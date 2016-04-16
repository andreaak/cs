using System.Windows.Forms;

namespace Patterns._02_Structural.Bridge._002_Motivation.Implementors
{
    // Implementor
    abstract class WindowImp
    {
        protected Button button;
        protected Form form;

        public abstract Form DevDrawForm();
        public abstract Button DevDrawButton();
    }
}
