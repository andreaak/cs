using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestClass]
    // Класс Semaphore - используется для управления доступом к пулу ресурсов. 
    // Потоки занимают слот семафора, вызывая метод WaitOne(), и освобождают занятый слот вызовом метода Release().
    public class _08_Semaphore
    {
        [TestMethod]
        public void Test1()
        {
            // Первый аргумент:
            // Задаем количество слотов для использования в данный момент (не более максимального клоличества).
            // Второй аргумент:
            // Задаем максимальное количество слотов для данного семафора.
            pool = new Semaphore(2, 4, "MySemafore65487563487");

            for (int i = 1; i <= 8; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(Work));
                thread.Start(i);
            }
            Thread.Sleep(2000);
            pool.Release(2);

            // Delay.
            Thread.Sleep(15000);
        }

        [TestMethod]
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
        public void Test2()
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

        //////////////////////////
        private static Semaphore pool;
        private static void Work(object number)
        {
            pool.WaitOne();

            Debug.WriteLine("Поток {0} занял слот семафора.", number);
            Thread.Sleep(1000);
            Debug.WriteLine("Поток {0} -----> освободил слот.", number);

            pool.Release();
        }

        //////////////////////////
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
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
