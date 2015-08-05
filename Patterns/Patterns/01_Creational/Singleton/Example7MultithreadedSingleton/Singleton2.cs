using System.Threading;

namespace MutexSingleton
{
    class Singleton2
    {
        private static volatile Singleton2 instance;
        private static Mutex mutex = new Mutex();

        private Singleton2() { }

        public static Singleton2 Instance
        {
            get
            {
                Thread.Sleep(500);

                if (instance == null)
                {
                    mutex.WaitOne();

                    if (instance == null)
                        instance = new Singleton2();

                    mutex.ReleaseMutex();
                }

                return instance;
            }
        }
    }
}
