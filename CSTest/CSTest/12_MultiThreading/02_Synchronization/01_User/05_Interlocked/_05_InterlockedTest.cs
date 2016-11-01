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
        public void TestInterlocked1()
        {
            var reporter = new Thread(_05_InterlockedUtils.Report)
            {
                IsBackground = true
            };
            reporter.Start();

            var threads = new Thread[5];

            for (uint i = 0; i < 5; ++i)
            {
                threads[i] = new Thread(_05_InterlockedUtils.Function);
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
        public void TestInterlocked2()
        {
            var threads = new Thread[5];

            for (uint i = 0; i < 5; ++i)
            {
                threads[i] = new Thread(_05_InterlockedUtils.Function2);
                threads[i].Name = "Thread " + i;
                threads[i].Start();
            }

            Thread.Sleep(7000);

            /*
            Блокировка получена. Thread = 11
            Начало записи. Thread = 11
            Окончание записи. Thread = 11
            Блокировка снята. Thread = 11
            Блокировка получена. Thread = 10
            Начало записи. Thread = 10
            Окончание записи. Thread = 10
            Блокировка снята. Thread = 10
            Блокировка получена. Thread = 15
            Начало записи. Thread = 15
            Окончание записи. Thread = 15
            Блокировка снята. Thread = 15
            Блокировка получена. Thread = 14
            Начало записи. Thread = 14
            Окончание записи. Thread = 14
            Блокировка снята. Thread = 14
            Блокировка получена. Thread = 12
            Начало записи. Thread = 12
            Окончание записи. Thread = 12
            Блокировка снята. Thread = 12
            Блокировка получена. Thread = 10
            Начало записи. Thread = 10
            Окончание записи. Thread = 10
            Блокировка снята. Thread = 10
            The thread 0x24b0 has exited with code 0 (0x0).
            Блокировка получена. Thread = 11
            Начало записи. Thread = 11
            Окончание записи. Thread = 11
            Блокировка снята. Thread = 11
            The thread 0x1b64 has exited with code 0 (0x0).
            Блокировка получена. Thread = 15
            Начало записи. Thread = 15
            Окончание записи. Thread = 15
            Блокировка снята. Thread = 15
            The thread 0x26d0 has exited with code 0 (0x0).
            Блокировка получена. Thread = 14
            Начало записи. Thread = 14
            Окончание записи. Thread = 14
            Блокировка снята. Thread = 14
            The thread 0x1600 has exited with code 0 (0x0).
            Блокировка получена. Thread = 12
            Начало записи. Thread = 12
            Окончание записи. Thread = 12
            Блокировка снята. Thread = 12
            */
        }
    }
}
