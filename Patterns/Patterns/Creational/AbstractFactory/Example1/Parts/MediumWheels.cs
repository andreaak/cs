using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Creational.AbstractFactory.Example1.Parts
{
    public class MediumWheels : Wheels
    {
        public MediumWheels()
        {
            Debug.WriteLine("Wheels are 16\"");
        }
    }
}
