using System;
using System.Threading;

namespace Patterns.Creational.Singleton._007_MultithreadedSingleton
{
    class MultithreadedSingleton
    {
        private static volatile MultithreadedSingleton instance;
        private static object syncRoot = new Object();

        private MultithreadedSingleton() { }

        public static MultithreadedSingleton Instance
        {
            get
            {
                Thread.Sleep(500);

                if (instance == null) 
                {
                    lock (syncRoot) // Закомментировать lock для проверки.
                    {
                        if (instance == null)
                            instance = new MultithreadedSingleton();
                    }
                }

                return instance;
            }
        }
    }
}
