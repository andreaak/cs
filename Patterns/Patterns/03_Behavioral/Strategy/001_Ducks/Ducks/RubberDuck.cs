using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Strategy._001_Ducks.Fly;
using System.Diagnostics;

namespace Patterns.Behavioral.Strategy._001_Ducks.Ducks
{
    public class RubberDuck : DuckBase
    {
        public RubberDuck()
        {
            flyBehaviour = new NoFly();
        }

        public override void Display()
        {
            Debug.WriteLine("Hi! I'm a rubber duck!");
        }
    }
}
