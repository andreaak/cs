using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization._02_Kernel
{
    [TestFixture]
    public class _09_AutoResetEventTest
    {
        // AutoResetEvent - Уведомляет ожидающий поток о том, что произошло событие. 
        static AutoResetEvent auto;
        
        [Test]
        public void TestAutoResetEvent1()
        {
            auto = new AutoResetEvent(false);
            var thread = new Thread(Function1);
            thread.Start();

            Debug.WriteLine("Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.\n");
            Thread.Sleep(500);
            auto.Set(); // Послать сигнал первому потоку

            Debug.WriteLine("Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.\n");
            Thread.Sleep(500);
            auto.Set(); // Послать сигнал второму потоку

            // Delay.
            Thread.Sleep(200);
            /*
            Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.

            Красный свет
            Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.

            Желтый
            Зеленый
            */
        }

        static void Function1()
        {
            Debug.WriteLine("Красный свет");
            auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
            Debug.WriteLine("Желтый");
            auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
            Debug.WriteLine("Зеленый");
        }

        [Test]
        public void TestAutoResetEvent2()
        {
            auto = new AutoResetEvent(false);
            var thread = new Thread(Function);
            thread.Start();
            var thread2 = new Thread(Function);
            thread2.Start();

            Thread.Sleep(500);
            Debug.WriteLine("\nПеревод 1 в сигнальное состояние.");
            auto.Set(); // Послать сигнал первому потоку

            Thread.Sleep(500);
            Debug.WriteLine("\nПеревод 2 в сигнальное состояние.");
            auto.Set(); // Послать сигнал второму потоку
            // Delay.
            thread.Join();
            thread2.Join();
            /*
            Поток 9 запущен и ожидает сигнала.
            Поток 10 запущен и ожидает сигнала.

            Перевод 1 в сигнальное состояние.
            Поток 9 завершается.

            Перевод 2 в сигнальное состояние.
            Поток 10 завершается.
            */
        }

        [Test]
        public void TestAutoResetEvent3()
        {
            auto = new AutoResetEvent(true);
            var thread = new Thread(Function);
            thread.Start();
            var thread2 = new Thread(Function);
            thread2.Start();

            Thread.Sleep(500);
            Debug.WriteLine("\nПеревод в сигнальное состояние.");
            auto.Set();

            // Delay.
            thread.Join();
            thread2.Join();
            /*
            Поток 9 запущен и ожидает сигнала.
            Поток 10 запущен и ожидает сигнала.
            Поток 9 завершается.

            Перевод в сигнальное состояние.
            Поток 10 завершается.
            */
        }

        static void Function()
        {
            Debug.WriteLine("Поток {0} запущен и ожидает сигнала.", Thread.CurrentThread.ManagedThreadId);
            auto.WaitOne();
            Debug.WriteLine("Поток {0} завершается.", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
