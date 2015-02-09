using System;
using Patterns.Behavioral.Command.Example1.ControlledSystems;

namespace Patterns.Behavioral.Command.Example1.Commands
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
