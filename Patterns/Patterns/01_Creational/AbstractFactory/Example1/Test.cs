using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Creational.AbstractFactory.Example1.Facilities;
using System;

namespace Patterns.Creational.AbstractFactory.Example1
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestBefore()
        {
            VolkswagenFacility facility = new RussianVolkswagenFacility();

            facility.GetCarBefore();
            Console.WriteLine("");
        }
        
        [TestMethod]
        public void Test1()
        {
            VolkswagenFacility facility = new RussianVolkswagenFacility();

            facility.GetCar("Golf");
            Console.WriteLine("");

            facility.GetCar("Passat");
            Console.WriteLine("");

            facility.GetCar("Tiguan");
            Console.WriteLine("");

            facility.GetCar("Touareg");
            Console.WriteLine("");
        }
    }
}
