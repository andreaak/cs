using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._01_Thread
{
    // Приоритеты потоков. 
    class MyThreadPriority2
    {
        public Thread RunThread;
        static bool _stop;
        readonly System.ConsoleColor color;

        public int Count
        {
            get;
            set;
        }

        public MyThreadPriority2(string name, System.ConsoleColor color)
        {
            RunThread = new Thread(Run)
            {
                Name = name
            };
            this.color = color;
            //Debug.ForegroundColor = this.color;
            Debug.WriteLine("Поток " + RunThread.Name + " начат.");
        }

        void Run()
        {
            do
            {
                Count++;
            }
            while (_stop == false && Count < 20000000);

            _stop = true;
            Debug.WriteLine("\nПоток " + RunThread.Name + " завершен.");
        }

        public void BeginInvoke()
        {
            RunThread.Start();
        }

        public void EndInvoke()
        {
            RunThread.Join();
        }

        public ThreadPriority Priority
        {
            set
            {
                RunThread.Priority = value;
            }
        }
    }
}
