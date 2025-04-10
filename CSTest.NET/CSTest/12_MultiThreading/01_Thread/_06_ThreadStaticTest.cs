using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._01_Thread
{
    [TestFixture]
    public class _06_ThreadStaticTest
    {
        [Test]
        public void TestThreadStatic()
        {
            // Запуск вторичного потока.
            Thread thread = new Thread(Method);
            thread.Start();
            thread.Join();

            Debug.WriteLine("Основной поток завершил работу...");
        }

        // Общая переменная счетчик.
        //[ThreadStatic] //Если снять комментарий то для каждого потока будет своя переменная counter
        private static int counter;

        // Рекурсивный запуск потоков.
        private static void Method()
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
