using CS_TDD._003_DI._000_Base;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._009_DI_Container
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest5()
        {
            // Создание DI контейнера.
            IUnityContainer container = new UnityContainer();

            // Регистрирование типов интерфейсов и классов, которые их реализуют для инстансирования зависимостей.
            container.RegisterType<IDataAccessObject, TestDataObject>();
            //container.RegisterType<IDataAccessObject, FileDataObject>();

            // Создание объекта типа FileManager и внедрение всех зависимостей
            //в соответствии с зарегистрированными в контейнере типами.
            FileManager manager = container.Resolve<FileManager>();
            Assert.IsTrue(manager.FindLogFile("file2.log"));
        }
    }
}
