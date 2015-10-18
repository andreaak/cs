using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._000_Base.Test;
using CS_TDD._003_DI._008_AbsFactoryInjection.AbstractFactory;

namespace CS_TDD._003_DI._008_AbsFactoryInjection.Test.AbstractFactory
{
    class StubConcreteFactory : IDataAccessObjectFactory
    {
        public IDataAccessObject CreateDataAccessObject()
        {
            return new StubFileDataObject();
        }
    }
}
