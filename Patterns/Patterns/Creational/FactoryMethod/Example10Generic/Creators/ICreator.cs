using System;

namespace Creational.FactoryMethod.Example10
{
    interface ICreator
    {
        T CreateProduct<T>();
    }
}
