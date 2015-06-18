using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestClass]
    public class _01_Monitor
    {
        // Объект для блокировки.
        private static readonly object block = new object();

        // Счетчик потоков.
        static private int counter;
        static private readonly System.Random random = new System.Random();

        [TestMethod]
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
            var threads = new Thread[2];

            new Thread(Function2).Start();

            new Thread(Function3).Start();

            Thread.Sleep(15000);
        }


        // Выполняется в отдельном потоке.
        private static void Function()
        {
            // Управляющий поток увеличивает счетчик и ожидает
            // произвольный период времени от 1 до 12 секунд.

            try
            {
                Monitor.Enter(block); // Начало блокировки.
                ++counter;
            }
            finally
            {
                Monitor.Exit(block);  // Конец блокировки.
            }

            int time = random.Next(1000, 12000);
            Thread.Sleep(time);

            try
            {
                Monitor.Enter(block); // Начало блокировки.
                --counter;
            }
            finally
            {
                Monitor.Exit(block);  // Конец блокировки.
            }
        }

        // Мониторинг количества запущеных потоков.
        private static void Report()
        {
            while (true)
            {
                int count;

                try
                {
                    Monitor.Enter(block);// Начало блокировки.
                    count = counter;
                }
                finally
                {
                    Monitor.Exit(block);
                }

                Debug.WriteLine("{0} поток(ов) активно", count);
                Thread.Sleep(100);
            }
        }

        private static void Function2()
        {
            for (uint i = 0; i < 3; ++i)
            {
                try
                {
                    Monitor.Enter(block); // Начало блокировки.
                    Debug.WriteLine("Начало блокировки Function2");
                    Thread.Sleep(1000);
                    ++counter;
                }
                finally
                {
                    Monitor.Exit(block);  // Конец блокировки.
                    Debug.WriteLine("Конец блокировки Function2");
                }
            }
        }

        private static void Function3()
        {
            for (uint i = 0; i < 3; ++i)
            {
                try
                {
                    Monitor.Enter(block); // Начало блокировки.
                    Debug.WriteLine("Начало блокировки Function3");
                    Thread.Sleep(2000);
                    ++counter;
                }
                finally
                {
                    Debug.WriteLine("Конец блокировки Function3");
                    Monitor.Exit(block);  // Конец блокировки.
                }
            }
        }
    }
}
