using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._008_AbsFactoryInjection.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._008_AbsFactoryInjection
{
    class Program
    {
        static void Main()
        {
            var client = new Client(new ConcreteFactory());

            IFileManager mgr = client.Run();

            string fileName = "file1.txt";

            if (mgr.FindLogFile(fileName))
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
