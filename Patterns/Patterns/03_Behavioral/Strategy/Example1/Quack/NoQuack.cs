using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Strategy.Example1.Quack
{
    public class NoQuack : IQuackable
    {
        public void Quack()
        {
            Console.WriteLine("...");
        }
    }
}
