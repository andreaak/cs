using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestClass]
    // Использование Mutex для синхронизации доступа к защищенным ресурсам.
    // Mutex - Примитив синхронизации, который также может использоваться в межпроцессной и междоменной синхронизации.
    // MutEx - Mutual Exclusion (Взаимное Исключение).
    public class _07_Mutex
    {
        [TestMethod]
        public void Test1()
        {
            var threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(Function))
                {
                    Name = i.ToString()
                };
                threads[i].Start();
            }

            // Delay.
            Thread.Sleep(15000);
        }

        [TestMethod]
        public void Test2()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(new ThreadStart(Function2));
                thread.Name = String.Format("Поток {0}", i + 1);
                thread.Start();
            }

            // Delay.
            Thread.Sleep(15000);
        }

        [TestMethod]
        // Рекурсивное запирание.
        public void Test3()
        {
            var instance = new SomeClass();

            var thread = new Thread(instance.Method1);
            thread.Start();

            var thread2 = new Thread(instance.Method1);
            thread2.Start();

            // Delay.
            thread.Join();
            thread2.Join();
            // Delay.
            Thread.Sleep(15000);
        }

        // Mutex - Примитив синхронизации, который также может использоваться в межпроцессорной синхронизации.
        // функционирует аналогично AutoResetEvent но снабжен дополнительной логикой:
        // 1. Запоминает какой поток им владеет. ReleaseMutex не может вызвать поток, который не владеет мьютексом.
        // 2. Управляет рекурсивным счетчиком, указывающим, сколько раз поток-владелец уже владел объектом.
        private static readonly Mutex Mutex1 = new Mutex(false, "MutexSample:AAED7056-380D-412E-9608-763495211EA8");
        static void Function()
        {
            bool myMutex = Mutex1.WaitOne();

            Debug.WriteLine("Поток {0} зашел в защищенную область.", Thread.CurrentThread.Name);
            Thread.Sleep(2000);
            Debug.WriteLine("Поток {0}  покинул защищенную область.\n", Thread.CurrentThread.Name);
            Mutex1.ReleaseMutex();
        }


        ///////////////////////
        private static Mutex mutex = new Mutex();
        private static void Function2()
        {
            for (int i = 0; i < 2; i++)
            {
                UseResource();
            }
        }

        // Данный метод представляет собой ресурс, который должен быть синхронизирован так,
        // что только один поток может его выполнять в одно время.
        private static void UseResource()
        {
            // Метод WaitOne используется для запроса на владение мьютексом.
            // Блокирует текущий поток.
            mutex.WaitOne();

            Debug.WriteLine("{0} вошел в защищенную область.", Thread.CurrentThread.Name);
            Thread.Sleep(1000); // Выполнение некоторой работы...
            Debug.WriteLine("{0} покидает защищенную область.\r\n", Thread.CurrentThread.Name);

            mutex.ReleaseMutex();  // Освобождение Mutex.

            Thread.Sleep(1000); // Выполнение некоторой работы...
        }
    }

    // Рекурсивное запирание.
    public class SomeClass : IDisposable
    {
        private Mutex mutex = new Mutex();

        public void Method1()
        {
            mutex.WaitOne();
            Debug.WriteLine("Method1 Start " + Thread.CurrentThread.ManagedThreadId);
            Method2();
            mutex.ReleaseMutex();
            Debug.WriteLine("Method1 End " + Thread.CurrentThread.ManagedThreadId);
        }

        public void Method2()
        {
            mutex.WaitOne();

            Debug.WriteLine("Method2 Start " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            mutex.ReleaseMutex();
            Debug.WriteLine("Method2 End " + Thread.CurrentThread.ManagedThreadId);
        }

        public void Dispose()
        {
            mutex.Dispose();
        }
    }
}
