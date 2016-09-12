using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
    [TestFixture]
    public class TestTaskWithResult
    {
        [Test]
        public void TestAsyncAwait8_ContinueTaskWithResult()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassUnderTest mc = new ClassUnderTest();
            Task<int> task = mc.OperationAsync8_ReturnTaskWithResult();
            task.ContinueWith(t => Debug.WriteLine("\nПродолжение задачи Результат : {0}", t.Result));
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation4 ThreadID 9
            Main thread ended. ThreadID 10

            Продолжение задачи Результат : 4
            */
        }

        [Test]
        public void TestAsyncAwait9_ContinueTaskWithArgument()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassUnderTest mc = new ClassUnderTest();
            double argument = 8.0;
            Task<double> task = mc.OperationAsync9_ReturnTaskWithResult_Argument(argument);
            task.ContinueWith(t => Debug.WriteLine("\nПродолжение задачи Результат : {0}", t.Result));
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation8 ThreadID 9
            Main thread ended. ThreadID 10

            Продолжение задачи Результат : 64
            */
        }
    }
}