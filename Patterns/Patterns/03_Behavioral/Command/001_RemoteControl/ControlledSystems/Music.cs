using System.Diagnostics;

namespace Patterns._03_Behavioral.Command._001_RemoteControl.ControlledSystems
{
    public class Music
    {
        public void TurnOn()
        {
            Debug.WriteLine("Музыка включена");
            State = State.On;
        }

        public void TurnOff()
        {
            Debug.WriteLine("Музыка выключена");
            State = State.Off;
        }

        public State State { get; set; }
    }
}
