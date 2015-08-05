namespace Patterns.Behavioral.Command.Example1.Commands
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
