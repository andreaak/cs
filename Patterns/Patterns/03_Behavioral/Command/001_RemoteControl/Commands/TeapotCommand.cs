using Patterns._03_Behavioral.Command._001_RemoteControl.ControlledSystems;

namespace Patterns._03_Behavioral.Command._001_RemoteControl.Commands
{
    public class TeapotCommand : ICommand
    {
        private Teapot _teapot;

        public TeapotCommand(Teapot teapot)
        {
            _teapot = teapot;
        }

        public void Execute()
        {
            _teapot.TurnOn();
        }

        public void Undo()
        {
            _teapot.TurnOff();
        }

        public override string ToString()
        {
            return "Включить чайник";
        }
    }
}
