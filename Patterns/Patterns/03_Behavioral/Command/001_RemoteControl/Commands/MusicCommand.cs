using System;
using Patterns.Behavioral.Command._001_RemoteControl.ControlledSystems;

namespace Patterns.Behavioral.Command._001_RemoteControl.Commands
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
