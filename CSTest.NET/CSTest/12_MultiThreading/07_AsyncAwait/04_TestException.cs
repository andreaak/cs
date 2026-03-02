using Azure;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _04_TestException
    {
        [Test]
        public void TestAsyncAwait11_CatchedException()
        {
            Debug.WriteLine("Staring async download");
            var mc = new _04_TestException();
            mc.DoDownloadAsync_CatchedException();
            Debug.WriteLine("After DoDownloadAsync_CatchedException");
            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            No such host is known. (www.micr1osoft.com:80)
            */
        }

        [Test]
        public async Task TestAsyncAwait11_CatchedExceptionAwait()
        {
            Debug.WriteLine("Staring async download");
            var mc = new _04_TestException();
            await mc.DoDownloadAsync_CatchedExceptionAwait();
            Debug.WriteLine("Async download ended");
            /*
            Staring async download
            Async download started
            No such host is known. (www.micr1osoft.com:80)
            Async download ended
            */
        }

        [Test]
        public async Task TestAsyncAwait11_Await()
        {
            Debug.WriteLine("Staring async download");
            var mc = new _04_TestException();

            try
            {
                await mc.DoDownloadAsync_NotCatchedException();
            }
            catch (Exception ex)
            {
                //await возвращает только одно (первое) исключение
                Debug.WriteLine(ex);
            }
            Debug.WriteLine("Async download ended");
            /*
            Staring async download
            
            Exception thrown: 'System.Net.WebException' in System.Private.CoreLib.dll
            
            System.Net.WebException: No such host is known. (www.micr1osoft.com:80)
             ---> System.Net.Http.HttpRequestException: No such host is known. (www.micr1osoft.com:80)
            ---> System.Net.Sockets.SocketException (11001): No such host is known.
            ...
            
            Async download ended
            */
        }
        
        [Test]
        public async Task TestAsyncAwait11_AwaitMultiple()
        {
            Debug.WriteLine("Staring async download");

            try
            {
                await OperationsAsync();
            }
            catch (Exception ex)
            {
                //await возвращает только одно (первое) исключение
                Debug.WriteLine("Log: " + ex);
            }
            Debug.WriteLine("Async download ended");
            /*
            Staring async download

            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #1 выбросила исключение в методе OperationsAsync

            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #3 выбросила исключение в методе OperationsAsync

            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #2 выбросила исключение в методе OperationsAsync

            Log: System.Exception: 3aдача #1 выбросила исключение в методе OperationsAsync
               at CSTest._12_MultiThreading._07_AsyncAwait._04_TestException.<>c.<OperationsAsync>b__4_0(Int32 taskNumber) in D:\Projects\My\cs\CSTest.NET\CSTest\12_MultiThreading\07_AsyncAwait\04_TestException.cs:line 113
            ...
            
            Async download ended
            */
        }

        [Test]
        public async Task TestAsyncAwait11_AwaitHandleMultiple()
        {
            Debug.WriteLine("Staring async download");

            var tasks = OperationsAsync();
            try
            {
                await tasks;
            }
            catch
            {
                var exceptions = tasks.Exception;
                var innerTasks = exceptions.InnerExceptions;
                foreach (var exception in innerTasks)
                {
                    Debug.WriteLine("Log: " + exception.Message);
                }
            }
            Debug.WriteLine("Async download ended");
            /*
            Staring async download
            
            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #1 выбросила исключение в методе OperationsAsync
            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #2 выбросила исключение в методе OperationsAsync
            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #3 выбросила исключение в методе OperationsAsync

            Log: 3aдача #1 выбросила исключение в методе OperationsAsync
            Log: 3aдача #2 выбросила исключение в методе OperationsAsync
            Log: 3aдача #3 выбросила исключение в методе OperationsAsync
            Async download ended
            */
        }

        [Test]
        public async Task TestAsyncAwait11_AwaitHandleMultipleWithContinue()
        {
            Debug.WriteLine("Staring async download");

            try
            {
                var tasks = OperationsAsync();
                await tasks.ContinueWith(t => { }, TaskContinuationOptions.ExecuteSynchronously);
                tasks.Wait();

            }
            catch (AggregateException ex)
            {
                foreach (var exception in ex.InnerExceptions)
                {
                    Debug.WriteLine("Log: " + exception.Message);
                }
            }
            Debug.WriteLine("Async download ended");
            /*
            Staring async download

            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #1 выбросила исключение в методе OperationsAsync

            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #3 выбросила исключение в методе OperationsAsync

            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #2 выбросила исключение в методе OperationsAsync

            Exception thrown: 'System.AggregateException' in System.Private.CoreLib.dll
            Log: 3aдача #1 выбросила исключение в методе OperationsAsync
            Log: 3aдача #2 выбросила исключение в методе OperationsAsync
            Log: 3aдача #3 выбросила исключение в методе OperationsAsync
            Async download ended
            */
        }

        [Test]
        public async Task TestAsyncAwait11_AwaitHandleMultipleWithContinue2()
        {
            Debug.WriteLine("Staring async download");

            await OperationsAsync().ContinueWith(t => 
            {
                try
                {
                    t.Wait();
                }
                catch (AggregateException ex)
                {
                    foreach (var exception in ex.InnerExceptions)
                    {
                        Debug.WriteLine("Log: " + exception.Message);
                    }
                }
            }, TaskContinuationOptions.ExecuteSynchronously);

            
            Debug.WriteLine("Async download ended");
            /*
            Staring async download

            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #1 выбросила исключение в методе OperationsAsync

            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #3 выбросила исключение в методе OperationsAsync

            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            3aдача #2 выбросила исключение в методе OperationsAsync

            Exception thrown: 'System.AggregateException' in System.Private.CoreLib.dll
            Log: 3aдача #1 выбросила исключение в методе OperationsAsync
            Log: 3aдача #2 выбросила исключение в методе OperationsAsync
            Log: 3aдача #3 выбросила исключение в методе OperationsAsync
            Async download ended
            */
        }

        private static Task OperationsAsync()

        {
            Action<int> operation = (taskNumber) =>
            {
                Thread.Sleep(taskNumber * 300);
                throw new Exception($"3aдача #{taskNumber} выбросила исключение в методе OperationsAsync");
            };
            Task t1 = Task.Run(() => operation.Invoke(1));
            Task t2 = Task.Run(() => operation.Invoke(2));
            Task t3 = Task.Run(() => operation.Invoke(3));
            return Task.WhenAll(t1, t2, t3);
        }

        [Test]
        public void TestAsyncAwait11_ExceptionAfterAwait()
        {
            Debug.WriteLine("Staring async download\n");
            var mc = new _04_TestException();
            mc.DoDownloadAsync_ExceptionAfterAwait();
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            <html><head>

            Exception thrown: 'System.Exception' in CSTest.dll
            Test
            */
        }

        [Test]
        public void TestAsyncAwait12_NotCatchedException()
        {
            Debug.WriteLine("Staring async download\n");
            var mc = new _04_TestException();
            mc.DoDownloadAsync_NotCatchedException();
            //try
            //{
            //    mc.DoDownloadAsync_NotCatchedException();
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("Exception : " + e.Message);
            //}
            //Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*Staring async download

            Async download started

            Exception thrown: 'System.Net.WebException' in mscorlib.dll

            */
        }

        //Исключения «выбрасываются» в месте вызова асинхронной операции, а не Callback-метода!
        public async void DoDownloadAsync_CatchedException()
        {
            using (var w = new WebClient())
            {
                try
                {
                    Debug.WriteLine("Async download started\n");
                    string txt = await w.DownloadStringTaskAsync("http://www.micr1osoft.com/");
                    Debug.WriteLine(txt);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        public async Task DoDownloadAsync_CatchedExceptionAwait()
        {
            using (var w = new WebClient())
            {
                try
                {
                    Debug.WriteLine("Async download started\n");
                    string txt = await w.DownloadStringTaskAsync("http://www.micr1osoft.com/");
                    Debug.WriteLine(txt);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        public async void DoDownloadAsync_ExceptionAfterAwait()
        {
            using (var w = new WebClient())
            {
                try
                {
                    string txt = await w.DownloadStringTaskAsync("http://www.microsoft.com/");
                    Debug.WriteLine(txt);
                    throw new Exception("Test");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        public async Task DoDownloadAsync_NotCatchedException()
        {
            using (var w = new WebClient())
            {
                string txt = await w.DownloadStringTaskAsync("http://www.micr1osoft.com/");
                Debug.WriteLine(txt);
            }
        }
    }

#endif
}