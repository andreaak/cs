using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization._02_Kernel
{
    // Класс Semaphore - используется для управления доступом к пулу ресурсов. 
    // Потоки занимают слот семафора, вызывая метод WaitOne(), и освобождают занятый слот вызовом метода Release().
    [TestFixture]
    public class _08_SemaphoreTest
    {
        private static Semaphore pool;

        [Test]
        public void TestSemaphore1()
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

            /*
            Поток 1 занял слот семафора.
            Поток 4 занял слот семафора.
            Поток 1 -----> освободил слот.
            Поток 3 занял слот семафора.
            Поток 4 -----> освободил слот.
            Поток 8 занял слот семафора.
            Поток 3 -----> освободил слот.
            Поток 7 занял слот семафора.
            Поток 8 -----> освободил слот.
            Поток 2 занял слот семафора.
            Поток 6 занял слот семафора.
            Поток 5 занял слот семафора.
            Поток 7 -----> освободил слот.
            Поток 2 -----> освободил слот.
            Поток 6 -----> освободил слот.
            Поток 5 -----> освободил слот.
            */
        }
        private static void Work(object number)
        {
            pool.WaitOne();

            Debug.WriteLine("Поток {0} занял слот семафора.", number);
            Thread.Sleep(1000);
            Debug.WriteLine("Поток {0} -----> освободил слот.", number);

            pool.Release();
        }
    }
}
