using Patterns._01_Creational.AbstractFactory._002_Base.AbstractProductA;
using Patterns._01_Creational.AbstractFactory._002_Base.AbstractProductB;

namespace Patterns._01_Creational.AbstractFactory._002_Base.AbstractFactory
{
    class ConcreteFactory2 : AbstractFactory
    {
        public override AbstractProductA.AbstractProductA CreateProductA()
        {
            return new ProductA2();
        }

        public override AbstractProductB.AbstractProductB CreateProductB()
        {
            return new ProductB2();
        }
    }
}
