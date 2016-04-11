using System;

namespace Creational.FactoryMethod._010_Generic
{
    interface ICreator
    {
        T CreateProduct<T>();
    }
}
