using System;

namespace Patterns.Creational.AbstractFactory.Example5
{
    interface IAbstractFactory
    {
        dynamic Make(Product product);
    }
}
