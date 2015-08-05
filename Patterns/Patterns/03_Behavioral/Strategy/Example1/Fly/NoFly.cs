using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Strategy.Example1.Fly
{
    public class NoFly : IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("---");
        }
    }
}
