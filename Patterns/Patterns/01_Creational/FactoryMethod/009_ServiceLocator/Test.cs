﻿using NUnit.Framework;
using Patterns._01_Creational.FactoryMethod._009_ServiceLocator.Locator;
using Patterns._01_Creational.FactoryMethod._009_ServiceLocator.Services.IServices;

namespace Patterns._01_Creational.FactoryMethod._009_ServiceLocator
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            IServiceLocator serviceLocator = new ServiceLocator();

            IServiceA serviceA = serviceLocator.GetService<IServiceA>();
            IServiceB serviceB = serviceLocator.GetService<IServiceB>();
            IServiceC serviceC = serviceLocator.GetService<IServiceC>();

            // Задержка.
            //Debug.ReadKey();
        }

    }
}
