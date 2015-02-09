using Patterns.Behavioral.Command.Example1.ControlledSystems;

namespace Patterns.Behavioral.Command.Example1.Commands
{
    public class TvCommand : ICommand
    {
        private Tv _tv;

        public TvCommand(Tv tv)
        {
            _tv = tv;
        }

        public void Execute()
        {
            _tv.TurnOn();
        }

        public void Undo()
        {
            _tv.TurnOff();
        }

        public override string ToString()
        {
            return "Включить телевизор";
        }
    }
}
