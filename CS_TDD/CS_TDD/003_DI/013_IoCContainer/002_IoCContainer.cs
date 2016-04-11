using CS_TDD._003_DI._013_IoCContainer.Application;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CS_TDD._003_DI._013_IoCContainer
{
    [TestFixture]
    class IoCContainerTest
    {
        [Test]
        public void ResolverTest()
        {
            Resolver resolver = new Resolver();
            resolver.Register<Shopper, Shopper>();
            //resolver.Register<ICreditCard, MasterCard>();
            resolver.Register<ICreditCard, Visa>();

            Shopper shopper = resolver.Resolve<Shopper>();
            shopper.Charge();
        }
    }

    public class Resolver
    {
        private Dictionary<Type, Type> dependencyMap = new Dictionary<Type, Type>();
        
        public T Resolve<T>()
        {
            return (T)Resolve(typeof (T));
        }

        private object Resolve(Type typeToResolve)
        {
            Type resolvedType = null;
            try
            {
                resolvedType = dependencyMap[typeToResolve];
            }
            catch
            {
                throw new Exception(string.Format("Could not resolve type {0}", typeToResolve.FullName));
            }

            ConstructorInfo firstConstructor = resolvedType.GetConstructors().First();
            ParameterInfo[] constructorParameters = firstConstructor.GetParameters();
            if (constructorParameters.Length == 0)
            {
                return Activator.CreateInstance(resolvedType);
            }
            IList<object> parameters = new List<object>();
            foreach(var parameterToResolve in constructorParameters)
            {
                parameters.Add(Resolve(parameterToResolve.ParameterType));
            }

            return firstConstructor.Invoke(parameters.ToArray());
        }

        public void Register<TFrom, TTo>()
        {
            dependencyMap.Add(typeof(TFrom), typeof(TTo));
        }
    }
}
