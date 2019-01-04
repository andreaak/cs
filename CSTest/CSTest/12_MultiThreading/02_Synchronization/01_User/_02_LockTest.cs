using System.Threading;
using CSTest._12_MultiThreading._02_Synchronization._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization._01_User
{
    [TestFixture]
    /* Критическая секция (critical section).
    Ключевое слово lock не позволит одному потоку войти в важный раздел кода в тот момент, когда в нем находится другой поток. 
    lock - блокирует блок кода так, что в каждый отдельный момент времени, этот блок кода
    сможет использовать только один поток. Все остальные потоки ждут пока текущий поток, закончит работу.
    При попытке входа другого потока в заблокированный код потребуется дождаться снятия блокировки объекта. 
    Ключевое слово lock вызывает Monitor.Enter() в начале блока и Monitor.Exit() в конце блока.
    */
    public class _02_LockTest
    {
        [Test]
        // Использовать блокировку для синхронизации доступа к объекту.
        public void TestLock1()
        {
            int[] a = { 1, 2, 3, 4, 5 };
            _02_LockThread mtl = new _02_LockThread("Потомок #1", a);
            _02_LockThread mt2 = new _02_LockThread("Потомок #2", a);
            mtl.Thread.Join();
            mt2.Thread.Join();
            /*
            Потомок #1 начат.
            Потомок #2 начат.
            Текущая сумма для потока Потомок #1 равна 1
            Текущая сумма для потока Потомок #1 равна 3
            Текущая сумма для потока Потомок #1 равна 6
            Текущая сумма для потока Потомок #1 равна 10
            Текущая сумма для потока Потомок #1 равна 15
            Сумма для потока Потомок #1 равна 15
            Потомок #1 завершен.
            The thread 'Потомок #1' (0x10ec) has exited with code 0 (0x0).
            Текущая сумма для потока Потомок #2 равна 1
            Текущая сумма для потока Потомок #2 равна 3
            Текущая сумма для потока Потомок #2 равна 6
            Текущая сумма для потока Потомок #2 равна 10
            Текущая сумма для потока Потомок #2 равна 15
            Сумма для потока Потомок #2 равна 15
            Потомок #2 завершен.
            The thread 'Потомок #2' (0x2350) has exited with code 0 (0x0).
            */
        }

        [Test]
        // Другой способ блокировки для синхронизации доступа к объекту. 
        public void TestLock2()
        {
            int[] a = { 1, 2, 3, 4, 5 };
            _02_LockThread2 mtl = new _02_LockThread2("Потомок #1", a);
            _02_LockThread2 mt2 = new _02_LockThread2("Потомок #2", a);
            mtl.Thread.Join();
            mt2.Thread.Join();
            /*
            Потомок #1 начат.
            Потомок #2 начат.
            Текущая сумма для потока Потомок #1 равна 1
            Текущая сумма для потока Потомок #1 равна 3
            Текущая сумма для потока Потомок #1 равна 6
            Текущая сумма для потока Потомок #1 равна 10
            Текущая сумма для потока Потомок #1 равна 15
            Текущая сумма для потока Потомок #2 равна 1
            Сумма для потока Потомок #1 равна 15
            Потомок #1 завершен.
            The thread 'Потомок #1' (0x13dc) has exited with code 0 (0x0).
            Текущая сумма для потока Потомок #2 равна 3
            Текущая сумма для потока Потомок #2 равна 6
            Текущая сумма для потока Потомок #2 равна 10
            Текущая сумма для потока Потомок #2 равна 15
            Сумма для потока Потомок #2 равна 15
            Потомок #2 завершен.
            The thread 'Потомок #2' (0x1c54) has exited with code 0 (0x0).
            */
        }

        [Test]
        public void TestLock3()
        {
            new Thread(_02_LockThreadUtils.FunctionCommonLockObject_1).Start();

            new Thread(_02_LockThreadUtils.FunctionCommonLockObject_2).Start();

            Thread.Sleep(2000);
            /*
            Начало блокировки Function2
            Конец блокировки Function2

            Начало блокировки Function3
            Конец блокировки Function3

            Начало блокировки Function2
            Конец блокировки Function2

            Начало блокировки Function3
            Конец блокировки Function3

            Начало блокировки Function2
            Конец блокировки Function2

            Начало блокировки Function3
            The thread 0x13e8 has exited with code 0 (0x0).
            Конец блокировки Function3
            */
        }

        [Test]
        // Lock - не принимает типов значений, а только ссылочные.
        public void TestLock4()
        {
            Thread[] threads = { new Thread(_02_LockThreadUtils.FunctionWithStructLockObject), 
                                   new Thread(_02_LockThreadUtils.FunctionWithStructLockObject), 
                                   new Thread(_02_LockThreadUtils.FunctionWithStructLockObject) };

            foreach (Thread t in threads)
            {
                t.Start();
            }
            Thread.Sleep(5000);
        }

    }
}
