using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CSTest._12_MultiThreading._05_TPL._01_Task;
using CSTest._12_MultiThreading._08_WinFormsWPF._0_Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._08_WinFormsWPF
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestWinFormsWPF1()
        {
            //dataTextBox.Text += "Beginning download\n";
            Debug.WriteLine("Beginning download");
            var sync = UIContext.SynchronizationContext;
            var req = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            req.Method = "GET";
            req.BeginGetResponse(
                asyncResult =>
                {
                    var resp = (HttpWebResponse)req.EndGetResponse(asyncResult);
                    string headersText = resp.Headers.ToString();
                    sync.Post(//Для избежания исключения при обновлении UI из другого потока
                        state => /*dataTextBox.Text += headersText*/ Debug.WriteLine(headersText),
                        null);
                },
                null);
            /*dataTextBox.Text += "Download started async\n";*/
            Debug.WriteLine("Download started async");
            Thread.Sleep(5000);
            /*
            Beginning download
            Download started async
            Age: 157
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
            */
        }

        [TestMethod]
        // Продемонстрировать продолжение задачи при нормальной работе.
        public void TestWinFormsWPFTaskContinue()
        {
            Debug.WriteLine("Основной поток запущен.");
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
            UIContext.Initialize();
            CancellationTokenSource cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew((object tc) =>
            {
                Debug.WriteLine("Рабочий поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Setup.LongMethod();
                ((CancellationToken)tc).ThrowIfCancellationRequested();
            }, cts.Token, cts.Token);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("Обработка при отмене задачи");
                Setup.DisposeCancellationTokenSource(cts);
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnCanceled,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("Обработка при исключении в задаче");
                Setup.DisposeCancellationTokenSource(cts);
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("Обработка при нормальном завершении задачи");
                Setup.DisposeCancellationTokenSource(cts);
            }, cts.Token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                UIContext.Current);
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
        // Продемонстрировать продолжение задачи при отмене.
        public void TestWinFormsWPFTaskContinueCancel()
        {
            Debug.WriteLine("Основной поток запущен.");
            UIContext.Initialize();
            CancellationTokenSource cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew((object tc) =>
            {
                Setup.LongMethod();
                ((CancellationToken)tc).ThrowIfCancellationRequested();
            }, cts.Token, cts.Token);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при отмене задачи");
                Setup.DisposeCancellationTokenSource(cts);
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnCanceled,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при исключении в задаче");
                Setup.DisposeCancellationTokenSource(cts);
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Обработка при нормальном завершении задачи");
                Setup.DisposeCancellationTokenSource(cts);
            }, cts.Token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                UIContext.Current);
            Thread.Sleep(2000);
            Setup.CancelTask(cts);
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
        // Продемонстрировать продолжение задачи при исключении.
        public void TestWinFormsWPFTaskContinueException()
        {
            Debug.WriteLine("Основной поток запущен.");
            Debug.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
            UIContext.Initialize();
            CancellationTokenSource cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew((object tc) =>
            {
                Debug.WriteLine("Рабочий поток: Id {0} ", Thread.CurrentThread.ManagedThreadId);
                Setup.ExceptionMethod();
                ((CancellationToken)tc).ThrowIfCancellationRequested();
            }, cts.Token, cts.Token);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("Обработка при отмене задачи");
                Setup.DisposeCancellationTokenSource(cts);
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnCanceled,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("Обработка при исключении в задаче");
                Setup.DisposeCancellationTokenSource(cts);
            }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                UIContext.Current);
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Поток продолжения: Id {0}", Thread.CurrentThread.ManagedThreadId);
                Debug.WriteLine("Обработка при нормальном завершении задачи");
                Setup.DisposeCancellationTokenSource(cts);
            }, cts.Token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                UIContext.Current);
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток запущен.
            Первичный поток: Id 8 
            Основной поток завершен.
            Рабочий поток: Id 9 
            Начало работы
            Окончание работы
            Поток продолжения: Id 8

            Обработка при нормальном завершении задачи
            */
        }



        [TestMethod]
        public void TestWinFormsWPFAsyncAwait1()
        {
            Debug.WriteLine("Staring async download\n");
            DoDownloadAsync("http://www.microsoft.com");
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            Age: 2399
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
        public void TestWinFormsWPFAsyncAwait2Exception()
        {
            NewMethod();
        }

        private async void NewMethod()
        {
            Debug.WriteLine("Staring async download\n");
            try
            {
                await DoDownloadAsync("http://www.microsoft55.com");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            Debug.WriteLine("Async download started\n");

            Thread.Sleep(5000);
            /*
            Staring async download

            Async download started

            Exception thrown: 'System.Net.WebException' in mscorlib.dll
            */
        }

        async Task DoDownloadAsync(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            var resp = (HttpWebResponse)await req.GetResponseAsync();
            // или
            // Task<WebResponse> getResponseTask = Task.Factory.FromAsync<WebResponse>(
            // req.BeginGetResponse, req.EndGetResponse, null);
            // var resp = (HttpWebResponse) await getResponseTask;

            //dataTextBox.Text += resp.Headers.ToString();
            //dataTextBox.Text += "Async download completed";
            Debug.WriteLine(resp.Headers.ToString());
            Debug.WriteLine("Async download completed");
        }
    }
}
