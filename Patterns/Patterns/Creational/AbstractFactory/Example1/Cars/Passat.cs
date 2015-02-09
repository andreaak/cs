using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory.Example1.PartsFactory;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example1.Cars
{
    public class Passat : Car
    {
        private CarPartsFactory _factory;

        public Passat(CarPartsFactory factory)
        {
            Name = "Passat";
            Body = "Sedan";

            _factory = factory;
        }

        public override void Configure()
        {
            Debug.WriteLine("Configuring " + Name);
            Debug.WriteLine("Body is: " + Body);

            Engine = _factory.CreateEngine();
            PaintColor = _factory.CreatePaint();
            Wheels = _factory.CreateWheels();
        }
    }
}
