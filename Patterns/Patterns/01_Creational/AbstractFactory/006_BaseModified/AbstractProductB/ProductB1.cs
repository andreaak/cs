using System;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory._006_BaseModified
{
    class ProductB1 : IAbstractProductB
    {
        public void Interact(IAbstractProductA a)
        {
            Debug.WriteLine(this.GetType().Name + " ��������������� � " + a.GetType().Name);
        }
    }
}
