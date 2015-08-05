using System;

namespace Patterns.Creational.AbstractFactory.Example2
{
    class CocaColaFactory : AbstractFactory
    {
        public override AbstractWater CreateWater()
        {
            return new CocaColaWater();
        }

        public override AbstractBottle CreateBottle()
        {
            return new CocaColaBottle();
        }
    }
}
