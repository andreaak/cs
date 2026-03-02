using NUnit.Framework;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace CSTest._12_MultiThreading._05_TPL
{
    /*
    Для  превращения  API  старых  асинхронных шаблонов  программирования  в  новый  TAP 
    используется  класс  TaskCompletionSource.  Если  необходимо  преобразовывать  API 
    шаблона  APM  в  TAP,  то  можно  воспользоваться  методами  FromAsync  фабрики  задач 
    (TaskFactory). 
    TaskCompletionSource<TResult> - создает асинхронные операций в виде задач.
    Он порождает задачу-марионетку, состояние и завершение которой
    контролируется с помощью методов класса TaskCompletionSource. При
    этом, для потребителей взаимодействие будет выглядеть как работа с
    обычной задачей, они не почувствуют разницы.
    Этот класс можно использовать для создания своего типа асинхронных операций.
     */
    [TestFixture]
    public class _02_TaskCompletionSource
    {
        [Test]
        public void TestTaskCompletionSource1()
        {
            var tcs = new TaskCompletionSource<int>();

            new Thread(() =>
            {
                Debug.WriteLine("Start Task");
                Thread.Sleep(5000);
                Debug.WriteLine("Set Result");
                tcs.SetResult(42);
                Debug.WriteLine("End Task");
            })
            { IsBackground = true }
            .Start();

            Task<int> task = tcs.Task; // "Подчиненная” задача
            Debug.WriteLine("Print Result");
            Debug.WriteLine(task.Result); // 42
            /*
            Print Result
            Start Task
            Set Result
            End Task
            The thread 0x10490 has exited with code 0 (0x0).
            42 
             */
        }

        [Test]
        public void TestTaskCompletionSource2()
        {
            Task<int> task = Run(() =>
            {
                Debug.WriteLine("Start Task");
                Thread.Sleep(5000);
                Debug.WriteLine("End Task");
                return 42; 
            });

            Debug.WriteLine(task.Result);
            /*
            Start Task
            End Task
            Set Result
            The thread 0x48b8 has exited with code 0 (0x0).
            42
             */
        }

        Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();
            new Thread(() =>
            {
                try
                {
                    TResult res = function();
                    Debug.WriteLine("Set Result");
                    tcs.SetResult(res);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }).Start();
            return tcs.Task;
        }

#if CS5
        [Test]
        public void TestTaskCompletionSource3()
        {
            var awaiter = GetAnswerToLife().GetAwaiter();

            awaiter.OnCompleted(() => Debug.WriteLine(awaiter.GetResult()));

            //Для задержки основного потока

            while (!awaiter.IsCompleted)
            {
                Thread.Sleep(1000);
            }
            /*
            Create timer
            Start timer
            Dispose timer
            Set result
            42
             */
        }
#endif
        Task<int> GetAnswerToLife()
        {
            var tcs = new TaskCompletionSource<int>();

            Debug.WriteLine("Create timer");
            // Создать таймер, который инициирует событие раз в 5000 миллисекунд:
            var timer = new System.Timers.Timer(5000) { AutoReset = false };

            timer.Elapsed += delegate
            {
                Debug.WriteLine("Dispose timer");
                timer.Dispose();
                Debug.WriteLine("Set result");
                tcs.SetResult(42);
            };
            Debug.WriteLine("Start timer");
            timer.Start();
            return tcs.Task;
        }

        [Test]
        public async Task TestTaskCompletionSource4()
        {
            try
            {
                string result = await APM_TAP1();
                Debug.WriteLine($"File content: {result}");
                string result2 = await APM_TAP2();
                Debug.WriteLine($"File content: {result}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetType());
                Debug.WriteLine(ex.Message);
            }
        }

        private static Task<string> APM_TAP1()
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            using (FileStream fs = new FileStream("test.txt",
                FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite,
                4096, true))
            {
                byte[] array = new byte[250];
                fs.BeginRead(array, 0, array.Length, (iAsyncResult) =>
                {
                    try
                    {
                        int bytes = fs.EndRead(iAsyncResult);
                        Debug.WriteLine($"Bytes read - {bytes}");
                        tcs.TrySetResult(Encoding.UTF8.GetString(array));
                    }
                    catch (OperationCanceledException ex)
                    {
                        tcs.TrySetCanceled(ex.CancellationToken);
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }

                }, null);

            }

            return tcs.Task;
        }

        private static Task<string> APM_TAP2()
        {
            TaskFactory taskFactory = new TaskFactory();

            using (FileStream fs = new FileStream("test.txt",
                        FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite,
                        4096, true))
            {
                byte[] array = new byte[250];

                return taskFactory.FromAsync(fs.BeginRead, (iAsyncResult) =>
                {
                    int bytes = fs.EndRead(iAsyncResult);
                    Console.WriteLine($"Bytes read - {bytes}");

                    return Encoding.UTF8.GetString(array);

                }, array, 0, array.Length, null);
            }
        }

        [Test]
        public async Task TestTaskCompletionSource5()
        {
            try
            {
                string result = await EAP_TAP();
                Debug.WriteLine($"File content: {result}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetType());
                Debug.WriteLine(ex.Message);
            }
        }


        private static Task<string> EAP_TAP()
        {
            WebClient webClient = new WebClient();

            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            webClient.DownloadStringCompleted += (sender, eventData) =>
            {
                try
                {
                    Console.WriteLine($"Sender - {sender.ToString()}");
                    tcs.TrySetResult(eventData.Result);
                }
                catch (OperationCanceledException ex)
                {
                    if (eventData.Cancelled == true)
                    {
                        Console.WriteLine($"ОТМЕНА");
                    }

                    tcs.TrySetCanceled(ex.CancellationToken);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        tcs.TrySetException(ex.InnerException);
                    }
                    else
                    {
                        tcs.TrySetException(ex);
                    }
                }
            };

            webClient.DownloadStringAsync(new Uri("http://microsoft.com/"));

            return tcs.Task;
        }




        Task Delay(int milliseconds)
        {
            var tcs = new TaskCompletionSource<object>();
            var timer = new System.Timers.Timer(milliseconds) { AutoReset = false };
            timer.Elapsed += delegate
            {
                timer.Dispose();
                tcs.SetResult(null);
            };
            timer.Start();
            return tcs.Task;
        }

    }
}
