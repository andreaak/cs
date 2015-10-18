using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestClass]
    class _08_SemaphoreSlimTest
    {
        [TestMethod]
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
        public void TestSemaphore2()
        {
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start();
            }

            Thread.Sleep(1000);
            slim.Release();  // Возможен принудительный сброс из потока владельца семафора.

            // Delay.
            Thread.Sleep(15000);
        }

        static readonly SemaphoreSlim slim = new SemaphoreSlim(1, 2);
        static void Function()
        {
            slim.Wait();

            Debug.WriteLine("Поток {0} начал работу.", Thread.CurrentThread.Name);
            Thread.Sleep(1000);
            Debug.WriteLine("Поток {0} закончил работу.\n", Thread.CurrentThread.Name);

            slim.Release();
        }
    }
}
