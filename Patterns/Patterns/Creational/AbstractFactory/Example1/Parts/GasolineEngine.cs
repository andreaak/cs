using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Creational.AbstractFactory.Example1.Parts
{
    public class GasolineEngine : Engine
    {
        public GasolineEngine()
        {
            Debug.WriteLine("Engine is gasoline");
        }
    }
}
