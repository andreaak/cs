using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Creational.AbstractFactory._003_Cars.Facilities;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory._003_Cars
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
