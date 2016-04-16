namespace Patterns._01_Creational.AbstractFactory._002_Base
{
    class Client
    {
        private AbstractProductA.AbstractProductA abstractProductA = null;
        private AbstractProductB.AbstractProductB abstractProductB = null;

        public Client(AbstractFactory.AbstractFactory factory)
        {
            this.abstractProductA = factory.CreateProductA();
            this.abstractProductB = factory.CreateProductB();
        }

        public void Run()
        {
            this.abstractProductB.Interact(this.abstractProductA);
        }
    }
}
