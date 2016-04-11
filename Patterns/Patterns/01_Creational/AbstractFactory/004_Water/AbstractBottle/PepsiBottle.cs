using System;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory._004_Water
{
    class PepsiBottle : AbstractBottle
    {
        public override void Interact(AbstractWater water)
        {
            Debug.WriteLine(this + " interacts with " + water);
        }
    }
}
