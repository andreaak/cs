using System;

namespace Patterns.Creational.AbstractFactory._002_Base
{
    class Client
    {
        private AbstractProductA abstractProductA = null;
        private AbstractProductB abstractProductB = null;

        public Client(AbstractFactory factory)
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
