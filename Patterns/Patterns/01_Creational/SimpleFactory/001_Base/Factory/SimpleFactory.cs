using Patterns._01_Creational.SimpleFactory._001_Base.Cars;

namespace Patterns._01_Creational.SimpleFactory._001_Base.Factory
{
    public class SimpleFactory
    {
        public Car GetCar(string type)
        {
            Car car = new Car();

            if (type == "Golf")
                car = new Golf();
            else if (type == "Passat")
                car = new Passat();
            else if (type == "Tiguan")
                car = new Tiguan();
            else if (type == "Touareg")
                car = new Touareg();

            return car;
        }
    }
}
