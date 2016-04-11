using System;

namespace Patterns.Creational.AbstractFactory._006_BaseModified
{
    interface IAbstractFactory
    {
        dynamic Make(Product product);
    }
}
