using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Creational.Singleton.Example2
{
    class Singleton
    {
        private static Singleton instance;

        private Singleton()
        {
            Console.WriteLine("Singleton Created");
        }

        public static Singleton GetInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
}
