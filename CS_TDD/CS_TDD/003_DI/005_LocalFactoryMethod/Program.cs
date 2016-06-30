using System;

namespace CS_TDD._003_DI._005_LocalFactoryMethod
{
    class Program
    {
        static void Main()
        {
            var mgr = new FileManager();

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
