using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _02_TestTask
    {
        [Test]
        public void TestAsyncAwait6_ContinueTask()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _00_ClassUnderTest mc = new _00_ClassUnderTest();
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
        public async void TestAsyncAwait7_ActionWithResultAfterAwait()
        {
            Debug.WriteLine("Staring async download\n");
            _00_ClassUnderTest mc = new _00_ClassUnderTest();
            /*await*/
            mc.OperationAsync7_ReturnTask_ActionWithResultAfterAwait();
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            Age: 5865
            X-Cache: HIT from proxy-zp.isd.dp.ua
            Connection: keep-alive
            Accept-Ranges: bytes
            Content-Length: 1020
            Content-Type: text/html
            Date: Thu, 22 Oct 2015 12:05:11 GMT
            ETag: "6082151bd56ea922e1357f5896a90d0a:1425454794"
            Last-Modified: Wed, 04 Mar 2015 07:39:54 GMT
            Server: Apache
            Via: 1.1 proxy-zp.isd.dp.ua (squid/3.5.10)

            Async download completed
            */
        }

        [Test]
        public async void TestAsyncAwait7_WithActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _00_ClassUnderTest mc = new _00_ClassUnderTest();
            await mc.OperationAsync7_ReturnTask_WithActionAfterAwait();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);

            /*
            Main ThreadID 8
            Operation ThreadID 9
            Begin
            End
            AfterFirst 2027
            Operation ThreadID 9
            Begin
            End
            AfterSecond 4038
            Main thread ended. ThreadID 8
            */
        }

        [Test]
        public async void TestAsyncAwait16_ReturnTaskAsNull_Exception()
        {
            var test = new _02_TestTask();
            Assert.Throws<NullReferenceException>(async () => await test.TestReturnNull());
        }

        [Test]
        public void TestAsyncAwait17_ReturnTaskAsNull_Exception()
        {
            var test = new _02_TestTask();

            Assert.That(async () => await test.TestReturnNull(), Throws.TypeOf<NullReferenceException>());
        }

        public Task TestReturnNull()
        {
            return null;
        }
    }

#endif
}