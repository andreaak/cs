using System;

namespace Patterns.Creational.AbstractFactory.Example2
{
    class Client
    {
        private AbstractWater water;
        private AbstractBottle bottle;

        public Client(AbstractFactory factory)
        {
            // ��������������� �������� ���������������.
            water = factory.CreateWater();
            bottle = factory.CreateBottle();
        }

        public void Run()
        {
            // ��������������� ��������� �������������.
            bottle.Interact(water);
        }
    }
}
