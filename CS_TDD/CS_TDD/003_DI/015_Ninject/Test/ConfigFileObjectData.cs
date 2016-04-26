using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._000_Base.Test;
using CS_TDD._003_DI._002_ConstructorInjection;
using Ninject.Modules;

namespace CS_TDD._003_DI._015_Ninject.Test
{
    class ConfigFileObjectData : NinjectModule
    {
        public override void Load()
        {
            // Bind dependencies
            //this.Bind<IDataAccessObject>().To<FileDataObject>();
            this.Bind<IDataAccessObject>().To<StubFileDataObject>();
            this.Bind<FileManager>().ToSelf();
        }
    }
}
