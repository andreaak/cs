using System;
using System.Diagnostics;

namespace Creational.FactoryMethod._010_Generic
{
    class ProductA : IProduct
    {
        public ProductA()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
