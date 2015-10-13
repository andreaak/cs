using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Есть два варианта работы потоков Foreground и Background
// Foreground - Будет работать после завершения работы первичного потока.
// Background - Завершает работу вместе с первичным потоком.
namespace CSTest._12_MultiThreading._01_Thread
{
    [TestClass]
    public class _05_BackgroundTest
    {
        [TestMethod]
        public void Test()
        {
            var thread = new Thread(Function);

            // IsBackground - устанавливает поток как фоновый. Не ждем завершения вторичного потока в данном случае.
            // По умолчанию - thread.IsBackground = false; 

            thread.IsBackground = true; // Закомментировать!
            thread.Start();

            Thread.Sleep(500);

            Debug.WriteLine("\nЗавершение главного потока.");
        }

        private static void Function()
        {
            for (int i = 0; i < 500; i++)
            {
                Thread.Sleep(10);
                Debug.Write(".");
            }
            Debug.WriteLine("\nЗавершение вторичного потока.");
        }


    }
}
