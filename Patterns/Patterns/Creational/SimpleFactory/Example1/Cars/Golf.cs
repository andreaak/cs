using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Patterns.Creational.SimpleFactory.Example1.Cars
{
    public class Golf : Car
    {

        public Golf()
        {
            Name = "Golf";
            Body = "Hatchback";
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
