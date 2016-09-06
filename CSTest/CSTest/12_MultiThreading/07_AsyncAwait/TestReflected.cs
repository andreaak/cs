using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class TestReflected
    {
        [Test]
        public void TestAsyncAwait1_ReturnVoid_WithoutActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassUnderTestReflected mc = new ClassUnderTestReflected();
            mc.OperationAsync_ReturnVoid_WithoutActionAfterAwait();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation ThreadID 8
            Begin
            End 
            */
        }

        [Test]
        public void TestAsyncAwait2_ReturnVoid_WithoutActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassUnderTestReflected mc = new ClassUnderTestReflected();
            mc.OperationAsync2_ReturnVoid_WithoutActionAfterAwaitClean();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation ThreadID 8
            Begin
            End 
            */
        }

        [Test]
        public void TestAsyncAwait3_ReturnVoid_ActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassUnderTestReflected mc = new ClassUnderTestReflected();
            mc.OperationAsync_ReturnVoid_ActionAfterAwait();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            MoveNext() call 1 times in ThreadID 10
            OperationAsync (Part I) ThreadID 10
            Operation ThreadID 9
            Begin

            SetStateMachine() ThreadID 10
            stateMachine.GetHashCode() 346948956
            this.GetHashCode() 346948956

            End
            MoveNext() call 2 times in ThreadID 9
            OperationAsync (Part II) ThreadID 9
            */
        }

        [Test]
        public void TestAsyncAwait5_ActionWithResultAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassUnderTestReflected mc = new ClassUnderTestReflected();
            mc.OperationAsync5_ReturnVoid_ActionWithResultAfterAwait();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation4 ThreadID 8
            Main thread ended. ThreadID 10

            Результат: 4
            */
        }

        [Test]
        public void TestAsyncAwait6_ContinueTask()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassUnderTestReflected mc = new ClassUnderTestReflected();
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
            ClassUnderTestReflected mc = new ClassUnderTestReflected();
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
            ClassUnderTestReflected mc = new ClassUnderTestReflected();
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
