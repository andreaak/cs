using System;

namespace Patterns._01_Creational.FactoryMethod._010_Generic.Creators
{
    class StandardCreator : ICreator
    {       
        public T CreateProduct<T>()
        {          
            return Activator.CreateInstance<T>();
        }
    }
}
