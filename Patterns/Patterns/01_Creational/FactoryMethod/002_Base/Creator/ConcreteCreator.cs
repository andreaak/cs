using Patterns._01_Creational.FactoryMethod._002_Base.Product;

namespace Patterns._01_Creational.FactoryMethod._002_Base.Creator
{
    class ConcreteCreator : Creator
    {
        public override Product.Product FactoryMethod()
        {
            return new ConcreteProduct();
        }
    }
}
