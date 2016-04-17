using System.Windows.Forms;

namespace Patterns._03_Behavioral.Command._004_Form.Command
{
    class PasteCommand : Command
    {

        public override void Execute()
        {

            if (MainForm.CurrentDocument != null)
            {
                LogExecution("paste text: " + Clipboard.GetText());
                MainForm.CurrentDocument.Paste();
            }
        }
    }

}
