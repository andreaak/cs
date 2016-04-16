using System.Diagnostics;

namespace Patterns._01_Creational.Singleton._003_ThreadSafe
{
    class Singleton
    {
        private static Singleton instance;

        private Singleton()
        {
            Debug.WriteLine("Singleton Created");
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
