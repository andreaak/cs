using System;

namespace Patterns.Creational.AbstractFactory.Example3
{
    abstract class AbstractFactory
    {
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }
}
