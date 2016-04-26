using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._000_Base.Test;
using CS_TDD._003_DI._014_Unity.Application;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace CS_TDD._003_DI._014_Unity.Test
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void DIContainerTest()
        {
            // Создание DI контейнера.
            IUnityContainer container = new UnityContainer();

            // Регистрирование типов интерфейсов и классов, которые их реализуют для инстансирования зависимостей.
            container.RegisterType<IDataAccessObject, StubFileDataObject>();
            //container.RegisterType<IDataAccessObject, FileDataObject>();

            // Создание объекта типа FileManager и внедрение всех зависимостей
            //в соответствии с зарегистрированными в контейнере типами.
            FileManager manager = container.Resolve<FileManager>();
            Assert.IsTrue(manager.FindLogFile("file2.log"));
        }
    }
}
