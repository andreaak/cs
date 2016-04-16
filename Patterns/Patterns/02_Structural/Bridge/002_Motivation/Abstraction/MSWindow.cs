
using Patterns._02_Structural.Bridge._002_Motivation.Implementors;

namespace Patterns._02_Structural.Bridge._002_Motivation.Abstraction
{
    // RefinedAbstraction
    class MSWindow : Window
    {
        public MSWindow()
        {
            this.implementor = new MSWindowImp();
            this.form = this.implementor.DevDrawForm();
            this.button = this.implementor.DevDrawButton();
        }

        // Operation
        public override void Draw()
        {
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.Draw();
        }
    }
}
