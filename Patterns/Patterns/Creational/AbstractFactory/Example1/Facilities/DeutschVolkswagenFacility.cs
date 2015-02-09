using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory.Example1.Cars;
using Patterns.Creational.AbstractFactory.Example1.PartsFactory;

namespace Patterns.Creational.AbstractFactory.Example1.Facilities
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
