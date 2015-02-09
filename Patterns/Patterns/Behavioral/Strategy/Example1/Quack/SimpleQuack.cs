using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Strategy.Example1.Quack
{
    public class SimpleQuack : IQuackable
    {
        public void Quack()
        {
            Debug.WriteLine("Quack! Quack!");
        }
    }
}
