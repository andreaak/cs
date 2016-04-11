using System;

namespace Patterns.Creational.AbstractFactory._004_Water
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
