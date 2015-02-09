using System;
using System.Diagnostics;

namespace Creational.FactoryMethod.Example10
{
    class ProductC : IProduct
    {
        public ProductC()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
