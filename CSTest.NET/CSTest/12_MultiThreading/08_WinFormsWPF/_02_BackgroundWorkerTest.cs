using System.Threading;
using CSTest._12_MultiThreading._08_WinFormsWPF._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._08_WinFormsWPF
{
    [TestFixture]
    public class _02_BackgroundWorkerTest
    {
        [Test]
        public void TestAsyncDelegates10BackgroundWorker()
        {
            _02_BackgroundWorker bg = new _02_BackgroundWorker();
            bg.StartWorker();
            Thread.Sleep(2000);
            /*
            StartWorker метод. Thread Id 8
            OnDoWork метод. Thread Id 9
            OnProgressChanged метод. Thread Id: 10 Value: 0
            OnProgressChanged метод. Thread Id: 11 Value: 1
            OnProgressChanged метод. Thread Id: 10 Value: 2
            OnProgressChanged метод. Thread Id: 11 Value: 3
            OnProgressChanged метод. Thread Id: 10 Value: 4
            OnProgressChanged метод. Thread Id: 11 Value: 5
            OnProgressChanged метод. Thread Id: 10 Value: 6
            OnProgressChanged метод. Thread Id: 11 Value: 7
            OnProgressChanged метод. Thread Id: 10 Value: 8
            OnProgressChanged метод. Thread Id: 11 Value: 9
            OnWorkCompleted метод. Thread Id 10
            Result = 3
            */
        }

        [Test]
        public void TestAsyncDelegates11BackgroundWorker()
        {
            _02_BackgroundWorker bg = new _02_BackgroundWorker();
            bg.StartWorker();
            Thread.Sleep(500);
            bg.CancelWorker();
            Thread.Sleep(1000);
            /*
            StartWorker метод. Thread Id 8
            OnDoWork метод. Thread Id 9
            OnProgressChanged метод. Thread Id: 10 Value: 0
            OnProgressChanged метод. Thread Id: 11 Value: 1
            OnProgressChanged метод. Thread Id: 10 Value: 2
            OnProgressChanged метод. Thread Id: 11 Value: 3
            CancelWorker метод. Thread Id 8
            OnWorkCompleted метод. Thread Id 10
            Cancelled
            */
        }
    }
}
