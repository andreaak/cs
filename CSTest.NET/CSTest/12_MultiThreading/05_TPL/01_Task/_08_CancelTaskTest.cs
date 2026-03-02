using Elastic.Clients.Elasticsearch;
using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestFixture]
    public class _08_CancelTaskTest
    {
        // Метод, исполняемый как задача, 
        static void TestTaskThrow(object ct)
        {
            CancellationToken cancelTok = (CancellationToken)ct;
            // Проверить, отменена ли задача, прежде чем запускать ее. 
            cancelTok.ThrowIfCancellationRequested();
            Debug.WriteLine("TestTask() запущен");
            for (int count = 0; count < 10; count++)
            {
                cancelTok.ThrowIfCancellationRequested();
                Thread.Sleep(500);
                Debug.WriteLine("В методе TestTask() подсчет равен " + count);
            }
            Debug.WriteLine("TestTask завершен");
        }

        [Test]
        // Простой пример отмены задачи с использованием опроса.
        public void TestTaskCancelAndThrow()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Создать объект источника признаков отмены. 
            CancellationTokenSource cancelTokSrc = new CancellationTokenSource();
            // Запустить задачу, передав признак отмены ей самой и делегату. 
            Task task = Task.Factory.StartNew(TestTaskThrow, cancelTokSrc.Token,
            cancelTokSrc.Token);
            // Дать задаче возможность исполняться вплоть до ее отмены. 
            Thread.Sleep(2000);
            try
            {
                // Отменить задачу. 
                Debug.WriteLine("Отмена задачи");
                cancelTokSrc.Cancel();
                // Приостановить выполнение метода Test() до тех пор, 
                // пока не завершится задача tsk. 
                Debug.WriteLine("Ожидание выполнения задачи");
                task.Wait();
            }
            catch (AggregateException /*exc*/)
            {
                if (task.IsCanceled)
                {
                    Debug.WriteLine("ExceptionLog: Задача отменена");
                }
                //Для просмотра исключения снять комментарии со следующей строки кода: 
                // Debug.WriteLine(exc); 
            }
            finally
            {
                task.Dispose();
                cancelTokSrc.Dispose();
            }
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            TestTask() запущен
            В методе TestTask() подсчет равен 0
            В методе TestTask() подсчет равен 1
            В методе TestTask() подсчет равен 2
            Отмена задачи
            Ожидание выполнения задачи
            В методе TestTask() подсчет равен 3
            An exception of type 'System.OperationCanceledException' occurred in System.Private.CoreLib.dll but was not handled in user code
            The operation was canceled.

            Exception thrown: 'System.AggregateException' in System.Private.CoreLib.dll
            ExceptionLog: Задача отменена
            Основной поток завершен.
            */
        }

        static void TestTask(object ct)
        {
            CancellationToken cancelTok = (CancellationToken)ct;
            // Проверить, отменена ли задача, прежде чем запускать ее. 
            cancelTok.ThrowIfCancellationRequested();
            Debug.WriteLine("TestTask() запущен");
            for (int count = 0; count < 10; count++)
            {
                // В данном примере для отслеживания отмены задачи применяется опрос, 
                if (cancelTok.IsCancellationRequested)
                {
                    Debug.WriteLine("Получен запрос на отмену задачи.");
                    return;
                }
                Thread.Sleep(500);
                Debug.WriteLine("В методе TestTask() подсчет равен " + count);
            }
            Debug.WriteLine("TestTask завершен");
        }

        [Test]
        // Простой пример отмены задачи с использованием опроса.
        public void TestTaskCancelWithFlag()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Создать объект источника признаков отмены. 
            CancellationTokenSource cancelTokSrc = new CancellationTokenSource();
            // Запустить задачу, передав признак отмены ей самой и делегату. 
            Task task = Task.Factory.StartNew(TestTask, cancelTokSrc.Token,
            cancelTokSrc.Token);
            // Дать задаче возможность исполняться вплоть до ее отмены. 
            Thread.Sleep(2000);
            try
            {
                // Отменить задачу. 
                Debug.WriteLine("Отмена задачи");
                cancelTokSrc.Cancel();
                // Приостановить выполнение метода Test() до тех пор, 
                // пока не завершится задача tsk. 
                Debug.WriteLine("Ожидание выполнения задачи");
                task.Wait();
            }
            catch (AggregateException /*exc*/)
            {
                if (task.IsCanceled)
                {
                    Debug.WriteLine("ExceptionLog: Задача отменена");
                }
                //Для просмотра исключения снять комментарии со следующей строки кода: 
                // Debug.WriteLine(exc); 
            }
            finally
            {
                task.Dispose();
                cancelTokSrc.Dispose();
            }
            Debug.WriteLine("Основной поток завершен.");

            /*
            В методе TestTask() подсчет равен 0
            В методе TestTask() подсчет равен 1
            В методе TestTask() подсчет равен 2
            Отмена задачи
            Ожидание выполнения задачи
            В методе TestTask() подсчет равен 3
            Получен запрос на отмену задачи.
            Основной поток завершен.
            */
        }

        [Test]
        public void TestTaskCancel3Linked()
        {
            CancellationTokenSource parentCts1 = new CancellationTokenSource();
            CancellationTokenSource parentCts2 = new CancellationTokenSource();
            CancellationTokenSource parentCts3 = new CancellationTokenSource();

            CancellationTokenSource linkedCts4 = CancellationTokenSource.CreateLinkedTokenSource(parentCts1.Token, parentCts2.Token);

            CancellationTokenSource linkedCts5 = CancellationTokenSource.CreateLinkedTokenSource(linkedCts4.Token, parentCts3.Token);

            CancellationToken parentToken1 = parentCts1.Token;
            CancellationToken parentToken2 = parentCts2.Token;
            CancellationToken parentToken3 = parentCts3.Token;
            CancellationToken linkedToken1 = linkedCts4.Token;
            CancellationToken linkedToken2 = linkedCts5.Token;

            var t1 = Task.Run(() => Do("1", parentToken1), parentToken1);
            var t2 = Task.Run(() => Do("2", parentToken2), parentToken2);
            var t3 = Task.Run(() => Do("3", parentToken3), parentToken3);
            var t4 = Task.Run(() => Do("4", linkedToken1), linkedToken1);
            var t5 = Task.Run(() => Do("5", linkedToken2), linkedToken2);

            parentToken1.Register(() => Canceled(1)); 
            parentToken2.Register(() => Canceled(2)); 
            parentToken3.Register(() => Canceled(3)); 
            linkedToken1.Register(() => Canceled(4)); 
            linkedToken2.Register(() => Canceled(5));


            parentCts1.CancelAfter(1500);
            //parentCts2.CancelAfter(1500);
            //parentCts3.CancelAfter(1500);
            //linkedCts4.CancelAfter(1500);
            //linkedCts5.CancelAfter(1500);

            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");
            /*
            3aдaчa #1 в потоке 7 начала свою работу.
            3aдaчa #2 в потоке 10 начала свою работу.
            3aдaчa #3 в потоке 17 начала свою работу.
            3aдaчa #4 в потоке 18 начала свою работу.
            3aдaчa #5 в потоке 19 начала свою работу.

            —Задача #1 в потоке 21 была отменена.—
            —Задача #4 в потоке 21 была отменена.—
            —Задача #5 в потоке 21 была отменена.—

	            3aдaчa #2 в потоке 10 насчитала - 11175.
	            3aдaчa #3 в потоке 17 насчитала - 11175.
            Основной поток завершен.

            */
        }

        [Test]
        public void TestTaskCancel4Continue()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> task = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Debug.WriteLine($"3aдaчa #1 выполнилась."); 
                return 10;

            }, cts.Token);

            Task c1 = task.ContinueWith((t) =>
            {
                //Несмотря на исключение продолжение будет запущено
                Debug.WriteLine($"3aдaчa #2 (Продолжение) выполнилась.");

                if (t.Status != TaskStatus.Canceled && t.Status != TaskStatus.Faulted)
                {
                    Debug.WriteLine($"Результат = {t.Result}");
                }

            }, cts.Token);

            cts.Cancel();

            Thread.Sleep(3000); // Даем завершить свое выполнение всем задачами.

            Debug.WriteLine($"Cтатус задачи #1 - {task.Status}");
            Debug.WriteLine($"Cтатус задачи #2 - {c1.Status}");

            /*
            3aдaчa #1 выполнилась.
            Cтатус задачи #1 - RanToCompletion
            Cтатус задачи #2 - Canceled
            */
        }

        private static void Do(string taskId, CancellationToken token)
        {
            Debug.WriteLine($"3aдaчa #{taskId} в потоке {Thread.CurrentThread.ManagedThreadId} начала свою работу.");
            Thread.Sleep(1000);
            int sum = 0;
            for (int i = 0; i < 150; i++)
            {
                token.ThrowIfCancellationRequested();
                Thread.Sleep(1);
                sum += i;
            }
            Debug.WriteLine($"\t3aдaчa #{taskId} в потоке {Thread.CurrentThread.ManagedThreadId} насчитала - {sum}.");
        }

        private static void Canceled(int taskId)
        {
            Debug.WriteLine($"—Задача #{taskId} в потоке {Thread.CurrentThread.ManagedThreadId} была отменена.—");
        }
    }
}
