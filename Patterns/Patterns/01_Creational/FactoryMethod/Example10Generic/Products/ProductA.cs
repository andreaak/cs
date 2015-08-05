using System;
using System.Diagnostics;

namespace Creational.FactoryMethod.Example10
{
    class ProductA : IProduct
    {
        public ProductA()
        {
            Console.WriteLine(this.GetType().Name);
        }
    }
}
