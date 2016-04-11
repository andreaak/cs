using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Behavioral.Strategy._001_Ducks.Fly
{
    public class NoFly : IFlyable
    {
        public void Fly()
        {
            Debug.WriteLine("---");
        }
    }
}
