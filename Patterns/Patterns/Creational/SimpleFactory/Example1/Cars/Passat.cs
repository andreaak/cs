using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory.Example1.PartsFactory;
using System.Diagnostics;

namespace Patterns.Creational.SimpleFactory.Example1.Cars
{
    public class Passat : Car
    {

        public Passat()
        {
            Name = "Passat";
            Body = "Sedan";
        }

        //public override void Configure()
        //{
        //    Debug.WriteLine("Configuring {0}", Name);
        //    Debug.WriteLine("Body is: {0}", Body);

        //    //Engine = _factory.CreateEngine();
        //    //PaintColor = _factory.CreatePaint();
        //    //Wheels = _factory.CreateWheels();
        //}
    }
}
