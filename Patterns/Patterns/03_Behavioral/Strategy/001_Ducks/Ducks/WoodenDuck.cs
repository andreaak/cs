using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Strategy._001_Ducks.Fly;
using Patterns.Behavioral.Strategy._001_Ducks.Quack;
using System.Diagnostics;

namespace Patterns.Behavioral.Strategy._001_Ducks.Ducks
{
    public class WoodenDuck : DuckBase
    {
        public WoodenDuck()
        {
            flyBehaviour = new NoFly();
            quackBehaviour = new NoQuack();
        }

        public override void Display()
        {
            Debug.WriteLine("Hi! I'm a wooden duck!");
        }
    }
}
