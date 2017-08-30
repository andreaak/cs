using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Parameters;
using Note.Domain.Repository;
using Utils;
using Utils.WorkWithDB.Wrappers;

namespace Note.Domain.Common
{
    public class NinjectObjectsFactory : IObjectsFactory
    {
        private readonly IKernel kernel;

        public NinjectObjectsFactory()
        {
            kernel = new StandardKernel(new ObjectData());
        }

        public T GetService<T>(params KeyValuePair<string, object>[] parameters)
        {
            var pr = parameters.Select(val => new Parameter(val.Key, val.Value, true)).ToArray();

            return kernel.Get<T>(pr);
        }

        public T GetService<T>()
        {
            return kernel.Get<T>();
        }
    }

    public class ObjectData : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDBWrapper>().ToMethod(GetDbWrapper);
            this.Bind<IDataRepository>().To<LinqToSqlRepository>().WithConstructorArgument("connectionString", GetConnectionString);
            this.Bind<DatabaseManager>().ToSelf();
        }

        private static IDBWrapper GetDbWrapper(IContext context)
        {
            var connectionString = context.Parameters.FirstOrDefault(pr => pr.Name == OptionsUtils.ConnectionStringName);
            var csValue = connectionString?.GetValue(context, null)?.ToString();
            var provider = context.Parameters.FirstOrDefault(pr => pr.Name == OptionsUtils.ProviderName);
            var prValue = provider?.GetValue(context, null)?.ToString();

            if (csValue == null)
            {
                return WrapperFactory.GetWrapper(OptionsUtils.Provider, OptionsUtils.ConnectionString);
            }
            return prValue == null ? 
                    WrapperFactory.GetWrapper(OptionsUtils.Provider, csValue) : 
                    WrapperFactory.GetWrapper(prValue, csValue);
        }

        private static string GetConnectionString(IContext context)
        {
            return GetDbWrapper(context).ConnectionString;
        }
    }
}
