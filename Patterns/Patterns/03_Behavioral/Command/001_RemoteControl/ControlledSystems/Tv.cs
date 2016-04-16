using System.Diagnostics;

namespace Patterns._03_Behavioral.Command._001_RemoteControl.ControlledSystems
{
    public class Tv
    {
        public void TurnOn()
        {
            Debug.WriteLine("Телевизор включен");
            State = State.On;
        }

        public void TurnOff()
        {
            Debug.WriteLine("Телевизор выключен");
            State = State.Off;
        }

        public State State { get; set; }
    }
}
