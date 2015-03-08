using CS_TDD._003_DI._000_Base;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._009_DI_Container
{
    class Program
    {
        static void Main()
        {
            string fileName = "file2.log";
            // Создания DI контейнера.
            IUnityContainer container = new UnityContainer();

            // Регистрирование типов интерфейсов и классов, которые их реализуют для инстансирования зависимостей.
            container.RegisterType<IDataAccessObject, FileDataObject>();
            //container.RegisterType<IDataAccessObject, StubFileDataObject>();

            // Создание объекта типа FileManager и внедрение всех зависимостей
            //в соответствии с зарегистрированными в контейнере типами.
            FileManager manager = container.Resolve<FileManager>();
            if (manager.FindLogFile(fileName))
            {
                Console.WriteLine("File {0} is found!", fileName);
            }
            else
            {
                Console.WriteLine("File {0} is not found!", fileName);
            }

            // или так
            FileManager manager2 = new FileManager();
            container.BuildUp<FileManager>(manager2);
            if (manager2.FindLogFile("file2.log"))
            {
                Console.WriteLine("File {0} is found!", fileName);
            }
            else
            {
                Console.WriteLine("File {0} is not found!", fileName);
            }

            Console.ReadKey();
        }
    }
}
