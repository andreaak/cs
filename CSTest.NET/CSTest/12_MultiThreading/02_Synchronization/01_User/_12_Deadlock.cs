using CSTest._04_Class._13_AnonymousTypes;
using CSTest._12_MultiThreading._02_Synchronization._0_Setup;
using NUnit.Framework;
using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._02_Synchronization._01_User
{
    [TestFixture]
    /*
   */
    public class _12_Deadlock
    {
        [Test]
        public void TestMonitor1()
        {
            Debug.WriteLine("=== Deadlock ===");
            new Thread(DeadlockThread1).Start();
            new Thread(DeadlockThread2).Start();

            Thread.Sleep(200); // небольшая пауза для визуализации

            Debug.WriteLine("\n=== Livelock ===");
            new Thread(LivelockThread1).Start();
            new Thread(LivelockThread2).Start();

            Thread.Sleep(200); // пауза

            Debug.WriteLine("\n=== Starvation ===");
            Thread low = new Thread(StarvationLowPriority) { Priority = ThreadPriority.Lowest };
            Thread high = new Thread(StarvationHighPriority) { Priority = ThreadPriority.Highest };
            low.Start();
            high.Start();
            /*
            === Deadlock ===
            DeadlockThread1 захватил A
            DeadlockThread2 захватил B

            === Livelock ===
            LivelockThread1 выполняет работу
            LivelockThread2 уступает...
            LivelockThread2 выполняет работу

            === Starvation ===
            HighPriorityThread получил lock
            HighPriorityThread получил lock
            HighPriorityThread получил lock
            HighPriorityThread получил lock
            HighPriorityThread получил lock
            LowPriorityThread получил lock
            HighPriorityThread получил lock
            HighPriorityThread получил lock
            HighPriorityThread получил lock
            HighPriorityThread получил lock
            HighPriorityThread получил lock
            */
        }

        [Test]
        public void TestLockThis()
        {
            TestLock obj = new TestLock();

            Thread t1 = new Thread(obj.MethodA);
            Thread t2 = new Thread(() =>
            {
                lock (obj) // кто-то снаружи тоже блокирует этот объект
                {
                    Debug.WriteLine("Внешний поток: захватили obj");
                    Thread.Sleep(2000);
                }
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Debug.WriteLine("Завершено");
            /*
            MethodA: захватили this
            MethodB: захватили this
            Внешний поток: захватили obj
            Завершено
            */
        }

        [Test]
        public void TestLockThis2()
        {
            var t = new Transaction();
            Monitor.Enter(t); // Этот поток получает открытую блокировку объекта
            // Заставляем поток пула вывести время LastTransaction
            // ПРИМЕЧАНИЕ. Поток пула заблокирован до вызова 
            // методом SomeMethod метода Monitor.Exit!

            //Debug.WriteLine(t.LastTransaction);
            ThreadPool.QueueUserWorkItem(o => Debug.WriteLine(t.LastTransaction));
            Thread.Sleep(3000);
            
            // Здесь выполняется какой-то код...
            Monitor.Exit(t);
        
            /*
            MethodA: захватили this
            MethodB: захватили this
            Внешний поток: захватили obj
            Завершено
            */
        }

        static object deadlockA = new object();
        static object deadlockB = new object();

        // Livelock ресурсы
        static bool livelockR1 = true;
        static bool livelockR2 = true;

        // Starvation ресурс
        static object starvationLock = new object();

        static void DeadlockThread1()
        {
            lock (deadlockA)
            {
                Debug.WriteLine("DeadlockThread1 захватил A");
                Thread.Sleep(50);
                lock (deadlockB)
                {
                    Debug.WriteLine("DeadlockThread1 захватил B");
                }
            }
        }

        static void DeadlockThread2()
        {
            lock (deadlockB)
            {
                Debug.WriteLine("DeadlockThread2 захватил B");
                Thread.Sleep(50);
                lock (deadlockA)
                {
                    Debug.WriteLine("DeadlockThread2 захватил A");
                }
            }
        }

        static void LivelockThread1()
        {
            while (true)
            {
                if (livelockR1 && livelockR2)
                {
                    livelockR1 = livelockR2 = false;
                    Debug.WriteLine("LivelockThread1 выполняет работу");
                    livelockR1 = livelockR2 = true;
                    break;
                }
                else
                {
                    Debug.WriteLine("LivelockThread1 уступает...");
                    Thread.Sleep(10);
                }
            }
        }

        static void LivelockThread2()
        {
            while (true)
            {
                if (livelockR1 && livelockR2)
                {
                    livelockR1 = livelockR2 = false;
                    Debug.WriteLine("LivelockThread2 выполняет работу");
                    livelockR1 = livelockR2 = true;
                    break;
                }
                else
                {
                    Debug.WriteLine("LivelockThread2 уступает...");
                    Thread.Sleep(10);
                }
            }
        }

        static void StarvationHighPriority()
        {
            for (int i = 0; i < 10; i++)
            {
                lock (starvationLock)
                {
                    Debug.WriteLine("HighPriorityThread получил lock");
                    Thread.Sleep(10);
                }
            }
        }

        static void StarvationLowPriority()
        {
            lock (starvationLock)
            {
                Debug.WriteLine("LowPriorityThread получил lock");
            }
        }
    }

    class TestLock
    {
        public void MethodA()
        {
            lock (this)
            {
                Debug.WriteLine("MethodA: захватили this");
                Thread.Sleep(1000); // имитация работы
                MethodB();
            }
        }

        public void MethodB()
        {
            lock (this)
            {
                Debug.WriteLine("MethodB: захватили this");
            }
        }
    }

    internal sealed class Transaction
    {
        private DateTime m_timeOfLastTrans;
        public void PerformTransaction()
        {
            Monitor.Enter(this);
            // Этот код имеет эксклюзивный доступ к данным...
            m_timeOfLastTrans = DateTime.Now;
            Monitor.Exit(this);
        }
        public DateTime LastTransaction
        {
            get
            {
                Monitor.Enter(this);
                // Этот код имеет совместный доступ к данным...
                DateTime temp = m_timeOfLastTrans;
                Monitor.Exit(this);
                return temp;
            }
        }
    }

}
