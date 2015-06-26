using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestClass]
    public class _09_AutoResetEvent
    {
        [TestMethod]
        public void Test()
        {
            Debug.WriteLine("Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.\n");

            var thread = new Thread(Function1);
            thread.Start();

            // Delay.
            Thread.Sleep(15000);
            auto.Set(); // Послать сигнал первому потоку

            // Delay.
            Thread.Sleep(15000);
            auto.Set(); // Послать сигнал второму потоку

            // Delay.
            Thread.Sleep(15000);
        }

        // AutoResetEvent - Уведомляет ожидающий поток о том, что произошло событие. 
        static readonly AutoResetEvent auto = new AutoResetEvent(false);
        static void Function1()
        {
            Debug.WriteLine("Красный свет");
            auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
            Debug.WriteLine("Желтый");
            auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
            Debug.WriteLine("Зеленый");
        }
    }
}
