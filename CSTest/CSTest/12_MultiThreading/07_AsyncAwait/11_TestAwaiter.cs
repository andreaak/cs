using System.Diagnostics;
using System.Threading;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _11_TestAwaiter
    {
        [Test]
        public void TestAsyncAwait_ReturnValue()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _11_ClassWithAwaiter mc = new _11_ClassWithAwaiter();
            mc.OperationAsync4_UseAwaiter_WithResult();
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
            _11_ClassWithAwaiter mc = new _11_ClassWithAwaiter();
            mc.OperationAsync5_CustomAwaiter();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            OperationWithResult ThreadID 7
            OnCompleted CSTest._12_MultiThreading._07_AsyncAwait._11_ClassWithAwaiter+<>c Void <OperationAsync5_CustomAwaiter>b__2_0()
            IsCompleted False
            OnCompleted System.Runtime.CompilerServices.AsyncMethodBuilderCore+MoveNextRunner Void Run()
            Main thread ended. ThreadID 10
            OperationWithResult End ThreadID 7
            Продолжение
            GetResult
            4
            */
        }
    }

#endif
}
