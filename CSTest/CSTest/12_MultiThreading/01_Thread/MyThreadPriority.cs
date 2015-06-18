using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._01_Thread
{
    class MyThreadPriority
    {
        public int Count;
        public Thread Thrd;
        static bool stop = false;
        static string currentName;
        /* Сконструировать новый поток. Обратите внимание на то, что 
        данный конструктор еще не начинает выполнение потоков. */
        public MyThreadPriority(string name)
        {
            Count = 0;
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            currentName = name;
        }
        // Начать выполнение нового потока, 
        void Run()
        {
            Debug.WriteLine("Поток " + Thrd.Name + " начат.");
            do
            {
                Count++;
                if (currentName != Thrd.Name)
                {
                    currentName = Thrd.Name;
                    Debug.WriteLine("В потоке " + currentName);
                }
            } while (stop == false && Count < 1000000000);
            stop = true;
            Debug.WriteLine("Поток " + Thrd.Name + " завершен.");
        }
    }

}
