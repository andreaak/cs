using System;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example3
{
    class ProductB2 : AbstractProductB
    {
        public override void Interact(AbstractProductA apa)
        {
            Debug.WriteLine(this + " interacts with " + apa);
        }
    }
}