using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._01_Thread
{
    class _01_BasicThread
    {
        public int Count;
        string threadName;

        public _01_BasicThread(string name)
        {
            Count = 0;
            threadName = name;
        }
        // Точка входа в поток, 
        public void Run()
        {
            string temp = new string(' ', 10);
            Debug.WriteLine(temp + threadName + " начат.");
            Debug.WriteLine(temp + "Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine(temp + "Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            
            do
            {
                Thread.Sleep(100);
                Debug.WriteLine(temp + "В потоке " + threadName + ", Count = " + Count);
                Count++;
            } while (Count < 10);
            Debug.WriteLine(temp + threadName + " завершен.");
        }
    }

    class _01_BasicThreadAutoStart
    {
        public int Count;
        public Thread Thread;

        public _01_BasicThreadAutoStart(string name)
        {
            Count = 0;
            ThreadStart method = new ThreadStart(this.Run);
            Thread = new Thread(method);
            Thread.Name = name; // задать имя потока 
            Thread.Start(); // начать поток 
        }
        // Точка входа в поток, 
        void Run()
        {
            string temp = new string(' ', 10);
            Debug.WriteLine(temp + Thread.Name + " начат.");
            Debug.WriteLine(temp + "Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine(temp + "Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            
            do
            {
                Thread.Sleep(100);
                Debug.WriteLine(temp + "В потоке " + Thread.Name + ", Count = " + Count);
                Count++;
            } while (Count < 10);
            Debug.WriteLine(temp + Thread.Name + " завершен.");
        }
    }

    class _01_BasicThreadWithArgument
    {
        public int Count;
        string threadName;

        public _01_BasicThreadWithArgument(string name)
        {
            Count = 0;
            threadName = name;
        }
        // Точка входа в поток, 
        public void Run(object argument)
        {
            int num = (int)argument;
            string temp = new string(' ', 10);
            Debug.WriteLine(temp + threadName + " начат со счета " + num);
            Debug.WriteLine(temp + "Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine(temp + "Thread Id {0}", Thread.CurrentThread.ManagedThreadId);

            do
            {
                Thread.Sleep(100);
                Debug.WriteLine(temp + "В потоке " + threadName + ", Count = " + Count);
                Count++;
            } while (Count < num);
            Debug.WriteLine(temp + threadName + " завершен.");
        }
    }

    class _01_BasicThreadAutoStartWithArgument
    {
        public int Count;
        public Thread Thread;

        // Обратите внимание на то, что конструктору класса 
        // MyThread передается также значение типа int. 
        public _01_BasicThreadAutoStartWithArgument(string name, int num)
        {
            Count = 0;
            // Вызвать конструктор типа ParameterizedThreadStart 
            // явным образом только ради наглядности примера. 
            Thread = new Thread(this.Run);
            Thread.Name = name;
            // Здесь переменная num передается методу Start () 
            //в качестве аргумента. 
            Thread.Start(num);
        }
        // Обратите внимание на то, что в этой форме метода Run() 
        // указывается параметр типа object. 
        void Run(object argument)
        {
            int num = (int)argument;
            string temp = new string(' ', 10);
            Debug.WriteLine(temp + Thread.Name + " начат со счета " + num);
            Debug.WriteLine(temp + "Thread HashCode {0}", Thread.CurrentThread.GetHashCode());
            Debug.WriteLine(temp + "Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            
            do
            {
                Thread.Sleep(100);
                Debug.WriteLine(temp + "В потоке " + Thread.Name + ", Count = " + Count);
                Count++;
            } while (Count < num);
            Debug.WriteLine(temp + Thread.Name + " завершен.");
        }
    }

    class _01_BasicThreadUtils
    {
        public static void WriteSecond()
        {
            WriteSecond(new string(' ', 10));
        }

        // Статический метод, который планируется выполнять одновременно в главном (первичном) и во вторичном потоках.
        public static void WriteSecond(string indent)
        {
            // CLR назначает каждому потоку свой стек и методы имеют свои собственные локальные переменные.
            // Отдельный экземпляр переменной counter создается в стеке каждого потока, 
            // поэтому для каждого потока выводятся, свои значения counter - 0,1,2.
            int counter = 0;

            while (counter < 10)
            {
                Debug.WriteLine(indent + "Thread Id {0}, counter = {1}", Thread.CurrentThread.GetHashCode(), counter);
                counter++;
            }
        }
        
        // Общая переменная счетчик.
        // По умолчанию статическая переменная одна для всех потоков.
        // Если нужна индивидуальная статическая переменная для каждого потока то употребляют аттрибут
        //[ThreadStatic]
        public static int counter;

        // Рекурсивный запуск потоков.
        public static void Method()
        {
            if (counter < 5)
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
