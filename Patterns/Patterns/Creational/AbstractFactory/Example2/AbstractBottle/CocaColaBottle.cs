using System;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example2
{
    class CocaColaBottle : AbstractBottle
    {
        public override void Interact(AbstractWater water)
        {
            Debug.WriteLine(this + " interacts with " + water);
        }
    }
}