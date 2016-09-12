using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class TestVoid
    {
        [Test]
        public void TestAsyncAwait1_ReturnVoid_WithoutActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassUnderTest mc = new ClassUnderTest();
            mc.OperationAsync2_ReturnVoid_WithoutActionAfterAwait();
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
            ClassUnderTest mc = new ClassUnderTest();
            mc.OperationAsync3_ReturnVoid_ActionAfterAwait();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            OperationAsync (Part I) ThreadID 10
            Operation ThreadID 9
            Begin
            End
            OperationAsync (Part II) ThreadID 9
            */
        }

        [Test]
        public void TestAsyncAwait5_ActionWithResultAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            ClassUnderTest mc = new ClassUnderTest();
            mc.OperationAsync5_ReturnVoid_ActionWithResultAfterAwait();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation4 ThreadID 9
            Main thread ended. ThreadID 10

            Результат: 4
            */
        }

        [Test]
        public void TestAsyncAwait10_UseAPM()
        {
            Debug.WriteLine("Staring async download\n");
            ClassUnderTest mc = new ClassUnderTest();
            mc.DoDownloadAsync10_UseAPMItems();
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            Age: 6800
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
        public void TestAsyncAwait13_Cancel()
        {
            Debug.WriteLine("Staring async download\n");
            ClassUnderTest mc = new ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoDownloadAsync13_Cancel(cts);
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(2000);
            Debug.WriteLine("Cancel Task\n");
            cts.Cancel();
            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            Cancel Task

            A first chance exception of type 'System.Threading.Tasks.TaskCanceledException' occurred in mscorlib.dll
            A first chance exception of type 'System.Threading.Tasks.TaskCanceledException' occurred in mscorlib.dll

            Download canceled.


            DoDownloadAsync4Cancel ended
            */
        }

        [Test]
        public void TestAsyncAwait14_WaitAll()
        {
            Debug.WriteLine("Staring async download\n");
            ClassUnderTest mc = new ClassUnderTest();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoDownloadAsync14_WaitAll(cts);
            Debug.WriteLine("Async download started\n");

            //Thread.Sleep(2000);
            //Debug.WriteLine("Cancel Task\n");
            //cts.Cancel();
            Thread.Sleep(20000);

            /*
            Staring async download

            Async download started


            Length of the download:  262103
            URL:http://msdn.microsoft.com/en-us/library/hh290136.aspx, Thread:15

            Length of the download:  474478
            URL:http://msdn.microsoft.com/library/windows/apps/br211380.aspx, Thread:8

            Length of the download:  150754
            URL:http://msdn.microsoft.com/en-us/library/ee256749.aspx, Thread:15
            URL:http://msdn.microsoft.com/en-us/library/hh290140.aspx, Thread:15

            Length of the download:  150226

            Length of the download:  264298
            URL:http://msdn.microsoft.com/en-us/library/hh290138.aspx, Thread:15

            Length of the download:  48318
            URL:http://msdn.microsoft.com/en-us/library/dd470362.aspx, Thread:8
            URL:http://msdn.microsoft.com/en-us/library/aa578028.aspx, Thread:8

            Length of the download:  179341

            Length of the download:  223716
            URL:http://msdn.microsoft.com/en-us/library/ms404677.aspx, Thread:15

            Length of the download:  156120
            URL:http://msdn.microsoft.com/en-us/library/ff730837.aspx, Thread:9
            URL:http://msdn.com, Thread:9

            Length of the download:  48537

            Downloads complete.
            */
        }

        [Test]
        public async void TestAsyncAwait15_WithNullResult()
        {
            ClassUnderTest test = new ClassUnderTest();
            string res = await test.TestReturnNullResult();
            Debug.WriteLine(res);//res == null
        }

        [Test]
        public async void TestAsyncAwait15_WithResult()
        {
            ClassUnderTest test = new ClassUnderTest();
            string res = await test.TestReturnTaskFromResult();
            Debug.WriteLine(res);//res == "AsyncTest"
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public async void TestAsyncAwait16_ReturnTaskAsNull_Exception()
        {
            ClassUnderTest test = new ClassUnderTest();
            await test.TestReturnNull();
        }

        [Test]
        public void TestAsyncAwait17_ReturnTaskAsNull_Exception()
        {
            ClassUnderTest test = new ClassUnderTest();

            Assert.That(async () => await test.TestReturnNull(), Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public async void TestAsyncAwait18_ReturnTask_EmptyOperation()
        {
            ClassUnderTest test = new ClassUnderTest();

            await test.TestReturnEmptyTaskFromResult();
        }
    }

#endif
}