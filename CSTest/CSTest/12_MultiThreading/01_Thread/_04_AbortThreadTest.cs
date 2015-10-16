using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._01_Thread
{
    [TestClass]
    public class _04_AbortThreadTest
    {
        [TestMethod]
        // Прервать поток с помощью метода Abort().
        public void TestThreadAbort1()
        {
            MyThreadAbort mtl = new MyThreadAbort("Мой Поток");
            Thread.Sleep(1000); // разрешить порожденному потоку начать свое выполнение 
            Debug.WriteLine("Прерывание потока.");
            mtl.Thrd.Abort();
            mtl.Thrd.Join(); // ожидать прерывания потока 
            Debug.WriteLine("Основной поток прерван.");
        }

        [TestMethod]
        // Использовать форму метода Abort(object statelnfo) .
        public void TestThreadAbort2()
        {
            MyThreadAbort2 mtl = new MyThreadAbort2("Мой Поток");
            Thread.Sleep(1000); // разрешить порожденному потоку начать свое выполнение 
            Debug.WriteLine("Прерывание потока.");
            mtl.Thrd.Abort();
            mtl.Thrd.Join(); // ожидать прерывания потока 
            Debug.WriteLine("Основной поток прерван.");
        }

        [TestMethod]
        public void TestThreadAbort3()
        {
            MyThreadResetAbort mt1 = new MyThreadResetAbort("Мой Поток");
            Thread.Sleep(1000); // разрешить порожденному потоку начать свое выполнение 
            Debug.WriteLine("Прерывание потока.");
            mt1.Thrd.Abort(); // это не остановит поток 
            Thread.Sleep(1000); // разрешить порожденному потоку выполняться подольше 
            Debug.WriteLine("Прерывание потока.");
            mt1.Thrd.Abort(100); // а это остановит поток 
            mt1.Thrd.Join(); // ожидать прерывания потока 
            Debug.WriteLine("Основной поток прерван.");
        }

        [TestMethod]
        public void TestThreadAbort4()
        {
            Thread thread = new Thread(new ThreadStart(Function));
            thread.Start();

            // Даем время запуститься вторичному потоку и поработать.
            Thread.Sleep(2000);

            // Прервать поток. Генерируется исключение ThreadAbortException.
            thread.Abort(); // Закоментировать!

            // Ждать завершения потока.
            thread.Join();

            //Debug.ForegroundColor = ConsoleColor.White;
            Debug.WriteLine("\n" + new string('_', 80));

        }

        private static void Function()
        {
            //Debug.ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                try
                {
                    Thread.Sleep(10);
                    Debug.Write(".");
                }
                catch (ThreadAbortException ex)
                {
                    //Debug.ForegroundColor = ConsoleColor.Red;
                    // Попытка 'проглотить' исключение и продолжиться выполняться в данном цикле.
                    // То есть вернуться в цикл и продолжить выводить counter.
                    Debug.WriteLine("\nThreadAbortException");

                    for (int i = 0; i < 160; i++)
                    {
                        Thread.Sleep(20);
                        Debug.Write(".");
                    }

                    //Debug.ForegroundColor = ConsoleColor.Green;

                    // Если не вызывать Thread.ResetAbort() - исключение повторно сгенерируется после выхода из catch{}
                    // Предотвращение повторной генерации ThreadAbortException!
                    Thread.ResetAbort();
                    break;
                }
            }
        }
    }
}
