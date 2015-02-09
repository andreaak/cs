using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.SimpleFactory.Example1.Cars;
using Factory = Patterns.Creational.SimpleFactory.Example1.Factory;

namespace Patterns.Creational.SimpleFactory.Example1.Facilities
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
