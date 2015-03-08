using CS_TDD._003_DI._000_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._008_AbsFactoryInjection.AbstractFactory
{
    class ConcreteFactory : IDataAccessObjectFactory
    {
        public IDataAccessObject CreateDataAccessObject()
        {
            return new FileDataObject();
        }
    }
}
