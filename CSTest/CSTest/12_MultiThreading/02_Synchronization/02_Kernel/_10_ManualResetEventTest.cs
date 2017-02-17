using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization._02_Kernel
{
    [TestFixture]

    public class _10_ManualResetEventTest
    {
        /*
        ManualResetEvent
	
        ManualResetEvent позволяет потокам взаимодействовать друг с другом путем передачи сигналов. 
        Обычно это взаимодействие касается задачи, которую один поток должен завершить до того, как другой продолжит работу. 
        Когда поток начинает работу, которая должна быть завершена до продолжения работы других потоков, 
        он вызывает метод Reset для того, чтобы поместить ManualResetEvent в несигнальное состояние. 
        Этот поток можно понимать как контролирующий ManualResetEvent. 
        Потоки, которые вызывают метод WaitOne в ManualResetEvent, будут заблокированы, ожидая сигнала. 
        Когда контролирующий поток завершит работу, он вызовет метод Set для сообщения о том, что ожидающие потоки могут продолжить работу. 
        Все ожидающие потоки освобождаются.
        */
        // ManualResetEvent - Уведомляет один или более ожидающих потоков о том, что произошло событие.
        private static ManualResetEvent manual;

        [Test]
        public void TestManualResetEvent1()
        {
            manual = new ManualResetEvent(false);
            Thread[] threads = { new Thread(Function), new Thread(Function) };

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            // Delay.
            Thread.Sleep(500);
            Debug.WriteLine("\nПеревод в сигнальное состояние.");
            manual.Set(); // Просылает сигнал всем потокам.

            // Delay.
            Thread.Sleep(500);
            /*
            Поток 9 запущен и ожидает сигнала.
            Поток 10 запущен и ожидает сигнала.

            Перевод в сигнальное состояние.
            Поток 10 завершается.
            Поток 9 завершается.
            */
        }

        static void Function()
        {
            Debug.WriteLine("Поток {0} запущен и ожидает сигнала.", Thread.CurrentThread.ManagedThreadId);
            manual.WaitOne(); // после завершения WaitOne() ManualResetEvent остаеться в сигнальном сотоянии.
            Debug.WriteLine("Поток {0} завершается.", Thread.CurrentThread.ManagedThreadId);
        }

        [Test]
        public void TestManualResetEvent2()
        {
            manual = new ManualResetEvent(false);
            Thread[] threads = { new Thread(Function2), new Thread(Function2) };

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            // Delay.
            Thread.Sleep(500);
            Debug.WriteLine("\nПеревод 1 в сигнальное состояние.");
            manual.Set(); // Просылает сигнал всем потокам.

            Thread.Sleep(500);
            Debug.WriteLine("\nПеревод 2 в сигнальное состояние.");
            manual.Set(); // Просылает сигнал всем потокам.

            // Delay.
            Thread.Sleep(500);
            /*
            Поток 9 запущен и ожидает сигнала.
            Поток 10 запущен и ожидает сигнала.

            Перевод 1 в сигнальное состояние.
            Поток 10 завершается.
            Поток 9 завершается.

            Перевод 2 в сигнальное состояние.
            */
        }

        static void Function2()
        {
            Debug.WriteLine("Поток {0} запущен и ожидает сигнала.", Thread.CurrentThread.ManagedThreadId);
            manual.WaitOne(); // после завершения WaitOne() ManualResetEvent остаеться в сигнальном сотоянии.
            manual.Reset();//ошибочное решение не приводящее к блокировке втогого потока
            Debug.WriteLine("Поток {0} завершается.", Thread.CurrentThread.ManagedThreadId);
        }

        [Test]
        public void TestManualResetEvent3()
        {
            // false - установка несигнального состояния.
            var manual = new ManualResetEvent(false);

            new Work("1", manual);

            Debug.WriteLine("\nОжидание поток 1.");
            manual.WaitOne(); // Ожидать уведомления о событии. 
            Debug.WriteLine("\nПеревод в не сигнальное состояние.");
            manual.Reset(); // Сбрасывает в несигнальное состояние.

            new Work("2", manual);
            Debug.WriteLine("\nОжидание поток 2.");
            manual.WaitOne(); // Ожидать уведомления о событии. 

            Debug.WriteLine("\nОкончание.");
            /*
            Ожидание поток 1.
            Запущен поток 1
            . . . . . . . . . . 
            Поток 1 завершен

            Перевод в сигнальное состояние.

            Перевод в не сигнальное состояние.

            Ожидание поток 2.
            Запущен поток 2
            . . . . . . . . . . 
            Поток 2 завершен

            Перевод в сигнальное состояние.

            Окончание.
            */
        }
    }

    class Work
    {
        readonly ManualResetEvent manual;
        readonly Thread thread;

        public Work(string name, ManualResetEvent manual)
        {
            this.thread = new Thread(this.Run)
            {
                Name = name
            };
            this.manual = manual;
            this.thread.Start();
        }

        void Run()
        {
            Debug.WriteLine("Запущен поток " + thread.Name);

            for (int i = 0; i < 10; i++)
            {
                Debug.Write(". ");
                Thread.Sleep(200);
            }

            Debug.WriteLine("\nПоток " + thread.Name + " завершен");

            // Уведомление о событии происходит при помощи вызова метода Set()
            Debug.WriteLine("\nПеревод в сигнальное состояние.");
            manual.Set();
        }
    }
}
