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
            Console.WriteLine("Cheap Volkswagen:");
            CarFactoryBase constructor = new CheapCarFactory(new VolkswagenBuilder());
            var car = constructor.Construct();
            Console.WriteLine(car);

            Console.WriteLine("Luxury Volkswagen2:");
            constructor = new LuxuryCarFactory(new VolkswagenBuilder());
            car = constructor.Construct();
            Console.WriteLine(car);

            Console.WriteLine("Cheap Audi:");
            constructor = new CheapCarFactory(new AudiBuilder());
            car = constructor.Construct();
            Console.WriteLine(car);

            Console.WriteLine("Luxury Audi:");
            constructor = new LuxuryCarFactory(new AudiBuilder());
            car = constructor.Construct();
            Console.WriteLine(car);

        }
    }
}
