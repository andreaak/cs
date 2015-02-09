using System;

namespace Creational.FactoryMethod.Example2
{
    class ConcreteCreator : Creator
    {
        public override Product FactoryMethod()
        {
            return new ConcreteProduct();
        }
    }
}
