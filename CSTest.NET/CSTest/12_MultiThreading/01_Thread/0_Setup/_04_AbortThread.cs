using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._01_Thread._0_Setup
{
    class _04_AbortThread
    {
        public Thread Thrd;
        public _04_AbortThread(string name)
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

    class _04_AbortThread2
    {
        public Thread Thrd;
        public _04_AbortThread2(string name)
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
                Debug.WriteLine("Поток прерван, код завершения " + exc.ExceptionState);
            }
        }
    }

    class _04_AbortThreadReset
    {
        public Thread Thrd;
        public _04_AbortThreadReset(string name)
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
                    if (exc.ExceptionState ==  null || (int)exc.ExceptionState == 0)
                    {
                        Debug.WriteLine("Прерывание потока отменено! " + "Код завершения " + exc.ExceptionState);
                        Thread.ResetAbort();
                    }
                    else
                    {
                        Debug.WriteLine("Поток прерван, код завершения " + exc.ExceptionState);
                    }
                }
            }
            Debug.WriteLine(Thrd.Name + " завершен нормально.");
        }
    }

    public static class Setup
    {
        public static void Function()
        {
            //Console.ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                try
                {
                    Thread.Sleep(200);
                    Debug.Write(".");
                }
                catch (ThreadAbortException ex)
                {
                    //Console.ForegroundColor = ConsoleColor.Red;
                    // Попытка 'проглотить' исключение и продолжиться выполняться в данном цикле.
                    // То есть вернуться в цикл и продолжить выводить counter.
                    Debug.WriteLine("\nThreadAbortException");
                    Debug.WriteLine("");
                    for (int i = 0; i < 10; i++)
                    {
                        Thread.Sleep(200);
                        Debug.Write("-");
                    }
                    Debug.WriteLine("");

                    //Console.ForegroundColor = ConsoleColor.Green;

                    // Если не вызывать Thread.ResetAbort() - исключение ThreadAbortException 
                    // повторно сгенерируется после выхода из catch{}
                    // Предотвращение повторной генерации ThreadAbortException!
                    //Thread.ResetAbort();
                }
            }
        }
    }

}
