using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Strategy.Example1.Quack;
using System.Diagnostics;

namespace Patterns.Behavioral.Strategy.Example1.Ducks
{
    public class ExoticDuck : DuckBase
    {
        public ExoticDuck()
        {
            quackBehaviour = new ExoticQuack();
        }

        public override void Display()
        {
            Console.WriteLine("Hi! I'm an exotic duck.");
        }
    }
}
