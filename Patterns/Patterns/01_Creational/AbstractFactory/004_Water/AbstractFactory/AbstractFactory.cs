using System;

namespace Patterns.Creational.AbstractFactory._004_Water
{
    abstract class AbstractFactory
    {
        public abstract AbstractWater CreateWater();
        public abstract AbstractBottle CreateBottle();
    }
}
