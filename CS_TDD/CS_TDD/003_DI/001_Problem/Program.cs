using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._001_Problem
{
    class Program
    {
        static void Main()
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
