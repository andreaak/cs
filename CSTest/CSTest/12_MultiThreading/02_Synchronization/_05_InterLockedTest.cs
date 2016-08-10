using System.Threading;
using CSTest._12_MultiThreading._02_Synchronization._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestFixture]
    /*
    Класс Interlocked предоставляет доступ к атомарным операциям, доступным в нескольких потоках.
    Операции, выполняемые при помощи методов класса Interlocked гарантировано блокируются для остальных потоков, 
    что позволяет избежать некоторых проблем синхронизации.
    */ 
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
            Поток 13 запускается.
            Поток 16 запускается.
            Поток 17 запускается.
            Поток 15 запускается.
            Поток 14 запускается.
            Поток [14] завершается.
            Поток [15] завершается.
            Поток [13] завершается.
            Поток [17] завершается.
            Поток [16] завершается.
            */
        }
    }
}
