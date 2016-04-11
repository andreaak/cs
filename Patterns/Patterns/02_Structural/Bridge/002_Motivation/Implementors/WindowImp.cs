using System.Windows.Forms;

namespace Patterns.Structural.Bridge._002_Motivation
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
