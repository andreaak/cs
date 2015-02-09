using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Creational.FactoryMethod.Example9
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            IServiceLocator serviceLocator = new ServiceLocator();

            IServiceA serviceA = serviceLocator.GetService<IServiceA>();
            IServiceB serviceB = serviceLocator.GetService<IServiceB>();
            IServiceC serviceC = serviceLocator.GetService<IServiceC>();

            // Задержка.
            Console.ReadKey();
        }

    }
}
