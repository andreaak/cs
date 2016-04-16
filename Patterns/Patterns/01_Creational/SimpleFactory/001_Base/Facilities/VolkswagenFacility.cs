using Patterns._01_Creational.SimpleFactory._001_Base.Cars;

namespace Patterns._01_Creational.SimpleFactory._001_Base.Facilities
{
    class VolkswagenFacility
    {

        private Factory.SimpleFactory factory;

        public VolkswagenFacility(Factory.SimpleFactory factory)
        {
            this.factory = factory;
        }
        
        public Car GetCar(string type)
        {
            Car car = factory.GetCar(type);

            car.Configure();
            car.AssembleBody();
            car.InstallEngine();
            car.Paint();
            car.InstallWheels();

            return car;
        }
    }
}
