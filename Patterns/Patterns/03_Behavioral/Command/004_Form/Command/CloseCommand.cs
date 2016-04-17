
namespace Patterns._03_Behavioral.Command._004_Form.Command
{
    class CloseCommand : Command
    {
        public override void Execute()
        {
            if (MainForm.CurrentDocument != null)
            {
                LogExecution("close");
                MainForm.CurrentDocument.Close();
            }
        }
    }
}
