using System.Windows.Forms;

namespace Patterns._03_Behavioral.Command._004_Form.Command
{
    class OpenCommand : Command
    {
        public override void Execute()
        {
            var filename = AskUser();
            if (!string.IsNullOrEmpty(filename))
            {
                var doc = new Document();
                doc.Open(filename);
                LogExecution(" opened");
                MainForm.MainFormInstance.AddDocument(doc);
            }
            else
            { LogExecution(" opening cancelled"); }
        }
        string AskUser()
        {
            LogExecution("Asking user.");
            var dlg = new OpenFileDialog();
            dlg.InitialDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.FileName;
            }
            else
                return string.Empty;
        }
    }
}
