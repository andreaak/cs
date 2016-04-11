using System;
using System.Diagnostics;

namespace Creational.FactoryMethod._002_Base
{
    class ConcreteProduct : Product
    {
        public ConcreteProduct()
        {
            Debug.WriteLine(this.GetHashCode());
        }
    }
}
