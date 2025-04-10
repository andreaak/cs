using System.Diagnostics;
using System.Threading;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _07_TestCancel
    {
        [Test]
        public void TestAsyncAwait_CancelToken1()
        {
            Debug.WriteLine("Staring async download");
            var mc = new _07_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoAsync_Generic(cts, mc.SumPageSizesAsync);
            Debug.WriteLine("Async download started");

            Thread.Sleep(2000);
            Debug.WriteLine("Cancel Task");
            cts.Cancel();
            Thread.Sleep(5000);
            /*
            Staring async download
            Async download started
            Cancel Task
            Exception thrown: 'System.Threading.Tasks.TaskCanceledException' in mscorlib.dll
            Exception thrown: 'System.Threading.Tasks.TaskCanceledException' in mscorlib.dll
            Operation canceled.
            DoAsync_Generic ended
            */
        }


        [Test]
        public void TestAsyncAwait_CancelToken2()
        {
            Debug.WriteLine("Staring async download");
            var mc = new _07_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoAsync_Generic(cts, mc.DoAsync_CancelToken);
            Debug.WriteLine("Async download started");

            Thread.Sleep(2000);
            Debug.WriteLine("Cancel Task");
            cts.Cancel();
            Thread.Sleep(5000);
            /*
            Staring async download
            Task 1 started
            Async download started
            Task 1 ended
            Task 2 started
            Task 2 ended
            Task 3 started
            Task 3 ended
            Task 4 started
            Task 4 ended
            Task 5 started
            Task 5 ended
            Task 6 started
            Cancel Task
            Exception thrown: 'System.Threading.Tasks.TaskCanceledException' in mscorlib.dll
            Exception thrown: 'System.Threading.Tasks.TaskCanceledException' in mscorlib.dll
            Operation canceled.
            DoAsync_Generic ended
            */
        }

        [Test]
        public void TestAsyncAwait_Cancel_ThrowIfCancellationRequested()
        {
            Debug.WriteLine("Staring async download");
            var mc = new _07_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoAsync_Generic(cts, mc.DoAsync_ThrowIfCancellationRequested);
            Debug.WriteLine("Async download started");

            Thread.Sleep(2000);
            Debug.WriteLine("Cancel Task");
            cts.Cancel();
            Thread.Sleep(5000);
            /*
            Staring async download
            Task 1 started
            Async download started
            Task 1 ended
            Task 2 started
            Task 2 ended
            Task 3 started
            Task 3 ended
            Task 4 started
            Task 4 ended
            Task 5 started
            Task 5 ended
            Task 6 started
            Cancel Task
            Task 6 ended
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Operation canceled.
            DoAsync_Generic ended
            */
        }

        [Test]
        public void TestAsyncAwait_Cancel_IsCancellationRequested()
        {
            Debug.WriteLine("Staring async download");
            var mc = new _07_ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoAsync_Generic(cts, mc.DoAsync_IsCancellationRequested);
            Debug.WriteLine("Async download started");

            Thread.Sleep(2000);
            Debug.WriteLine("Cancel Task");
            cts.Cancel();
            Thread.Sleep(5000);
            /*
            Staring async download
            Task 1 started
            Async download started
            Task 1 ended
            Task 2 started
            Task 2 ended
            Task 3 started
            Task 3 ended
            Task 4 started
            Task 4 ended
            Task 5 started
            Task 5 ended
            Task 6 started
            Cancel Task
            Task 6 ended
            DoAsync_Generic ended
            */
        }
    }

#endif
}