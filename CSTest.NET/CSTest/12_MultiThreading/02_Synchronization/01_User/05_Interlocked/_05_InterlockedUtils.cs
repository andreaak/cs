using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace CSTest._12_MultiThreading._02_Synchronization._01_User._05_Interlocked
{
    class _05_InterlockedUtils
    {
        // Счетчик запущеных потоков.
        private static long counter;
        private static readonly Random random = new Random();

        // Проверка количества запущеных потоков. 
        public static void Report()
        {
            while (true)
            {
                long number = Interlocked.Read(ref counter);

                Debug.WriteLine("{0} поток(ов) активно.", number);
                Thread.Sleep(100);
            }
        }

        public static void FunctionIncrementDecrement()
        {
            // Поток увеличивает счетчик.
            // Interlocked.Increment(ref counter);

            counter++;
            try
            {
                // Поток ожидает произвольный период времени от 1 до 12 секунд.
                int time = random.Next(200, 1000);
                Thread.Sleep(time);
            }
            finally
            {
                // Поток уменьшает счетчик. 
                // Interlocked.Decrement(ref counter);
                counter--;
            }
        }

        //SpinLock
        static readonly _05_InterlockedExchange block = new _05_InterlockedExchange(10); // Интервал 10 млск.
        static readonly FileStream stream = File.Open("log.txt", FileMode.Append, FileAccess.Write, FileShare.None);
        static readonly StreamWriter writer = new StreamWriter(stream);

        // Будет запускаться в отдельном потоке.
        public static void FunctionInterlockedExchange()
        {
            using (new _05_InterlockedSpinLockManager(block)) // Вызывается block.Enter();
            {
                Debug.WriteLine("Начало записи. Thread = " + Thread.CurrentThread.Name);
                writer.WriteLine("Поток {0} запускается.", Thread.CurrentThread.Name);
                writer.Flush(); // Очищает буфер writer и записывает данные в файл.
                Debug.WriteLine("Окончание записи. Thread = " + Thread.CurrentThread.Name);
            }   // Вызывается public void Dispose() { block.Exit(); }

            int time = random.Next(200, 1000);
            Thread.Sleep(time); // Усыпляется поток на случайный период времени.

            using (new _05_InterlockedSpinLockManager(block)) // Вызывается block.Enter();
            {
                Debug.WriteLine("Начало записи 2. Thread = " + Thread.CurrentThread.Name);
                writer.WriteLine("Поток [{0}] завершается 2.", Thread.CurrentThread.Name);
                writer.Flush(); // Очищает буфер writer и записывает данные в файл.
                Debug.WriteLine("Окончание записи 2. Thread = " + Thread.CurrentThread.Name);
            }   // Вызывается public void Dispose() { block.Exit(); }
        }
    }
}
