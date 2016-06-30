using CS_TDD._003_DI._000_Base;

namespace CS_TDD._003_DI._008_AbsFactoryInjection.AbstractFactory
{
    interface IDataAccessObjectFactory
    {
        IDataAccessObject CreateDataAccessObject();
    } 
}
