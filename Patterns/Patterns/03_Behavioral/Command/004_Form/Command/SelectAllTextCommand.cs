namespace Patterns._03_Behavioral.Command._004_Form.Command
{
    class SelectAllTextCommand : Command
    {
        public override void Execute()
        {
            if (MainForm.CurrentDocument != null)
            {
                LogExecution(MainForm.CurrentDocument.Text + "select all text");
                MainForm.CurrentDocument.DocumentContent.SelectAll();
            }
        }
    }
}
