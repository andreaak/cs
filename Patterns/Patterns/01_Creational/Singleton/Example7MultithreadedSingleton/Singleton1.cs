using System;
using System.Threading;

namespace MultithreadedSingleton
{
    class Singleton1
    {
        private static volatile Singleton1 instance;
        private static object syncRoot = new Object();

        private Singleton1() { }

        public static Singleton1 Instance
        {
            get
            {
                Thread.Sleep(500);

                if (instance == null) 
                {
                    lock (syncRoot) // Закомментировать lock для проверки.
                    {
                        if (instance == null)
                            instance = new Singleton1();
                    }
                }

                return instance;
            }
        }
    }
}
