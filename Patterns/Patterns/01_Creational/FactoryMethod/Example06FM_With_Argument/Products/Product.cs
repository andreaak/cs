using System;

namespace Creational.FactoryMethod.Example6
{
    abstract class Product
    {
        public Product()
        {
            Console.WriteLine(this.GetType().Name);
        }
    }
}
