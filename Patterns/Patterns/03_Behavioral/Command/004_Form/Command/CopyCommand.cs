namespace Patterns._03_Behavioral.Command._004_Form.Command
{
    class CopyCommand : Command
    {
        public override void Execute()
        {
            if (MainForm.CurrentDocument != null)
            {
                LogExecution(MainForm.CurrentDocument.Text + "copy text: " + MainForm.CurrentDocument.DocumentContent.SelectedText);
                MainForm.CurrentDocument.Copy();
            }
        }
    }
}
