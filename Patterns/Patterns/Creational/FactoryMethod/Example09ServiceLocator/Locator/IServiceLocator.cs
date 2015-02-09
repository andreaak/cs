using System;


namespace Creational.FactoryMethod.Example9
{
    interface IServiceLocator
    {
        T GetService<T>();
    }
}
