using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._02_Synchronization._0_Setup
{
    class _02_LockThread
    {
        public Thread Thread;
        int[] a;
        int answer;
        // Создать один объект типа SumArray для всех экземпляров класса MyThread. 
        static SumArray sa = new SumArray();
        
        // Сконструировать новый поток. 
        public _02_LockThread(string name, int[] nums)
        {
            a = nums;
            Thread = new Thread(this.Run);
            Thread.Name = name;
            Thread.Start(); // начать поток 
        }

        // Начать выполнение нового потока, 
        void Run()
        {
            Debug.WriteLine(Thread.Name + " начат.");
            answer = sa.SumIt(a);
            Debug.WriteLine("Сумма для потока " + Thread.Name + " равна " + answer);
            Debug.WriteLine(Thread.Name + " завершен.");
        }
    }

    class SumArray
    {
        int sum;
        object lockOn = new object(); // закрытый объект, доступный для последующей блокировки 
        
        public int SumIt(int[] nums)
        {
            lock (lockOn)// заблокировать весь метод 
            { 
                sum = 0; // установить исходное значение суммы 
                for (int i = 0; i < nums.Length; i++)
                {
                    sum += nums[i];
                    Debug.WriteLine("Текущая сумма для потока " + Thread.CurrentThread.Name + " равна " + sum);
                    Thread.Sleep(100); // разрешить переключение задач 
                }
                return sum;
            }
        }
    }

    class _02_LockThread2
    {
        public Thread Thread;
        int[] a;
        int answer;
        /* Создать один объект типа SumArray для всех  экземпляров класса MyThread. */
        static SumArray2 sa = new SumArray2();
        
        // Сконструировать новый поток. 
        public _02_LockThread2(string name, int[] nums)
        {
            a = nums;
            Thread = new Thread(this.Run);
            Thread.Name = name;
            Thread.Start(); // начать поток 
        }
        // Начать выполнение нового потока, 
        void Run()
        {
            Debug.WriteLine(Thread.Name + " начат.");
            lock (sa)// Заблокировать вызовы метода SumIt(). 
            {
                answer = sa.SumIt(a);
            }
            Debug.WriteLine("Сумма для потока " + Thread.Name + " равна " + answer);
            Debug.WriteLine(Thread.Name + " завершен.");
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
                Debug.WriteLine("Текущая сумма для потока " + Thread.CurrentThread.Name + " равна " + sum);
                Thread.Sleep(100); // разрешить переключение задач 
            }
            return sum;
        }
    }

    class _02_LockThreadUtils
    {
        // Объект для блокировки.
        private static readonly object block = new object();

        
        public static void Function2()
        {
            for (uint i = 0; i < 3; ++i)
            {
                lock (block)
                {
                    Debug.WriteLine("Начало блокировки Function2");
                    Thread.Sleep(200);
                    Debug.WriteLine("Конец блокировки Function2");
                }
            }
        }

        public static void Function3()
        {
            for (uint i = 0; i < 3; ++i)
            {
                lock (block)
                {
                    Debug.WriteLine("Начало блокировки Function3");
                    Thread.Sleep(200);
                    Debug.WriteLine("Конец блокировки Function3");
                }
            }
        }

        static private int counter = 0;
        // Нельзя использовать объекты блокировки структурного типа.
        // blockStruct - не может быть структурным.
        static private int blockStruct = 0;
        public static void FunctionWithError()
        {
            for (int i = 0; i < 50; ++i)
            {
                //lock (blockStruct) // Нельзя использовать объекты блокировки структурного типа.
                {
                    Debug.WriteLine(++counter);
                }
            }
        }
    }
}
