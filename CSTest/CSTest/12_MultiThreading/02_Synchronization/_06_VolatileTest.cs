using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    /*   
    Ключевое слово volatile можно применять к полям следующих типов:
    1. Ссылочные типы.
    2. Типы, такие как sbyte, byte, short, ushort, int, uint, char, float и bool.
    3. Тип перечисления с одним из следующих базовых типов: byte, sbyte, short, ushort, int или uint.
    4. Параметры универсальных типов, являющиеся ссылочными типами.
    
    Ключевое слово volatile можно применить только к полям класса или структуры.
    Локальные переменные не могут быть объявлены как volatile.
    */

    [TestClass]
    public class _06_VolatileTest
    {
        [TestMethod]
        public void TestVolatile1()
        {
            Debug.WriteLine("Main: запускается поток на 2 секунды.");
            var thread = new Thread(Worker);
            thread.Start();

            Thread.Sleep(2000);

            stop = true;
            Debug.WriteLine("Main: ожидание завершения потока");
            thread.Join();
        }

        /*
        Поля, объявленные как volatile, не проходят оптимизацию компилятором, 
        которая предусматривает доступ посредством отдельного потока. 
        Это гарантирует наличие наиболее актуального значения в поле в любое время.
        Ключевое слово гарантирует что при чтении и записи  манипуляция будет 
        происходить непосредственно с памятью а не со значениями, 
        которые кэшированы в регистры процессора.
       */

        static volatile bool stop; // Без JIT оптимизации.
        //static bool stop; // С JIT оптимизацией.

        private static void Worker(object o)
        {
            /* 
            При компиляции данного кода JIT компилятор обнаружит, что переменная stop не меняется в методе.
            JIT Компилятор может создать код, заранее проверяющий состояние переменной stop
            и если она true сразу вывести результат "Worker: остановлен при x = 0"
            В противном случае JIT компилятор создает код входящий в бесконечный цикл 
            и бесконечно увеличивающий переменную x.
            */

            // ВНИМАНИЕ! Оптимизация не производиться в режиме отладки (DEBUG).
            int x = 0;

            while (!stop)
            {
                // checked
                {
                    x++;
                }
            }

            // Код после JIT оптимизации, если stop не voatile:
            // (Если stop - volatile - то оптимизация JIT компилятором производиться не будет.)
            // int x = 0;         
            // if (stop != true)
            // {
            //     while (true)
            //     {
            //         x++;
            //     }
            // }            

            Debug.WriteLine("Worker: остановлен при x = {0}.", x);
        }

        [TestMethod]
        // Альтернативные операции VolatileWrite() и VolatileRead() ключевому слову volatile.
        public void TestVolatile2()
        {
            Debug.WriteLine("Main: запускается поток на 2 секунды.");
            var t = new Thread(Worker2);
            t.Start();

            Thread.Sleep(2000);

            Thread.VolatileWrite(ref stop2, 1);

            Debug.WriteLine("Main: ожидание завершения потока.");
            t.Join();
        }

        // static volatile int stop2 = 0;
        static int stop2;
        private static void Worker2(object o)
        {
            int x = 0;

            while (Thread.VolatileRead(ref stop2) != 1)
            {
                x++;
            }

            Debug.WriteLine("Worker: остановлен при x = {0}.", x);
        }
    }

    public sealed class DoubleCheckedLock
    {
        // Поле должно быть volatile!
        private static volatile DoubleCheckedLock _instance;
        private static readonly object _syncRoot = new object();

        private DoubleCheckedLock()
        {
        }

        public static DoubleCheckedLock Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new DoubleCheckedLock();

                        }
                    }
                }
                return _instance;
            }
        }
    }
}
