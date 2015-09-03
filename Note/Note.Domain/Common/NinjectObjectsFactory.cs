using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Note.Domain.Repository;
using Utils.WorkWithDB;
using Utils.WorkWithDB.Wrappers;

namespace Note.Domain.Common
{
    public class NinjectObjectsFactory : IObjectsFactory
    {
        private IKernel kernel;
        
        public NinjectObjectsFactory()
        {
            kernel = new StandardKernel(new ObjectData());
        }

        public T GetService<T>()
        {
            return kernel.Get<T>();
        }
    }

    class ObjectData : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDBWrapper>().ToMethod(GetDbWrapper);
            this.Bind<IDataRepository>().To<LinqToSqlRepository>().WithConstructorArgument("connectionString", GetConnectionString);
            this.Bind<DatabaseManager>().ToSelf();
        }

        private static IDBWrapper GetDbWrapper(IContext context)
        {
            return WrapperFactory.GetWrapper();
        }

        private static string GetConnectionString(IContext context)
        {
            return WrapperFactory.GetWrapper().ConnectionString;
        }
    }
}
