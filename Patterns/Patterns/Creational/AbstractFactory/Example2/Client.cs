using System;

namespace Patterns.Creational.AbstractFactory.Example2
{
    class Client
    {
        private AbstractWater water;
        private AbstractBottle bottle;

        public Client(AbstractFactory factory)
        {
            // Абстрагирование процесса инстанцирования.
            water = factory.CreateWater();
            bottle = factory.CreateBottle();
        }

        public void Run()
        {
            // Абстрагирование вариантов использования.
            bottle.Interact(water);
        }
    }
}
