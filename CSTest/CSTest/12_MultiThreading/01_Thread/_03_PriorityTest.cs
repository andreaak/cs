using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._01_Thread
{
    [TestClass]
    public class _03_PriorityTest
    {
        [TestMethod]
        // Продемонстрировать влияние приоритетов потоков.
        public void TestThreadPriority1()
        {
            MyThreadPriority mt1 = new MyThreadPriority("с высоким приоритетом");
            MyThreadPriority mt2 = new MyThreadPriority("с низким приоритетом");
            // Установить приоритеты для потоков. 
            mt1.Thrd.Priority = ThreadPriority.AboveNormal;
            mt2.Thrd.Priority = ThreadPriority.BelowNormal;
            // Начать потоки, 
            mt1.Thrd.Start();
            mt2.Thrd.Start();
            mt1.Thrd.Join();
            mt2.Thrd.Join();
            Debug.WriteLine("");
            Debug.WriteLine("Поток " + mt1.Thrd.Name +
            " досчитал до " + mt1.Count);
            Debug.WriteLine("Поток " + mt2.Thrd.Name +
            " досчитал до " + mt2.Count);

        }

        [TestMethod]
        // Продемонстрировать влияние приоритетов потоков.
        public void TestThreadPriority2()
        {
            //Debug.SetWindowSize(80, 40);

            var work1 = new MyThreadPriority2("с высоким приоритетом", System.ConsoleColor.Red);
            var work2 = new MyThreadPriority2("с низким приоритетом", System.ConsoleColor.Yellow);

            // Установить приоритеты для потоков. 
            work1.Priority = ThreadPriority.Highest;
            work2.Priority = ThreadPriority.Lowest;

            work2.BeginInvoke();
            work1.BeginInvoke();


            work1.EndInvoke();
            work2.EndInvoke();

            Debug.WriteLine("");
            Debug.WriteLine("Поток " + work1.RunThread.Name + " досчитал до " + work1.Count);
            Debug.WriteLine("Поток " + work2.RunThread.Name + " досчитал до " + work2.Count);

            // Delay.
            //Debug.ReadKey();

        }
    }
}
