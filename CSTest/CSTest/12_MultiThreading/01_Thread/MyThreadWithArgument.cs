using System.Diagnostics;
using System.Threading;

namespace CSTest._12_MultiThreading._01_Thread
{
    class MyThreadWithArgument
    {
        public int Count;
        public Thread Thrd;
        // Обратите внимание на то, что конструктору класса 
        // MyThread передается также значение типа int. 
        public MyThreadWithArgument(string name, int num)
        {
            Count = 0;
            // Вызвать конструктор типа ParameterizedThreadStart 
            // явным образом только ради наглядности примера. 
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            // Здесь переменная num передается методу Start () 
            //в качестве аргумента. 
            Thrd.Start(num);
        }
        // Обратите внимание на то, что в этой форме метода Run() 
        // указывается параметр типа object. 
        void Run(object num)
        {
            Debug.WriteLine(Thrd.Name + " начат со счета " + num);
            do
            {
                Thread.Sleep(100);
                Debug.WriteLine("В потоке " + Thrd.Name + ", Count = " + Count);
                Count++;
            } while (Count < (int)num);
            Debug.WriteLine(Thrd.Name + " завершен.");
        }
    }
}
