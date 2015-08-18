using System;
using CS_TDD._003_DI._006_FactoryInjection;

namespace CS_TDD._003_DI._007_AbstractTestClassPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "file2.log";

            FileManager mgr = new FileManager();

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
