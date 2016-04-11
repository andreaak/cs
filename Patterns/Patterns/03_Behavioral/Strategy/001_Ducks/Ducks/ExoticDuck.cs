using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Strategy._001_Ducks.Quack;
using System.Diagnostics;

namespace Patterns.Behavioral.Strategy._001_Ducks.Ducks
{
    public class ExoticDuck : DuckBase
    {
        public ExoticDuck()
        {
            quackBehaviour = new ExoticQuack();
        }

        public override void Display()
        {
            Debug.WriteLine("Hi! I'm an exotic duck.");
        }
    }
}
