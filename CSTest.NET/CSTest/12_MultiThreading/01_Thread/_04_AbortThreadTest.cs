using System.Diagnostics;
using System.Threading;
using CSTest._12_MultiThreading._01_Thread._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._01_Thread
{
    [TestFixture]
    public class _04_AbortThreadTest
    {
        [Test]
        // Прервать поток с помощью метода Abort().
        public void TestThreadAbort1()
        {
            _04_AbortThread mtl = new _04_AbortThread("Поток");
            Thread.Sleep(1000); // разрешить порожденному потоку начать свое выполнение 
            Debug.WriteLine("Прерывание потока.");
            mtl.Thrd.Abort();
            mtl.Thrd.Join(); // ожидать прерывания потока 
            Debug.WriteLine("Основной поток прерван.");
            /*
            Поток начат.
            1 2 3 4 5 6 7 8 9 10 
            11 12 13 14 15 16 17 18 19 20 
            21 22 23 24 25 26 27 28 29 30 
            31 32 33 34 35 36 37 38 39 40 
            Прерывание потока.
            A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll

            An exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll but was not handled in user code
            The thread 'Поток' (0x2888) has exited with code 0 (0x0).
            Основной поток прерван.
            */
        }

        [Test]
        public void TestThreadAbort2()
        {
            _04_AbortThread2 mtl = new _04_AbortThread2("Поток");
            Thread.Sleep(1000); // разрешить порожденному потоку начать свое выполнение 
            Debug.WriteLine("Прерывание потока.");
            mtl.Thrd.Abort();
            mtl.Thrd.Join(); // ожидать прерывания потока 
            Debug.WriteLine("Основной поток прерван.");
            
            /*
            Поток начат.
            1 2 3 4 5 6 7 8 9 10 
            11 12 13 14 15 16 17 18 19 20 
            21 22 23 24 25 26 27 28 29 30 
            31 32 33 34 35 36 37 38 39 40 
            Прерывание потока.
            A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll
            Поток прерван, код завершения 
            A first chance exception of type 'System.Threading.ThreadAbortException' occurred in CSTest.dll
            
            An exception of type 'System.Threading.ThreadAbortException' occurred in CSTest.dll but was not handled in user code
            The thread 'Поток' (0x2ac0) has exited with code 0 (0x0).
            Основной поток прерван.            
            */
        }

        [Test]
        // Использовать форму метода Abort(object stateInfo).
        public void TestThreadAbort3()
        {
            Debug.WriteLine("Запуск потока.");
            _04_AbortThreadReset mt1 = new _04_AbortThreadReset("Поток");
            Thread.Sleep(1000); // разрешить порожденному потоку начать свое выполнение 
            Debug.WriteLine("Прерывание потока.");
            mt1.Thrd.Abort(); // это не остановит поток 
            Thread.Sleep(1000); // разрешить порожденному потоку выполняться подольше 
            Debug.WriteLine("Прерывание потока.");
            mt1.Thrd.Abort(100); // а это остановит поток 
            mt1.Thrd.Join(); // ожидать прерывания потока 
            Debug.WriteLine("Основной поток прерван.");
            
            /*
            Запуск потока.
            Поток.начат.
            1 2 3 4 5 6 7 8 9 10 
            11 12 13 14 15 16 17 18 19 20 
            21 22 23 24 25 26 27 28 29 30 
            31 32 33 34 35 36 37 38 39 40 
            Прерывание потока.
            A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll
            Прерывание потока отменено! Код завершения 
            41 42 43 44 45 46 47 48 49 50 
            51 52 53 54 55 56 57 58 59 60 
            61 62 63 64 65 66 67 68 69 70 
            71 72 73 74 75 76 77 78 79 80 
            Прерывание потока.
            A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll
            Поток прерван, код завершения 100
            A first chance exception of type 'System.Threading.ThreadAbortException' occurred in CSTest.dll

            An exception of type 'System.Threading.ThreadAbortException' occurred in CSTest.dll but was not handled in user code
            The thread 'Поток' (0x2034) has exited with code 0 (0x0).
            Основной поток прерван.
            */
        }

        [Test]
        public void TestThreadAbort4()
        {
            Thread thread = new Thread(new ThreadStart(Setup.Function));
            thread.Name = "Поток";
            thread.Start();

            // Даем время запуститься вторичному потоку и поработать.
            Thread.Sleep(2000);

            // Прервать поток. Генерируется исключение ThreadAbortException.
            thread.Abort();

            // Ждать завершения потока.
            thread.Join();

            //Debug.ForegroundColor = ConsoleColor.White;
            Debug.WriteLine("\n" + new string('_', 80));

            //Без Thread.ResetAbort();
            /*
            .........A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll

            ThreadAbortException

            ----------
            A first chance exception of type 'System.Threading.ThreadAbortException' occurred in CSTest.dll
            An exception of type 'System.Threading.ThreadAbortException' occurred in CSTest.dll but was not handled in user code
            The thread 'Поток' (0x1a14) has exited with code 0 (0x0).

            ________________________________________________________________________________
            */

            //При Thread.ResetAbort();
            /*
            .........A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll

            ThreadAbortException

            ----------............................................
            */
        }
    }
}
