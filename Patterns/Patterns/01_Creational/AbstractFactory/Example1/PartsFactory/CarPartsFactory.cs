using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory.Example1.Parts;

namespace Patterns.Creational.AbstractFactory.Example1.PartsFactory
{
    public abstract class CarPartsFactory
    {
        public abstract Engine CreateEngine();
        public abstract Paint CreatePaint();
        public abstract Wheels CreateWheels();
    }
}
