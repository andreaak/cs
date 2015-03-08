using CS_TDD._003_DI._000_Base;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._010_Ninject
{
    class ConfigFileObjectData : NinjectModule
    {
        public override void Load()
        {
            //this.Bind<IDataAccessObject>().To<FileDataObject>();
            this.Bind<IDataAccessObject>().To<TestDataObject>();
            this.Bind<FileManager>().ToSelf();
        }
    }
}
