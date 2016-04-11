using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Creational.AbstractFactory._003_Cars.Parts
{
    public class MediumWheels : Wheels
    {
        public MediumWheels()
        {
            Debug.WriteLine("Wheels are 16\"");
        }
    }
}
