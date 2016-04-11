using System;


namespace Creational.FactoryMethod._009_ServiceLocator
{
    interface IServiceLocator
    {
        T GetService<T>();
    }
}
