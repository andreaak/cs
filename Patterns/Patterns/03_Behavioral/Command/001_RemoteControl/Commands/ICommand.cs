namespace Patterns.Behavioral.Command._001_RemoteControl.Commands
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
