using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Command._001_RemoteControl.ControlledSystems
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
