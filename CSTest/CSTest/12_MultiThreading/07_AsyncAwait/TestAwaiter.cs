using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class TestAwaiter
    {
        [Test]
        public void TestAsyncAwait4_ReturnValue()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassWithAwaiter mc = new ClassWithAwaiter();
            mc.OperationAsync4_ReturnVoid_UseAwaiter();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Main thread ended. ThreadID 10
            Operation4 ThreadID 9

            Результат: 4
            */
        }

        [Test]
        public void TestAsyncAwait5_CustomAwaiter()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassWithAwaiter mc = new ClassWithAwaiter();
            mc.OperationAsync5_CustomAwaiter();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            /*
            Main ThreadID 8
            OnCompleted
            IsCompleted
            GetResult
            Return Value
            Main thread ended. ThreadID 8
            */
        }
    }

    #endif
}
