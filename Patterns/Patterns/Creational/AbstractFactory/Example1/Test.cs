using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Patterns.Creational.AbstractFactory.Example1.Facilities;

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
            Debug.WriteLine("");
        }
        
        [TestMethod]
        public void Test1()
        {
            VolkswagenFacility facility = new RussianVolkswagenFacility();

            facility.GetCar("Golf");
            Debug.WriteLine("");

            facility.GetCar("Passat");
            Debug.WriteLine("");

            facility.GetCar("Tiguan");
            Debug.WriteLine("");

            facility.GetCar("Touareg");
            Debug.WriteLine("");
        }
    }
}
