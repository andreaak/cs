using System;

namespace Creational.FactoryMethod.Example2
{   
    abstract class Creator
    {
        Product product;

        public abstract Product FactoryMethod();

        public void AnOperation()
        {
            product = FactoryMethod();
        }
    }
}
