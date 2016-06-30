using CS_TDD._003_DI._008_AbsFactoryInjection.AbstractFactory;
using System;

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
