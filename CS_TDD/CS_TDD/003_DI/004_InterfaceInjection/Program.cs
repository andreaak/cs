using CS_TDD._003_DI._000_Base;
using System;

namespace CS_TDD._003_DI._004_InterfaceInjection
{
    class Program
    {
        static void Main()
        {
            string fileName = "file2.log";

            FileManager mgr = new FileManager();

            // Внедерние зависимости.
            //mgr.FindLogFile(fileName, new StubFileDataObject());
            if (mgr.FindLogFile(fileName, new FileDataObject()))
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
