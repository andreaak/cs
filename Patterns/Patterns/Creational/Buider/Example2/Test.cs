using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Creational.Builder.Example2.Factory;
using Creational.Builder.Example2.Builder;
using System.Diagnostics;

namespace Creational.Builder.Example2
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Debug.WriteLine("Cheap Volkswagen:");
            CarFactoryBase constructor = new CheapCarFactory(new VolkswagenBuilder());
            var car = constructor.Construct();
            Debug.WriteLine(car);

            Debug.WriteLine("Luxury Volkswagen2:");
            constructor = new LuxuryCarFactory(new VolkswagenBuilder());
            car = constructor.Construct();
            Debug.WriteLine(car);

            Debug.WriteLine("Cheap Audi:");
            constructor = new CheapCarFactory(new AudiBuilder());
            car = constructor.Construct();
            Debug.WriteLine(car);

            Debug.WriteLine("Luxury Audi:");
            constructor = new LuxuryCarFactory(new AudiBuilder());
            car = constructor.Construct();
            Debug.WriteLine(car);

        }
    }
}
