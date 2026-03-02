using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _10_TestReflected
    {
       

        [Test]
        public void TestAsyncAwait6_ContinueTask()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _10_ClassUnderTestReflected mc = new _10_ClassUnderTestReflected();
            Task task = mc.OperationAsync6_ReturnTask();
            task.ContinueWith(t => Debug.WriteLine("\nПродолжение задачи"));
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation ThreadID 9
            Begin
            Main thread ended. ThreadID 10
            End

            Продолжение задачи
            */
        }

        [Test]
        public void TestAsyncAwait8_ContinueTaskWithResult()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _10_ClassUnderTestReflected mc = new _10_ClassUnderTestReflected();
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
        public void TestAsyncAwait9_ContinueTaskWithArgumentt()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _10_ClassUnderTestReflected mc = new _10_ClassUnderTestReflected();
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

#endif
}
