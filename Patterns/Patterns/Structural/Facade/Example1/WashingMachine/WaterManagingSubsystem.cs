using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Structural.Facade.Example1.WashingMachine
{
    class WaterManagingSubsystem
    {
        public void FillWater(int litres)
        {
            Console.WriteLine("Fill with {0} litres of water", litres);
        }

        public void EmptyWater()
        {
            Console.WriteLine("Empty water tank");
        }
    }
}
