using System;
using System.Diagnostics;

namespace Patterns._02_Structural.Flyweight._001_Base
{
    class ConcreteFlyweight : Flyweight
    {
        string intrinsicState = "ConcreteFlyweight ";
        ConsoleColor extrinsicState;

        public override void Operation(ConsoleColor extrinsicState)
        {
            this.extrinsicState = extrinsicState;
            //Console.ForegroundColor = this.extrinsicState;
            Debug.WriteLine(intrinsicState + GetHashCode());
        }
    }
}
