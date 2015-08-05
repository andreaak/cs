using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory.Example1.PartsFactory;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example1.Cars
{
    public class Touareg : Car
    {
        private CarPartsFactory _factory;

        public Touareg()
        {
            Name = "Touareg";
            Body = "Big Crossover";
        }
        
        public Touareg(CarPartsFactory factory)
        {
            Name = "Touareg";
            Body = "Big Crossover";

            _factory = factory;
        }

        public override void Configure()
        {
            Console.WriteLine("Configuring " + Name);
            Console.WriteLine("Body is: " + Body);

            Engine = _factory.CreateEngine();
            PaintColor = _factory.CreatePaint();
            Wheels = _factory.CreateWheels();
        }
    }
}
