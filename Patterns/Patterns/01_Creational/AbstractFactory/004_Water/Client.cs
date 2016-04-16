namespace Patterns._01_Creational.AbstractFactory._004_Water
{
    class Client
    {
        private AbstractWater.AbstractWater water;
        private AbstractBottle.AbstractBottle bottle;

        public Client(AbstractFactory.AbstractFactory factory)
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
