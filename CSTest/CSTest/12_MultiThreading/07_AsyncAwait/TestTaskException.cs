using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
    [TestFixture]
    public class TestTaskException
    {

        [Test]
        public void TestAsyncAwait11_Exception()
        {
            Debug.WriteLine("Staring async download\n");
            ClassUnderTest mc = new ClassUnderTest();
            mc.DoDownloadAsync11_InnerException();
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
        public void TestAsyncAwait12_Exception()
        {
            Debug.WriteLine("Staring async download\n");
            ClassUnderTest mc = new ClassUnderTest();

            try
            {
                mc.DoDownloadAsync12_OuterException();
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

    }
}