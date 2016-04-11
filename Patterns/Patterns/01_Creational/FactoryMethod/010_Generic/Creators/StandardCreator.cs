using System;
using System.Collections.Generic;

namespace Creational.FactoryMethod._010_Generic
{
    class StandardCreator : ICreator
    {       
        public T CreateProduct<T>()
        {          
            return Activator.CreateInstance<T>();
        }
    }
}
