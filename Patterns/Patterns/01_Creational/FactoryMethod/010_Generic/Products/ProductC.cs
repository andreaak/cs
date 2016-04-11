using System;
using System.Diagnostics;

namespace Creational.FactoryMethod._010_Generic
{
    class ProductC : IProduct
    {
        public ProductC()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
