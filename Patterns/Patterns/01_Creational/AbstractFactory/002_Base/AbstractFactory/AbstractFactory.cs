namespace Patterns._01_Creational.AbstractFactory._002_Base.AbstractFactory
{
    abstract class AbstractFactory
    {
        public abstract AbstractProductA.AbstractProductA CreateProductA();
        public abstract AbstractProductB.AbstractProductB CreateProductB();
    }
}
