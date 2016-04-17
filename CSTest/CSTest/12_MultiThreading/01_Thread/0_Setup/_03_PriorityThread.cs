using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._01_Thread._0_Setup
{
    class _03_PriorityThread
    {
        public int Count;
        public Thread Thread;
        static bool stop = false;
        static string currentName;

        /* Сконструировать новый поток. Данный конструктор еще не начинает выполнение потоков. */
        public _03_PriorityThread(string name)
        {
            Count = 0;
            Thread = new Thread(this.Run);
            Thread.Name = name;
            currentName = name;
        }

        // Начать выполнение нового потока, 
        void Run()
        {
            Debug.WriteLine("Поток " + Thread.Name + " начат.");
            do
            {
                Count++;
                if (currentName != Thread.Name)
                {
                    currentName = Thread.Name;
                    //Debug.WriteLine("В потоке " + currentName);
                }
            } while (stop == false && Count < 20000000);
            stop = true;
            Debug.WriteLine("Поток " + Thread.Name + " завершен.");
        }
    }

    class _03_PriorityThread2
    {
        static bool stop;

        public int Count;
        public Thread Thread;

        public ThreadPriority Priority
        {
            set
            {
                Thread.Priority = value;
            }
        }

        public _03_PriorityThread2(string name, System.ConsoleColor color)
        {
            Thread = new Thread(Run)
            {
                Name = name
            };
            Debug.WriteLine("Поток " + Thread.Name + " начат.");
        }

        void Run()
        {
            do
            {
                Count++;
            }
            while (stop == false && Count < 20000000);

            stop = true;
            Debug.WriteLine("\nПоток " + Thread.Name + " завершен.");
        }

        public void BeginInvoke()
        {
            Thread.Start();
        }

        public void EndInvoke()
        {
            Thread.Join();
        }


    }

}
