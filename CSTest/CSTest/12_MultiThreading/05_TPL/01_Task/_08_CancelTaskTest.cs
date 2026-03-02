using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

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
        public void TestTaskCancel2()
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
        public void TestTaskCancel4Continue()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> task = Task.Run(() =>
            {
                Thread.Sleep(5000);
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

            });

            cts.Cancel();

            Thread.Sleep(9000); // Даем завершить свое выполнение всем задачами.

            Debug.WriteLine($"Cтатус задачи #1 - {task.Status}");
            Debug.WriteLine($"Cтатус задачи #2 - {c1.Status}");

            /*
            3aдaчa #1 выполнилась.
            3aдaчa #2 (Продолжение) выполнилась.
            Результат = 10
            Cтатус задачи #1 - RanToCompletion
            Cтатус задачи #2 - RanToCompletion
            */
        }
    }
}
