using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Creational.Singleton.Example2.ThreadSafe
{
    class Singleton
    {
        private static Singleton instance;
        private static object locking = new object();

        private Singleton()
        {
            Debug.WriteLine("Singleton Created");
        }

        public static Singleton GetInstance()
        {

            if (instance == null)
            {
                lock (locking)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
}
