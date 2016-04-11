using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Patterns.Creational.AbstractFactory._003_Cars.Parts
{
    public class GasolineEngine : Engine
    {
        public GasolineEngine()
        {
            Debug.WriteLine("Engine is gasoline");
        }
    }
}
