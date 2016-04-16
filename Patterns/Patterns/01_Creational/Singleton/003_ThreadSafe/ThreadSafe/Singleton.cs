using System.Diagnostics;

namespace Patterns._01_Creational.Singleton._003_ThreadSafe.ThreadSafe
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
