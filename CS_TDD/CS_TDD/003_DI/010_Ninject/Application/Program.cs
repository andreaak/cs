using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._002_ConstructorInjection;
using System;

namespace CS_TDD._003_DI._010_Ninject.Application
{
    class Program
    {
        public static void Main()
        {
            string fileName = "file2.log";

            FileManager mgr = new FileManager(new FileDataObject());


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
