using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._01_Thread
{
    class MyThreadAutoStart
    {
        public int Count;
        public Thread Thrd;
        public MyThreadAutoStart(string name)
        {
            Count = 0;
            Thrd = new Thread(this.Run);
            Thrd.Name = name; // задать имя потока 
            Thrd.Start(); // начать поток 
        }
        // Точка входа в поток, 
        void Run()
        {
            Debug.WriteLine(Thrd.Name + " начат.");
            do
            {
                Thread.Sleep(100);
                Debug.WriteLine("В потоке " + Thrd.Name + ", Count = " + Count);
                Count++;
            } while (Count < 10);
            Debug.WriteLine(Thrd.Name + " завершен.");
        }
    }
}
