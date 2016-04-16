using System;
using System.Diagnostics;

namespace Patterns._02_Structural.Flyweight._001_Base
{
    class UnsharedConcreteFlyweight : Flyweight
    {
        string allState = "UnsharedConcreteFlyweight ";

        public override void Operation(ConsoleColor extrinsicState)
        {
            //Console.ForegroundColor = extrinsicState;
            Debug.WriteLine(allState + GetHashCode());
        }
    }
}
