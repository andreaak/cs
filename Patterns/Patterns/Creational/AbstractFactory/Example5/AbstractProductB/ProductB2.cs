using System;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example5
{
    class ProductB2 : IAbstractProductB
    {
        public void Interact(IAbstractProductA a)
        {
            Debug.WriteLine(this.GetType().Name + " ��������������� � " + a.GetType().Name);
        }
    }
}