using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSTest._12_MultiThreading._01_Thread
{
    class MyThread
    {
        public int Count;
        string thrdName;
        public MyThread(string name)
        {
            Count = 0;
            thrdName = name;
        }
        // Точка входа в поток, 
        public void Run()
        {
            Debug.WriteLine(thrdName + " начат.");
            do
            {
                Thread.Sleep(100);
                Debug.WriteLine("В потоке " + thrdName + ", Count = " + Count);
                Count++;
            } while (Count < 10);
            Debug.WriteLine(thrdName + " завершен.");
        }
    }
}
