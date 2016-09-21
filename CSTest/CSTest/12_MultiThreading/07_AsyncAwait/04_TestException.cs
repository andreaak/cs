using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using System.Net;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _04_TestException
    {
        [Test]
        public void TestAsyncAwait11_CatchedException()
        {
            Debug.WriteLine("Staring async download\n");
            var mc = new _04_TestException();
            mc.DoDownloadAsync_CatchedException();
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            A first chance exception of type 'System.Net.WebException' occurred in mscorlib.dll
            The remote server returned an error: (503) Server Unavailable.
            */
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

            try
            {
                mc.DoDownloadAsync_NotCatchedException();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception : " + e.Message);
            }
            Debug.WriteLine("Async download started\n");

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

        public async void DoDownloadAsync_NotCatchedException()
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