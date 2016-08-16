using NUnit.Framework;
using Patterns._01_Creational.SimpleFactory._001_Base.Facilities;

namespace Patterns._01_Creational.SimpleFactory._001_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Factory.SimpleFactory factory = new Factory.SimpleFactory();
            VolkswagenFacility facility = new VolkswagenFacility(factory);

            facility.GetCar("Golf");
        }
    }
}
