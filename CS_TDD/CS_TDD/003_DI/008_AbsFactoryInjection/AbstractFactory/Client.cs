﻿namespace CS_TDD._003_DI._008_AbsFactoryInjection.AbstractFactory
{
    class Client
    {
        IDataAccessObjectFactory factory;

        public Client(IDataAccessObjectFactory factory)
        {
            this.factory = factory;
        }

        public IFileManager Run()
        {
            return new FileManager(factory.CreateDataAccessObject());
        }
    }
}
