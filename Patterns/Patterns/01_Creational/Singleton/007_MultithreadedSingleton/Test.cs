using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;

namespace Patterns.Creational.Singleton._007_MultithreadedSingleton
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
            //Debug.ReadKey();
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
            //Debug.ReadKey();
        }

        static void TestMethod()
        {
            MultithreadedSingleton singleton = MultithreadedSingleton.Instance;
            Debug.WriteLine(singleton.GetHashCode());
        }

        static void TestMethod2()
        {
            MutexSingleton singleton = MutexSingleton.Instance;
            Debug.WriteLine(singleton.GetHashCode());
        }
    }
}
