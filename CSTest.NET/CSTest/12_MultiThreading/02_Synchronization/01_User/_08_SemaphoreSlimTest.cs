﻿using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization._01_User
{
    [TestFixture]
    public class _08_SemaphoreSlimTest
    {
        static SemaphoreSlim slim;

        [Test]
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
        public void TestSemaphoreSlim1()
        {
            slim = new SemaphoreSlim(0, 1);
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start(500);
            }

            // Delay.
            Thread.Sleep(1000);
            Debug.WriteLine("Сброс");
            slim.Release();

            // Delay.
            Thread.Sleep(3000);

            /*
            Сброс
            Поток 9 начал работу.
            Поток 9 закончил работу.

            Поток 10 начал работу.
            The thread 0x4ed4 has exited with code 0 (0x0).
            Поток 10 закончил работу.

            The thread 0x580 has exited with code 0 (0x0).
            Поток 11 начал работу.
            Поток 11 закончил работу.
            */
        }

        [Test]
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
        public void TestSemaphoreSlim2()
        {
            slim = new SemaphoreSlim(1, 1);
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start(500);
            }

            // Delay.
            Thread.Sleep(1000);
            Debug.WriteLine("Сброс");
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
        public void TestSemaphoreSlim3()
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
        public void TestSemaphoreSlim4()
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

        [Test]
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
        public void TestSemaphoreSlim5()
        {
            slim = new SemaphoreSlim(0, 1);
            Task[] tasks = 
            {
                Task.Run(async () => await FunctionAsync(500)),
                Task.Run(async () => await FunctionAsync(500)),
                Task.Run(async () => await FunctionAsync(500))
            };

            // Delay.
            Thread.Sleep(3000);
            Debug.WriteLine("Сброс");
            slim.Release();
            Task.WaitAll(tasks);
            // Delay.
            //Thread.Sleep(3000);

            /*
            Поток 10 начал работу.
            Поток 9 начал работу.
            Поток 11 начал работу.
            Сброс
            Поток 9 в критической секции.
            Поток 9 закончил работу.

            Поток 10 в критической секции.
            Поток 10 закончил работу.

            Поток 9 в критической секции.
            Поток 9 закончил работу.
            */
        }

        [Test]
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
        public void TestSemaphoreSlim6()
        {
            slim = new SemaphoreSlim(1, 1);
            Task[] tasks =
            {
                Task.Run(async () => await FunctionAsync(500)),
                Task.Run(async () => await FunctionAsync(500)),
                Task.Run(async () => await FunctionAsync(500))
            };

            // Delay.
            Thread.Sleep(1000);
            Debug.WriteLine("Wait");
            Task.WaitAll(tasks);
            /*
            Поток 10 начал работу.
            Поток 11 начал работу.
            Поток 9 начал работу.
            Поток 10 в критической секции.
            Поток 10 закончил работу.

            Поток 9 в критической секции.
            Wait
            Поток 9 закончил работу.

            Поток 10 в критической секции.
            Поток 10 закончил работу.
            */
        }

        static void Function(object delay)
        {
            Debug.WriteLine("Поток {0} начал работу.", Thread.CurrentThread.ManagedThreadId);
            slim.Wait();

            Debug.WriteLine("Поток {0} в критической секции.", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep((int)delay);
            Debug.WriteLine("Поток {0} закончил работу.\n", Thread.CurrentThread.ManagedThreadId);

            slim.Release();
        }

        static async Task FunctionAsync(int delay)
        {
            Debug.WriteLine("Поток {0} начал работу.", Thread.CurrentThread.ManagedThreadId);
            await slim.WaitAsync();
            try
            {
                Debug.WriteLine("Поток {0} в критической секции.", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(delay);
                Debug.WriteLine("Поток {0} закончил работу.\n", Thread.CurrentThread.ManagedThreadId);
            }
            finally
            {
                slim.Release();
            }
        }
    }
}
