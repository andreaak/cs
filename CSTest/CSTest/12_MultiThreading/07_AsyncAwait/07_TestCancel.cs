using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CSTest._12_MultiThreading._07_AsyncAwait._0_Setup;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._07_AsyncAwait
{
#if CS5

    [TestFixture]
    public class _07_TestCancel
    {
        [Test]
        public void TestAsyncAwait13_Cancel()
        {
            Debug.WriteLine("Staring async download\n");
            var mc = new _07_TestCancel();
            CancellationTokenSource cts = new CancellationTokenSource();
            mc.DoDownloadAsync_Cancel(cts);
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

        public async void DoDownloadAsync_Cancel(CancellationTokenSource cts)
        {
            //CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                Task sumTask = SumPageSizesAsync(cts.Token);

                //Do some other stuff...
                //....

                //Wait until sumTask is Finished...
                await sumTask;

            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("\r\nDownload canceled.\r\n");
            }
            catch (Exception)
            {
                Debug.WriteLine("\r\nDownload failed.\r\n");
            }

            Debug.WriteLine("\r\n DoDownloadAsync4Cancel ended\r\n");
        }

        private async Task SumPageSizesAsync(CancellationToken ct)
        {
            HttpClient client = new HttpClient();

            IEnumerable<string> urlList = Setup.SetUpURLList();

            var total = 0;

            foreach (var url in urlList)
            {
                HttpResponseMessage response = await client.GetAsync(url, ct);
                byte[] urlContents = await response.Content.ReadAsByteArrayAsync();

                Setup.DisplayResults(url, urlContents);

                total += urlContents.Length;
            }

            Debug.WriteLine(string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total));
        }
    }

#endif
}