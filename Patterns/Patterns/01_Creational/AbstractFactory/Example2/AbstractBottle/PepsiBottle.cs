using System;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example2
{
    class PepsiBottle : AbstractBottle
    {
        public override void Interact(AbstractWater water)
        {
            Console.WriteLine(this + " interacts with " + water);
        }
    }
}
