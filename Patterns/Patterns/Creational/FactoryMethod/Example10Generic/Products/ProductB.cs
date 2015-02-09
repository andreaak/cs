using System;
using System.Diagnostics;

namespace Creational.FactoryMethod.Example10
{
    class ProductB : IProduct
    {
        public ProductB()
        {
            Debug.WriteLine(this.GetType().Name);
        }
    }
}
