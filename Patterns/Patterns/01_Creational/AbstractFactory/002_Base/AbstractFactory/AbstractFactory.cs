using System;

namespace Patterns.Creational.AbstractFactory._002_Base
{
    abstract class AbstractFactory
    {
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }
}
