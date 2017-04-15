using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization._01_User._05_Interlocked
{
    /*
    Класс Interlocked предоставляет доступ к атомарным операциям, доступным в нескольких потоках.
    Операции, выполняемые при помощи методов класса Interlocked гарантировано блокируются для остальных потоков, 
    что позволяет избежать некоторых проблем синхронизации.
    */
    [TestFixture]
    public class _05_InterlockedTest
    {
        [Test]
        public void TestInterlocked1IncrementDecrement()
        {
            var reporter = new Thread(_05_InterlockedUtils.Report)
            {
                IsBackground = true
            };
            reporter.Start();

            var threads = new Thread[5];

            for (uint i = 0; i < 5; ++i)
            {
                threads[i] = new Thread(_05_InterlockedUtils.FunctionIncrementDecrement);
                threads[i].Name = "Thread " + i;
                threads[i].Start();
            }

            Thread.Sleep(7000);

            /*
            0 поток(ов) активно.
            5 поток(ов) активно.
            5 поток(ов) активно.
            The thread 'Thread 1' (0x1ba0) has exited with code 0 (0x0).
            4 поток(ов) активно.
            4 поток(ов) активно.
            4 поток(ов) активно.
            The thread 'Thread 2' (0xa20) has exited with code 0 (0x0).
            The thread 'Thread 4' (0x2588) has exited with code 0 (0x0).
            2 поток(ов) активно.
            2 поток(ов) активно.
            2 поток(ов) активно.
            The thread 'Thread 0' (0x23bc) has exited with code 0 (0x0).
            The thread 'Thread 3' (0x26d0) has exited with code 0 (0x0).
            0 поток(ов) активно.
            */
        }

        [Test]
        public void TestInterlocked2Exchange()
        {
            var threads = new Thread[5];

            for (uint i = 0; i < 5; ++i)
            {
                threads[i] = new Thread(_05_InterlockedUtils.FunctionInterlockedExchange);
                threads[i].Name = "Thread " + i;
                threads[i].Start();
            }

            Thread.Sleep(7000);

            /*
            Блокировка получена. Thread = Thread 0
            Начало записи. Thread = Thread 0
            Окончание записи. Thread = Thread 0
            Блокировка снята. Thread = Thread 0

            Блокировка получена. Thread = Thread 3
            Начало записи. Thread = Thread 3
            Окончание записи. Thread = Thread 3
            Блокировка снята. Thread = Thread 3

            Блокировка получена. Thread = Thread 4
            Начало записи. Thread = Thread 4
            Окончание записи. Thread = Thread 4
            Блокировка снята. Thread = Thread 4

            Блокировка получена. Thread = Thread 2
            Начало записи. Thread = Thread 2
            Окончание записи. Thread = Thread 2
            Блокировка снята. Thread = Thread 2

            Блокировка получена. Thread = Thread 1
            Начало записи. Thread = Thread 1
            Окончание записи. Thread = Thread 1
            Блокировка снята. Thread = Thread 1

            Блокировка получена. Thread = Thread 1
            Начало записи 2. Thread = Thread 1
            Окончание записи 2. Thread = Thread 1
            Блокировка снята. Thread = Thread 1

            The thread 0x3af8 has exited with code 0 (0x0).
            Блокировка получена. Thread = Thread 3
            Начало записи 2. Thread = Thread 3
            Окончание записи 2. Thread = Thread 3
            Блокировка снята. Thread = Thread 3

            The thread 0x35ac has exited with code 0 (0x0).
            Блокировка получена. Thread = Thread 2
            Начало записи 2. Thread = Thread 2
            Окончание записи 2. Thread = Thread 2
            Блокировка снята. Thread = Thread 2

            The thread 0x39cc has exited with code 0 (0x0).
            Блокировка получена. Thread = Thread 0
            Начало записи 2. Thread = Thread 0
            Окончание записи 2. Thread = Thread 0
            Блокировка снята. Thread = Thread 0

            The thread 0x5584 has exited with code 0 (0x0).
            Блокировка получена. Thread = Thread 4
            Начало записи 2. Thread = Thread 4
            Окончание записи 2. Thread = Thread 4
            Блокировка снята. Thread = Thread 4
            */
        }
    }
}
