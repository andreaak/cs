using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory._003_Cars.Cars;
using Patterns.Creational.AbstractFactory._003_Cars.PartsFactory;

namespace Patterns.Creational.AbstractFactory._003_Cars.Facilities
{
    class DeutschVolkswagenFacility : VolkswagenFacility
    {
        protected override Car CreateCar(string type)
        {
            CarPartsFactory factory = new DeutschCarPartsFactory();

            if (type == "Golf")
                return new Golf(factory);
            else if (type == "Passat")
                return new Passat(factory);
            else if (type == "Tiguan")
                return new Tiguan(factory);
            else if (type == "Touareg")
                return new Touareg(factory);

            return null;
        }
    }
}
