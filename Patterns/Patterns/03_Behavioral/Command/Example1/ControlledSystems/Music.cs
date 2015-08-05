using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Command.Example1.ControlledSystems
{
    public class Music
    {
        public void TurnOn()
        {
            Console.WriteLine("Музыка включена");
            State = State.On;
        }

        public void TurnOff()
        {
            Console.WriteLine("Музыка выключена");
            State = State.Off;
        }

        public State State { get; set; }
    }
}
