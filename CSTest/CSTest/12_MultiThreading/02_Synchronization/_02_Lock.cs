using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._02_Synchronization
{
    [TestClass]
    public class _02_Lock
    {
        // Объект для блокировки.
        private static readonly object block = new object();
        
        [TestMethod]
        // Использовать блокировку для синхронизации доступа к объекту.
        public void Test1()
        {
            int[] a = { 1, 2, 3, 4, 5 };
            MyThread mtl = new MyThread("Потомок #1", a);
            MyThread mt2 = new MyThread("Потомок #2", a);
            mtl.Thrd.Join();
            mt2.Thrd.Join();
        }

        [TestMethod]
        // Другой способ блокировки для синхронизации доступа к объекту. 
        public void Test2()
        {
            int[] a = { 1, 2, 3, 4, 5 };
            MyThread2 mtl = new MyThread2("Потомок #1", a);
            MyThread2 mt2 = new MyThread2("Потомок #2", a);
            mtl.Thrd.Join();
            mt2.Thrd.Join();
        }

        [TestMethod]
        public void Test3()
        {
            var threads = new Thread[2];

            new Thread(Function2).Start();

            new Thread(Function3).Start();

            Thread.Sleep(15000);
        }

        class SumArray
        {
            int sum;
            object lockOn = new object(); // закрытый объект, доступный 
            // для последующей блокировки 
            public int SumIt(int[] nums)
            {
                lock (lockOn)
                { // заблокировать весь метод 
                    sum = 0; // установить исходное значение суммы 
                    for (int i = 0; i < nums.Length; i++)
                    {
                        sum += nums[i];
                        Debug.WriteLine("Текущая сумма для потока " +
                        Thread.CurrentThread.Name + " равна " + sum);
                        Thread.Sleep(100); // разрешить переключение задач 
                    }
                    return sum;
                }
            }
        }

        class SumArray2
        {
            int sum;
            public int SumIt(int[] nums)
            {
                sum = 0; // установить исходное значение суммы 
                for (int i = 0; i < nums.Length; i++)
                {
                    sum += nums[i];
                    Debug.WriteLine("Текущая сумма для потока " +
                    Thread.CurrentThread.Name + " равна " + sum);
                    Thread.Sleep(100); // разрешить переключение задач 
                }
                return sum;
            }
        }

        class MyThread
        {
            public Thread Thrd;
            int[] a;
            int answer;
            // Создать один объект типа SumArray для всех 
            // экземпляров класса MyThread. 
            static SumArray sa = new SumArray();
            // Сконструировать новый поток. 
            public MyThread(string name, int[] nums)
            {
                a = nums;
                Thrd = new Thread(this.Run);
                Thrd.Name = name;
                Thrd.Start(); // начать поток 
            }
            // Начать выполнение нового потока, 
            void Run()
            {
                Debug.WriteLine(Thrd.Name + " начат.");
                answer = sa.SumIt(a);
                Debug.WriteLine("Сумма для потока " + Thrd.Name + " равна " + answer);
                Debug.WriteLine(Thrd.Name + " завершен.");
            }
        }

        class MyThread2
        {
            public Thread Thrd;
            int[] a;
            int answer;
            /* Создать один объект типа SumArray для всех 
            экземпляров класса MyThread. */
            static SumArray2 sa = new SumArray2();
            // Сконструировать новый поток. 
            public MyThread2(string name, int[] nums)
            {
                a = nums;
                Thrd = new Thread(this.Run);
                Thrd.Name = name;
                Thrd.Start(); // начать поток 
            }
            // Начать выполнение нового потока, 
            void Run()
            {
                Debug.WriteLine(Thrd.Name + " начат.");
                // Заблокировать вызовы метода Sumlt(). 
                lock (sa)
                {
                    answer = sa.SumIt(a);
                }
                Debug.WriteLine("Сумма для потока " + Thrd.Name +
                " равна " + answer);
                Debug.WriteLine(Thrd.Name + " завершен.");
            }
        }

        private static void Function2()
        {
            for (uint i = 0; i < 3; ++i)
            {
                lock (block)
                {
                    Debug.WriteLine("Начало блокировки Function2");
                    Thread.Sleep(1000);
                    Debug.WriteLine("Конец блокировки Function2");
                }
            }
        }

        private static void Function3()
        {
            for (uint i = 0; i < 3; ++i)
            {
                lock (block)
                {
                    Debug.WriteLine("Начало блокировки Function3");
                    Thread.Sleep(1000);
                    Debug.WriteLine("Конец блокировки Function3");
                }
            }
        }
    
    }
}
