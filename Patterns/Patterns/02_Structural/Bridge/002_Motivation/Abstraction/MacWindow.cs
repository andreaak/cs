
using Patterns._02_Structural.Bridge._002_Motivation.Implementors;

namespace Patterns._02_Structural.Bridge._002_Motivation.Abstraction
{
    // RefinedAbstraction
    class MacWindow : Window
    {
        public MacWindow()
        {
            this.implementor = new MacWindowImp();
            this.form = this.implementor.DevDrawForm();
            this.button = this.implementor.DevDrawButton();
        }

        // Operation
        public override void Draw()
        {
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            base.Draw();
        }
    }
}
