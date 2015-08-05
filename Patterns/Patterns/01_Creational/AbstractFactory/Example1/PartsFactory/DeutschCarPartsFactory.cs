using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Creational.AbstractFactory.Example1.Parts;

namespace Patterns.Creational.AbstractFactory.Example1.PartsFactory
{
    public class DeutschCarPartsFactory : CarPartsFactory
    {
        public override Engine CreateEngine()
        {
            return new DieselEngine();
        }

        public override Paint CreatePaint()
        {
            return new WhitePaint();
        }

        public override Wheels CreateWheels()
        {
            return new BigWheels();
        }
    }
}
