using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Patterns.Creational.SimpleFactory.Example1.Facilities;
using Factory = Patterns.Creational.SimpleFactory.Example1.Factory;

namespace Patterns.Creational.SimpleFactory.Example1
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
