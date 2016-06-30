using CS_TDD._003_DI._000_Base;
using System;

namespace CS_TDD._003_DI._003_PropertyInjection
{
    class Program
    {
        static void Main()
        {
            string fileName = "file2.log";

            FileManager mgr = new FileManager();

            mgr.DataAccessObject = new FileDataObject();

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
