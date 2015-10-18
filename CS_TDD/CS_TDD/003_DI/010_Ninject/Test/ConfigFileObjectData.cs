using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._000_Base.Test;
using Ninject.Modules;

namespace CS_TDD._003_DI._010_Ninject.Test
{
    class ConfigFileObjectData : NinjectModule
    {
        public override void Load()
        {
            //this.Bind<IDataAccessObject>().To<FileDataObject>();
            this.Bind<IDataAccessObject>().To<StubFileDataObject>();
            this.Bind<FileManager>().ToSelf();
        }
    }
}
