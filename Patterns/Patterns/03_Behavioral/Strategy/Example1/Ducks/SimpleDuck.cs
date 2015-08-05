using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Strategy.Example1.Ducks
{
    public class SimpleDuck : DuckBase
    {
        public override void Display()
        {
            Console.WriteLine("Hi! I'm a simle duck.");
        }
    }
}
