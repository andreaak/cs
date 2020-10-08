using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._02_Synchronization._0_Setup
{
    class _01_MonitorThread
    {
        // Объект для блокировки.
        private static readonly object block = new object();
        // Счетчик потоков.
        static private int counter;
        static private readonly System.Random random = new System.Random();

        // Выполняется в отдельном потоке.
        public static void Function()
        {
            try
            {
                Monitor.Enter(block); // Начало блокировки.
                Debug.WriteLine(string.Format("{0} start work", Thread.CurrentThread.Name));
                int time = random.Next(200, 1000);
                Thread.Sleep(time);
                Debug.WriteLine(string.Format("{0} end work", Thread.CurrentThread.Name));
            }
            finally
            {
                Monitor.Exit(block);  // Конец блокировки.
            }
        }

        // Выполняется в отдельном потоке.
        public static void Function1()
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

            int time = random.Next(200, 1000);
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
        public static void Report1()
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

        public static void Function2()
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
                    Debug.WriteLine("Конец блокировки Function2");
                    Monitor.Exit(block);  // Конец блокировки.
                }
            }
        }

        public static void Function3()
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
        private static int blockStruct = 0;
        public static void FunctionWithError()
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

        public static void FunctionWithError2()
        {
            int blockStruct2 = 0;
            for (int i = 0; i < 50; ++i)
            {
                // Устанавливается блокировка постоянно в новый object (boxing).
                Monitor.Enter(blockStruct2);
                try
                {
                    Debug.WriteLine(++counter);
                }
                finally
                {
                    // Попытка снять блокировку с незаблокированного объекта (boxing создает новый объект).
                    Monitor.Exit(blockStruct2);
                }
            }
        }
    }
}
