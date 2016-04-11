using System;

namespace Creational.FactoryMethod._002_Base
{
    class ConcreteCreator : Creator
    {
        public override Product FactoryMethod()
        {
            return new ConcreteProduct();
        }
    }
}
