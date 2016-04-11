using System.Threading;

namespace Patterns.Creational.Singleton._007_MultithreadedSingleton
{
    class MutexSingleton
    {
        private static volatile MutexSingleton instance;
        private static Mutex mutex = new Mutex();

        private MutexSingleton() { }

        public static MutexSingleton Instance
        {
            get
            {
                Thread.Sleep(500);

                if (instance == null)
                {
                    mutex.WaitOne();

                    if (instance == null)
                        instance = new MutexSingleton();

                    mutex.ReleaseMutex();
                }

                return instance;
            }
        }
    }
}
