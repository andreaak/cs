using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory.Example1.Cars;

namespace Patterns.Creational.AbstractFactory.Example1.Facilities
{
    abstract class VolkswagenFacility
    {
        public CarBefore GetCarBefore()
        {
            CarBefore car = new CarBefore();

            car.Configure();
            car.AssembleBody();
            car.InstallEngine();
            car.Paint();
            car.InstallWheels();

            return car;
        }
        
        public Car GetCar(string type)
        {
            Car car = CreateCar(type);

            car.Configure();
            car.AssembleBody();
            car.InstallEngine();
            car.Paint();
            car.InstallWheels();

            return car;
        }

        protected abstract Car CreateCar(string type);
    }
}
