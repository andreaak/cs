using NUnit.Framework;
using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestFixture]
    public class _08_SemaphoreSlimTest
    {
        static SemaphoreSlim slim;

        [Test]
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
        public void TestSemaphoreSlim1()
        {
            slim = new SemaphoreSlim(1);
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start();
            }

            // Delay.
            Thread.Sleep(3000);
            /*
            Поток 9 начал работу.
            Поток 9 закончил работу.

            The thread 0x2168 has exited with code 0 (0x0).
            Поток 10 начал работу.
            Поток 10 закончил работу.

            The thread 0x2180 has exited with code 0 (0x0).
            Поток 11 начал работу.
            Поток 11 закончил работу.
            */
        }

        [Test]
        public void TestSemaphoreSlim2()
        {
            slim = new SemaphoreSlim(1, 2);
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start(1000);
            }

            Thread.Sleep(1000);
            Debug.WriteLine("Сброс");
            slim.Release();  // Возможен принудительный сброс из потока владельца семафора.

            // Delay.
            Thread.Sleep(5000);
            /*
            Поток 9 начал работу.
            Сброс
            Поток 10 начал работу.
            Поток 9 закончил работу.

            Поток 11 начал работу.
            The thread 0x23e0 has exited with code 0 (0x0).
            Поток 10 закончил работу.

            The thread 0x23c4 has exited with code 0 (0x0).
            Поток 11 закончил работу.
            */
        }

        [Test]
        public void TestSemaphoreSlim3()
        {
            slim = new SemaphoreSlim(2);
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start(500 * (i + 1));
            }

            // Delay.
            Thread.Sleep(3000);
            /*
            Поток 9 начал работу.
            Поток 10 начал работу.
            Поток 9 закончил работу.

            The thread 0x19ec has exited with code 0 (0x0).
            Поток 11 начал работу.
            Поток 10 закончил работу.

            The thread 0x1014 has exited with code 0 (0x0).
            Поток 11 закончил работу.
            */
        }

        static void Function(object delay)
        {
            slim.Wait();

            Debug.WriteLine("Поток {0} начал работу.", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep((int)delay);
            Debug.WriteLine("Поток {0} закончил работу.\n", Thread.CurrentThread.ManagedThreadId);

            slim.Release();
        }
    }
}
