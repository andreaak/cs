using System;
using Patterns.Behavioral.Command.Example1.ControlledSystems;

namespace Patterns.Behavioral.Command.Example1.Commands
{
    public class MusicCommand : ICommand
    {
        private Music _music;

        public MusicCommand(Music music)
        {
            _music = music;
        }

        public void Execute()
        {
            _music.TurnOn();
        }

        public void Undo()
        {
            _music.TurnOff();
        }

        public override string ToString()
        {
            return "Включить музыку";
        }
    }
}
