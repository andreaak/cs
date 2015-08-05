using System;
using System.Diagnostics;

namespace Creational.FactoryMethod.Example10
{
    class ProductB : IProduct
    {
        public ProductB()
        {
            Console.WriteLine(this.GetType().Name);
        }
    }
}
