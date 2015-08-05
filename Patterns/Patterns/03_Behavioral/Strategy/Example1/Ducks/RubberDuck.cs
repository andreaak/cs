using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Strategy.Example1.Fly;
using System.Diagnostics;

namespace Patterns.Behavioral.Strategy.Example1.Ducks
{
    public class RubberDuck : DuckBase
    {
        public RubberDuck()
        {
            flyBehaviour = new NoFly();
        }

        public override void Display()
        {
            Console.WriteLine("Hi! I'm a rubber duck!");
        }
    }
}
