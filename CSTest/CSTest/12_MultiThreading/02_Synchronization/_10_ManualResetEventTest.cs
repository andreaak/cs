using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._02_Synchronization
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
        private static ManualResetEvent manual = new ManualResetEvent(false);

        [Test]
        public void TestManualResetEvent1()
        {

            Thread[] threads = { new Thread(Function1), new Thread(Function2) };

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            // Delay.
            Debug.WriteLine("Нажмите на любую клавишу для перевода ManualResetEvent в сигнальное состояние.\n");
            Thread.Sleep(2000);
            Debug.WriteLine("Нажатие кнопки.\n");
            manual.Set(); // Просылает сигнал всем потокам.

            // Delay.
            Thread.Sleep(2000);
            /*
            Нажмите на любую клавишу для перевода ManualResetEvent в сигнальное состояние.

            Поток 1 запущен и ожидает сигнала.
            Поток 2 запущен и ожидает сигнала.
            Нажатие кнопки.

            Поток 1 завершается.
            Поток 2 завершается.
            */
        }
        static void Function1()
        {
            Debug.WriteLine("Поток 1 запущен и ожидает сигнала.");
            manual.WaitOne(); // после завершения WaitOne() ManualResetEvent остаеться в сигнальном сотоянии.
            Debug.WriteLine("Поток 1 завершается.");

        }

        static void Function2()
        {
            Debug.WriteLine("Поток 2 запущен и ожидает сигнала.");
            manual.WaitOne(); // после завершения WaitOne() ManualResetEvent остаеться в сигнальном сотоянии.
            Debug.WriteLine("Поток 2 завершается.");
        }

        [Test]
        public void TestManualResetEvent2()
        {
            // false - установка несигнального состояния.
            var manual = new ManualResetEvent(false);

            var thread = new Work("1", manual);
            Debug.WriteLine("Основной поток ожидает событие.\n");

            manual.WaitOne(); // Ожидать уведомления о событии. 
            Debug.WriteLine("\nОсновной поток получил уведомление о событии от первого потока.\n");

            manual.Reset(); // Сбрасывает в несигнальное состояние.

            thread = new Work("2", manual);
            Debug.WriteLine("Основной поток ожидает событие.\n");

            manual.WaitOne(); // Ожидать уведомления о событии. 
            Debug.WriteLine("\nОсновной поток получил уведомление о событии от второго потока.");

            // Delay.
            Thread.Sleep(10000);

            /*
            Основной поток ожидает событие.

            Запущен поток 1
            . . . . . . . . . . 
            Поток 1 завершен

            Основной поток получил уведомление о событии от первого потока.

            Основной поток ожидает событие.

            Запущен поток 2
            . . . . . . . . . . 
            Поток 2 завершен

            Основной поток получил уведомление о событии от второго потока.
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
            manual.Set();
        }
    }
}
