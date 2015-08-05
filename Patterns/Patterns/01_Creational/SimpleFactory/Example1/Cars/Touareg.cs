using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory.Example1.PartsFactory;
using System.Diagnostics;

namespace Patterns.Creational.SimpleFactory.Example1.Cars
{
    public class Touareg : Car
    {

        public Touareg()
        {
            Name = "Touareg";
            Body = "Big Crossover";
        }

        //public override void Configure()
        //{
        //    Console.WriteLine("Configuring {0}", Name);
        //    Console.WriteLine("Body is: {0}", Body);

        //    //Engine = _factory.CreateEngine();
        //    //PaintColor = _factory.CreatePaint();
        //    //Wheels = _factory.CreateWheels();
        //}
    }
}
