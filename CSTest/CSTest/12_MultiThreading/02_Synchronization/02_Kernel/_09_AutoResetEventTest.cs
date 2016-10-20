using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestFixture]
    public class _09_AutoResetEventTest
    {
        // AutoResetEvent - Уведомляет ожидающий поток о том, что произошло событие. 
        static readonly AutoResetEvent auto = new AutoResetEvent(false);
        
        [Test]
        public void TestAutoResetEvent1()
        {

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
            var thread = new Thread(Function3);
            thread.Start();
            var thread2 = new Thread(Function4);
            thread2.Start();

            Thread.Sleep(1000);
            Debug.WriteLine("Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.\n");
            Thread.Sleep(500);
            auto.Set(); // Послать сигнал первому потоку
            auto.Set(); // Послать сигнал второму потоку
            // Delay.
            thread.Join();
            thread2.Join();
            Thread.Sleep(2000);
            /*
            Поток 1 запущен и ожидает сигнала.
            Поток 2 запущен и ожидает сигнала.
            Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.

            Поток 1 завершается.
            Поток 2 завершается.
            */
        }

        static void Function3()
        {
            Debug.WriteLine("Поток 1 запущен и ожидает сигнала.");
            auto.WaitOne(); // Остановка выполнения вторичного потока 1
            Debug.WriteLine("Поток 1 завершается.");

        }

        static void Function4()
        {
            Debug.WriteLine("Поток 2 запущен и ожидает сигнала.");
            auto.WaitOne(); // Остановка выполнения вторичного потока 2
            Debug.WriteLine("Поток 2 завершается.");
        }
    }
}
