using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _03_TestTaskWithResult
    {
        [Test]
        public void TestAsyncAwait8_ContinueTaskWithResult()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _00_ClassUnderTest mc = new _00_ClassUnderTest();
            Task<int> task = mc.OperationAsync8_ReturnTaskWithResult();
            task.ContinueWith(t => Debug.WriteLine("\n����������� ������ ��������� : {0}", t.Result));
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation4 ThreadID 9
            Main thread ended. ThreadID 10

            ����������� ������ ��������� : 4
            */
        }

        [Test]
        public void TestAsyncAwait9_ContinueTaskWithArgument()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _00_ClassUnderTest mc = new _00_ClassUnderTest();
            double argument = 8.0;
            Task<double> task = mc.OperationAsync9_ReturnTaskWithResult_Argument(argument);
            task.ContinueWith(t => Debug.WriteLine("\n����������� ������ ��������� : {0}", t.Result));
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation8 ThreadID 9
            Main thread ended. ThreadID 10

            ����������� ������ ��������� : 64
            */
        }

        [Test]
        public async void TestAsyncAwait15_WithNullResult()
        {
            var test = new _03_TestTaskWithResult();
            string res = await test.TestReturnNullResult();
            Debug.WriteLine(res);//res == null
        }

        private string Bonus { get; set; }

        public async Task<string> TestReturnNullResult()
        {
            if (Bonus == null)
                return null;
            await Task.Run(() => { });
            return "AsyncTest";
        }
    }

#endif
}