using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Strategy._001_Ducks.Ducks
{
    public class SimpleDuck : DuckBase
    {
        public override void Display()
        {
            Debug.WriteLine("Hi! I'm a simle duck.");
        }
    }
}
