using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Creational.AbstractFactory.Example1.Parts
{
    public class BigWheels : Wheels
    {
        public BigWheels()
        {
            Debug.WriteLine("Wheels are 17\"");
        }
    }
}
