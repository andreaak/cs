using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory._003_Cars.Parts;

namespace Patterns.Creational.AbstractFactory._003_Cars.PartsFactory
{
    public abstract class CarPartsFactory
    {
        public abstract Engine CreateEngine();
        public abstract Paint CreatePaint();
        public abstract Wheels CreateWheels();
    }
}
