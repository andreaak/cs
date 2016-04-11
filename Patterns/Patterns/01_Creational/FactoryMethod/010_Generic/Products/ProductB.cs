using System;
using System.Diagnostics;

namespace Creational.FactoryMethod._010_Generic
{
    class ProductB : IProduct
    {
        public ProductB()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
