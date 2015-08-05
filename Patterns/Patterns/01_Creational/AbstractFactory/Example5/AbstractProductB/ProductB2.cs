using System;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example5
{
    class ProductB2 : IAbstractProductB
    {
        public void Interact(IAbstractProductA a)
        {
            Console.WriteLine(this.GetType().Name + " взаимодействует с " + a.GetType().Name);
        }
    }
}
