using System;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example5
{
    class ProductB1 : IAbstractProductB
    {
        public void Interact(IAbstractProductA a)
        {
            Debug.WriteLine(this.GetType().Name + " взаимодействует с " + a.GetType().Name);
        }
    }
}
