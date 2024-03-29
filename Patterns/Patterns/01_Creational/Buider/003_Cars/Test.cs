﻿using System.Diagnostics;
using NUnit.Framework;
using Patterns._01_Creational.Buider._003_Cars.Builder;
using Patterns._01_Creational.Buider._003_Cars.Factory;

namespace Patterns._01_Creational.Buider._003_Cars
{
    [TestFixture]
    public class Test
    {
        [Test]
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
