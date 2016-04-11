using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Creational.SimpleFactory._001_Base.Facilities;

namespace Patterns.Creational.SimpleFactory._001_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Factory.SimpleFactory factory = new Factory.SimpleFactory();
            VolkswagenFacility facility = new VolkswagenFacility(factory);

            facility.GetCar("Golf");
        }
    }
}
