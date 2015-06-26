using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._01_Thread
{
    [TestClass]
    public class _06_ThreadStatic
    {
        [TestMethod]
        public void Test()
        {
            // Запуск вторичного потока.
            var thread = new Thread(Method);
            thread.Start();
            thread.Join();

            Debug.WriteLine("Основной поток завершил работу...");

        }

        // Общая переменная счетчик.
        //[ThreadStatic] //Если снять комментарий то для каждого потока будет своя переменная counter
        public static int counter;

        // Рекурсивный запуск потоков.
        public static void Method()
        {
            if (counter < 100)
            {
                counter++; // Увеличение счетчика вызваных методов.
                Debug.WriteLine(counter + " - СТАРТ --- " + Thread.CurrentThread.GetHashCode());
                var thread = new Thread(Method);
                thread.Start();
                thread.Join(); // Закомментировать.               
            }

            Debug.WriteLine("Поток {0} завершился.", Thread.CurrentThread.GetHashCode());
        }
    }
}
