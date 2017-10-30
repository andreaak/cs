using System.Diagnostics;
using System.Threading;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _01_TestVoid
    {
        [Test]
        public void TestAsyncAwait1_ReturnVoid_WithoutActionAfterAwait()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            _00_ClassUnderTest mc = new _00_ClassUnderTest();
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
            _00_ClassUnderTest mc = new _00_ClassUnderTest();
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
            _00_ClassUnderTest mc = new _00_ClassUnderTest();
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
            _00_ClassUnderTest mc = new _00_ClassUnderTest();
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
    }

#endif
}