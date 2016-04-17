
namespace Patterns._03_Behavioral.Command._004_Form.Command
{
    abstract class Command
    {
        public abstract void Execute();

        protected void LogExecution(string text)
        {
            MainForm.MainFormInstance.Log(this.GetType().Name + " -> " + text);
        }
    }
}
