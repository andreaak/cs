using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadedSingleton;
using MutexSingleton;
using System;
using System.Threading;

namespace Patterns.Creational.Singleton.Example7
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Thread[] threads = { 
                                   new Thread(TestMethod), 
                                   new Thread(TestMethod) 
                               };

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            // Delay.
            //Console.ReadKey();
        }

        [TestMethod]
        public void Test2()
        {
            Thread[] threads = { 
                                   new Thread(TestMethod2), 
                                   new Thread(TestMethod2) 
                               };

            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            
            // Delay.
            //Console.ReadKey();
        }

        static void TestMethod()
        {
            Singleton1 singleton = Singleton1.Instance;
            Console.WriteLine(singleton.GetHashCode());
        }

        static void TestMethod2()
        {
            Singleton2 singleton = Singleton2.Instance;
            Console.WriteLine(singleton.GetHashCode());
        }
    }
}
