﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory.Example1.PartsFactory;
using System.Diagnostics;

namespace Patterns.Creational.AbstractFactory.Example1.Cars
{
    public class Tiguan : Car
    {
        private CarPartsFactory _factory;

        public Tiguan(CarPartsFactory factory)
        {
            Name = "Tiguan";
            Body = "Crossover";

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