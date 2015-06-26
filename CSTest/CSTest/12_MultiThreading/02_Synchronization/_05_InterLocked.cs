using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestClass]
    public class _05_InterLocked
    {
        [TestMethod]
        // Interlocked - Предоставляет атомарные операции для переменных, общедоступных нескольким потокам. 
        public void Test1()
        {
            var reporter = new Thread(Report)
            {
                IsBackground = true
            };
            reporter.Start();

            var threads = new Thread[150];

            for (uint i = 0; i < 150; ++i)
            {
                threads[i] = new Thread(Function);
                threads[i].Start();
            }

            Thread.Sleep(15000);
        }

        [TestMethod]
        public void Test2()
        {
            var threads = new Thread[50];

            for (uint i = 0; i < 50; ++i)
            {
                threads[i] = new Thread(Function2);
                threads[i].Start();
            }

            // Задержка.
            Console.ReadKey();
        }


        // Счетчик запущеных потоков.
        static private long counter;
        static private readonly Random random = new Random();

        private static void Function()
        {
            // Поток увеличивает счетчик.
            // Interlocked.Increment(ref counter);

            counter++;
            try
            {
                // Поток ожидает произвольный период времени от 1 до 12 секунд.
                int time = random.Next(1, 12);
                Thread.Sleep(time);
            }
            finally
            {
                // Поток уменьшает счетчик. 
                // Interlocked.Decrement(ref counter);
                counter--;
            }
        }

        // Проверка количества запущеных потоков. 
        private static void Report()
        {
            while (true)
            {
                long number = Interlocked.Read(ref counter);

                Debug.WriteLine("{0} поток(ов) активно.", number);
                Thread.Sleep(100);
            }
        }


        //SpinLock
        static readonly SpinLock block = new SpinLock(10); // Интервал 10 млск.
        static readonly FileStream stream = File.Open("log.txt", FileMode.Append, FileAccess.Write, FileShare.None);
        static readonly StreamWriter writer = new StreamWriter(stream);

        // Будет запускаться в отдельном потоке.
        static void Function2()
        {
            using (new SpinLockManager(block)) // Вызывается block.Enter();
            {
                writer.WriteLine("Поток {0} запускается.", Thread.CurrentThread.GetHashCode());
                writer.Flush(); // Очищает буфер writer и записывает данные в файл.
            }   // Вызывается public void Dispose() { block.Exit(); }

            int time = random.Next(10, 200);
            Thread.Sleep(time); // Усыпляется поток на случайный период времени.

            using (new SpinLockManager(block)) // Вызывается block.Enter();
            {
                writer.WriteLine("Поток [{0}] завершается.", Thread.CurrentThread.GetHashCode());
                writer.Flush(); // Очищает буфер writer и записывает данные в файл.
            }   // Вызывается public void Dispose() { block.Exit(); }
        }
    
    }
}
