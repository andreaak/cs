﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestAsyncAwait1_1()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClass mc = new TestClass();
            mc.OperationAsync();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation ThreadID 8
            Begin
            End
            */
        }

        [TestMethod]
        public void TestAsyncAwait1_2()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClassReflected mc = new TestClassReflected();
            mc.OperationAsync();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation ThreadID 8
            Begin
            End 
            */
        }

        [TestMethod]
        public void TestAsyncAwait2_1()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClass mc = new TestClass();
            mc.OperationAsync2();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation ThreadID 8
            Begin
            End
            */
        }

        [TestMethod]
        public void TestAsyncAwait2_2()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClassReflected mc = new TestClassReflected();
            mc.OperationAsync2();
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation ThreadID 8
            Begin
            End 
            */
        }

        [TestMethod]
        public void TestAsyncAwait3_1()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClass mc = new TestClass();
            mc.OperationAsync3();
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

        [TestMethod]
        public void TestAsyncAwait3_2()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClassReflected mc = new TestClassReflected();
            mc.OperationAsync3();
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

        [TestMethod]
        public void TestAsyncAwait4_1ReturnValue()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClass mc = new TestClass();
            mc.OperationAsync4();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Main thread ended. ThreadID 10
            Operation4 ThreadID 9

            Результат: 4
            */
        }

        [TestMethod]
        public void TestAsyncAwait5_1ReturnValue()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClass mc = new TestClass();
            mc.OperationAsync5();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation4 ThreadID 9
            Main thread ended. ThreadID 10

            Результат: 4
            */
        }

        [TestMethod]
        public void TestAsyncAwait5_2ReturnValue()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClassReflected mc = new TestClassReflected();
            mc.OperationAsync5();
            Debug.WriteLine("Main thread ended. ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            /*
            Main ThreadID 10
            Operation4 ThreadID 8
            Main thread ended. ThreadID 10

            Результат: 4
            */
        }

        [TestMethod]
        public void TestAsyncAwait6_1Continue()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClass mc = new TestClass();
            Task task = mc.OperationAsync6();
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

        [TestMethod]
        public void TestAsyncAwait6_2Continue()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClassReflected mc = new TestClassReflected();
            Task task = mc.OperationAsync6();
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

        [TestMethod]
        public void TestAsyncAwait7_1ContinueWithReturn()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClass mc = new TestClass();
            Task<int> task = mc.OperationAsync7();
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

        [TestMethod]
        public void TestAsyncAwait7_2ContinueWithReturn()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClassReflected mc = new TestClassReflected();
            Task<int> task = mc.OperationAsync7();
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

        [TestMethod]
        public void TestAsyncAwait8_1ContinueWithArgument()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClass mc = new TestClass();
            double argument = 8.0;
            Task<double> task = mc.OperationAsync8(argument);
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

        [TestMethod]
        public void TestAsyncAwait8_2ContinueWithArgument()
        {
            Debug.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            TestClassReflected mc = new TestClassReflected();
            double argument = 8.0;
            Task<double> task = mc.OperationAsync8(argument);
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

        [TestMethod]
        public void TestAsyncAwait9()
        {
            Debug.WriteLine("Staring async download\n");
            TestClass mc = new TestClass();
            mc.DoDownloadAsync9();
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

        [TestMethod]
        public void TestAsyncAwait10OldModel()
        {
            Debug.WriteLine("Staring async download\n");
            TestClass mc = new TestClass();
            mc.DoDownloadAsync10OldModel();
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

        [TestMethod]
        public void TestAsyncAwait11Exception()
        {
            Debug.WriteLine("Staring async download\n");
            TestClass mc = new TestClass();
            mc.DoDownloadAsync11InnerException();
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            A first chance exception of type 'System.Net.WebException' occurred in mscorlib.dll
            The remote server returned an error: (503) Server Unavailable.
            */
        }

        [TestMethod]
        public void TestAsyncAwait12Exception()
        {
            Debug.WriteLine("Staring async download\n");
            TestClass mc = new TestClass();

            try
            {
                mc.DoDownloadAsync12OuterException();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception : " + e.Message);
            }
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*

            */
        }
        
        [TestMethod]
        public void TestAsyncAwait13Cancel()
        {
            Debug.WriteLine("Staring async download\n");
            TestClass mc = new TestClass();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoDownloadAsync13Cancel(cts);
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

        [TestMethod]
        public void TestAsyncAwait14WaitAll()
        {
            Debug.WriteLine("Staring async download\n");
            TestClass mc = new TestClass();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoDownloadAsync14WaitAll(cts);
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
    }
}