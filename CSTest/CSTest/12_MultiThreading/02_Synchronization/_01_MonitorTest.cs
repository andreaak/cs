using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestClass]
    public class _01_MonitorTest
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
        //Блокировка в разных методах
        public void Test2()
        {
            var threads = new Thread[2];

            new Thread(Function2).Start();

            new Thread(Function3).Start();

            Thread.Sleep(15000);
        }

        [TestMethod]
        // Lock - не принимает типов значений, а только ссылочные.
        public void Test3()
        {
            Thread[] threads = { new Thread(FunctionWithError), new Thread(FunctionWithError), new Thread(FunctionWithError) };

            foreach (Thread t in threads)
            {
                t.Start();
            }
            Thread.Sleep(5000);
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

        // Нельзя использовать объекты блокировки структурного типа.
        // block - не может быть структурным.
        static private int blockStruct = 0;
        static private void FunctionWithError()
        {
            for (int i = 0; i < 50; ++i)
            {
                // Устанавливается блокировка постоянно в новый object (boxing).
                Monitor.Enter((object)blockStruct);
                try
                {
                    Debug.WriteLine(++counter);
                }
                finally
                {
                    // Попытка снять блокировку с незаблокированного объекта (boxing создает новый объект).
                    Monitor.Exit((object)blockStruct);
                }
            }
        }
    }
}
