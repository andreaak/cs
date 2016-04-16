using System;
using System.Collections.Generic;
using Patterns._01_Creational.FactoryMethod._009_ServiceLocator.Services;
using Patterns._01_Creational.FactoryMethod._009_ServiceLocator.Services.IServices;

namespace Patterns._01_Creational.FactoryMethod._009_ServiceLocator.Locator
{
    class ServiceLocator : IServiceLocator
    {
        private IDictionary<object, object> services = null;

        // Конструктор.
        internal ServiceLocator()
        {
            this.services = new Dictionary<object, object>();

            this.services.Add(typeof(IServiceA), new ServiceA());
            this.services.Add(typeof(IServiceB), new ServiceB());
            this.services.Add(typeof(IServiceC), new ServiceC());
        }

        public T GetService<T>()
        {
            try
            {
                return (T)services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("Запрашиваемый сервис не зарегистрирован!");
            }
        }
    }
}
