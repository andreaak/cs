using System.Diagnostics;

namespace Patterns._03_Behavioral.Command._001_RemoteControl.ControlledSystems
{
    public class Teapot
    {
        public void TurnOn()
        {
            Debug.WriteLine("Чайник включен");
            State = State.On;
        }

        public void TurnOff()
        {
            Debug.WriteLine("Чайник выключен");
            State = State.Off;
        }

        public State State { get; set; }
    }
}
