using System;

namespace Patterns.Creational.AbstractFactory.Example2
{
    abstract class AbstractFactory
    {
        public abstract AbstractWater CreateWater();
        public abstract AbstractBottle CreateBottle();
    }
}
