using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._01_Thread
{
    class MyThreadAbort
    {
        public Thread Thrd;
        public MyThreadAbort(string name)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            Thrd.Start();
        }
        // Это точка входа в поток, 
        void Run()
        {
            Debug.WriteLine(Thrd.Name + " начат.");
            for (int i = 1; i <= 1000; i++)
            {
                Debug.Write(i + " ");
                if ((i % 10) == 0)
                {
                    Debug.WriteLine("");
                    Thread.Sleep(250);
                }
            }
            Debug.WriteLine(Thrd.Name + " завершен.");
        }
    }

    class MyThreadAbort2
    {
        public Thread Thrd;
        public MyThreadAbort2(string name)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            Thrd.Start();
        }
        // Это точка входа в поток, 
        void Run()
        {
            try
            {
                Debug.WriteLine(Thrd.Name + " начат.");
                for (int i = 1; i <= 1000; i++)
                {
                    Debug.Write(i + " ");
                    if ((i % 10) == 0)
                    {
                        Debug.WriteLine("");
                        Thread.Sleep(250);
                    }
                }
                Debug.WriteLine(Thrd.Name + " завершен нормально.");
            }
            catch (ThreadAbortException exc)
            {
                Debug.WriteLine("Поток прерван, код завершения " +
                exc.ExceptionState);
            }
        }
    }

    class MyThreadResetAbort
    {
        public Thread Thrd;
        public MyThreadResetAbort(string name)
        {
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            Thrd.Start();
        }
        // Это точка входа в поток, 
        void Run()
        {
            Debug.WriteLine(Thrd.Name + ".начат.");
            for (int i = 1; i <= 1000; i++)
            {
                try
                {
                    Debug.Write(i + " ");
                    if ((i % 10) == 0)
                    {
                        Debug.WriteLine("");
                        Thread.Sleep(250);
                    }
                }
                catch (ThreadAbortException exc)
                {
                    if ((int)exc.ExceptionState == 0)
                    {
                        Debug.WriteLine("Прерывание потока отменено! " +
                        "Код завершения " + exc.ExceptionState);
                        Thread.ResetAbort();
                    }
                    else
                        Debug.WriteLine("Поток прерван, код завершения " +
                        exc.ExceptionState);
                }
            }
            Debug.WriteLine(Thrd.Name + " завершен нормально.");
        }
    }

}
