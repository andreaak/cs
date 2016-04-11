using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Strategy._001_Ducks.Quack
{
    public class SimpleQuack : IQuackable
    {
        public void Quack()
        {
            Debug.WriteLine("Quack! Quack!");
        }
    }
}
