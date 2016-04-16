namespace Patterns._01_Creational.AbstractFactory._004_Water
{
    class Client
    {
        private AbstractWater.AbstractWater water;
        private AbstractBottle.AbstractBottle bottle;

        public Client(AbstractFactory.AbstractFactory factory)
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
