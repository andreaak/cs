using System;
using System.Collections.Generic;

namespace Creational.FactoryMethod.Example10
{
    class StandardCreator : ICreator
    {       
        public T CreateProduct<T>()
        {          
            return Activator.CreateInstance<T>();
        }
    }
}
