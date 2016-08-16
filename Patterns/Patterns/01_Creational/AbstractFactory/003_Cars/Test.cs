using System.Diagnostics;
using NUnit.Framework;
using Patterns._01_Creational.AbstractFactory._003_Cars.Facilities;

namespace Patterns._01_Creational.AbstractFactory._003_Cars
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestBefore()
        {
            VolkswagenFacility facility = new RussianVolkswagenFacility();

            facility.GetCarBefore();
            Debug.WriteLine("");
        }
        
        [Test]
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
