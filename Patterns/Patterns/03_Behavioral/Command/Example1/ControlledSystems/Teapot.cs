using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Command.Example1.ControlledSystems
{
    public class Teapot
    {
        public void TurnOn()
        {
            Console.WriteLine("Чайник включен");
            State = State.On;
        }

        public void TurnOff()
        {
            Console.WriteLine("Чайник выключен");
            State = State.Off;
        }

        public State State { get; set; }
    }
}
