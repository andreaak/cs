using System;
using System.Diagnostics;

namespace Creational.FactoryMethod.Example2
{
    class ConcreteProduct : Product
    {
        public ConcreteProduct()
        {
            Console.WriteLine(this.GetHashCode());
        }
    }
}
