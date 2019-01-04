using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    // Id - Получает уникальный идентификатор данного экземпляра Task.
    // CurrentId - Возвращает уникальный идентификатор выполняющейся в настоящее время задачи Task.
    [TestFixture]
    public class _02_TaskIdTest
    {
        // Метод, исполняемый как задача,
        static void MyTask()
        {
            Debug.WriteLine("MyTask() №" + Task.CurrentId + " запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("В методе MyTask() #" + Task.CurrentId + ", подсчет равен " + count);
            }
            Debug.WriteLine("MyTask №" + Task.CurrentId + " завершен");
        }

        static void MyTask(CancellationToken token)
        {
            Debug.WriteLine("MyTask() №" + Task.CurrentId + " запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("Статус задачи задачи tsk: " + tsk.Status); ;
                token.ThrowIfCancellationRequested();
            }
            Debug.WriteLine("MyTask №" + Task.CurrentId + " завершен");
        }

        static void MyTaskFault(CancellationToken token)
        {
            Debug.WriteLine("MyTask() №" + Task.CurrentId + " запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("Статус задачи задачи tsk: " + tsk.Status); ;
                if (count > 4)
                {
                    throw new Exception("Test");
                }
            }
            Debug.WriteLine("MyTask №" + Task.CurrentId + " завершен");
        }

        [Test]
        // Возвратить значение из задачи.
        public void TestTaskId()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объекты двух задач.
            Task tsk = new Task(_02_TaskIdTest.MyTask);
            Debug.WriteLine("Статус задачи задачи tsk: " + tsk.Status);
            Task tsk2 = new Task(_02_TaskIdTest.MyTask);// Запустить задачи на исполнение,
            tsk.Start();
            Debug.WriteLine("Статус задачи задачи tsk: " + tsk.Status);
            tsk2.Start();
            Debug.WriteLine("Идентификатор задачи tsk: " + tsk.Id);
            Debug.WriteLine("Идентификатор задачи tsk2: " + tsk2.Id);

            // Сохранить метод Main() активным до завершения остальных задач,
            Thread.Sleep(6000);
            Debug.WriteLine("Статус задачи задачи tsk: " + tsk.Status);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Идентификатор задачи tsk: 1
            Идентификатор задачи tsk2: 2
            MyTask() №2 запущен
            MyTask() №1 запущен
            В методе MyTask() #1, подсчет равен 0
            В методе MyTask() #2, подсчет равен 0
            В методе MyTask() #2, подсчет равен 1
            В методе MyTask() #1, подсчет равен 1
            В методе MyTask() #2, подсчет равен 2
            В методе MyTask() #1, подсчет равен 2
            В методе MyTask() #2, подсчет равен 3
            В методе MyTask() #1, подсчет равен 3
            В методе MyTask() #2, подсчет равен 4
            В методе MyTask() #1, подсчет равен 4
            В методе MyTask() #2, подсчет равен 5
            В методе MyTask() #1, подсчет равен 5
            В методе MyTask() #2, подсчет равен 6
            В методе MyTask() #1, подсчет равен 6
            В методе MyTask() #2, подсчет равен 7
            В методе MyTask() #1, подсчет равен 7
            В методе MyTask() #2, подсчет равен 8
            В методе MyTask() #1, подсчет равен 8
            В методе MyTask() #2, подсчет равен 9
            MyTask №2 завершен
            В методе MyTask() #1, подсчет равен 9
            MyTask №1 завершен
            Основной поток завершен.
            */
        }

        static Task tsk;
        [Test]
        // Возвратить значение из задачи.
        public void TestTaskStatus()
        {
            Debug.WriteLine("Основной поток запущен.");
            CancellationTokenSource cts = new CancellationTokenSource();
            // Сконструировать объекты двух задач.
            tsk = new Task(() => _02_TaskIdTest.MyTask(cts.Token));
            Debug.WriteLine("Статус задачи после создания tsk: " + tsk.Status);
            Thread.Sleep(1000);
            tsk.Start();
            Debug.WriteLine("Статус задачи после запуска tsk: " + tsk.Status);

            // Сохранить метод Main() активным до завершения остальных задач,
            Thread.Sleep(6000);
            Debug.WriteLine("Статус задачи после завершения tsk: " + tsk.Status);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Статус задачи после создания tsk: Created
            Статус задачи после запуска tsk: WaitingToRun
            MyTask() №1 запущен
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            MyTask №1 завершен
            Статус задачи после завершения tsk: RanToCompletion
            Основной поток завершен.
            */
        }

        [Test]
        // Возвратить значение из задачи.
        public void TestTaskStatus_Canceled()
        {
            Debug.WriteLine("Основной поток запущен.");
            CancellationTokenSource cts = new CancellationTokenSource();
            // Сконструировать объекты двух задач.
            tsk = new Task(() => _02_TaskIdTest.MyTask(cts.Token), cts.Token);
            Debug.WriteLine("Статус задачи после создания tsk: " + tsk.Status);
            tsk.Start();
            Debug.WriteLine("Статус задачи после запуска tsk: " + tsk.Status);

            cts.CancelAfter(1000);
            Thread.Sleep(6000);
            Debug.WriteLine("Статус задачи после завершения tsk: " + tsk.Status);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Статус задачи после создания tsk: Created
            Статус задачи после запуска tsk: WaitingToRun
            MyTask() №1 запущен
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            An exception of type 'System.OperationCanceledException' occurred in mscorlib.dll but was not handled in user code
            The operation was canceled.

            Статус задачи после завершения tsk: Canceled
            Основной поток завершен.
            */
        }

        [Test]
        // Возвратить значение из задачи.
        public void TestTaskStatus_Faulted()
        {
            Debug.WriteLine("Основной поток запущен.");
            CancellationTokenSource cts = new CancellationTokenSource();
            // Сконструировать объекты двух задач.
            tsk = new Task(() => _02_TaskIdTest.MyTaskFault(cts.Token), cts.Token);
            Debug.WriteLine("Статус задачи после создания tsk: " + tsk.Status);
            tsk.Start();
            Debug.WriteLine("Статус задачи после запуска tsk: " + tsk.Status);

            Thread.Sleep(6000);
            Debug.WriteLine("Статус задачи после завершения tsk: " + tsk.Status);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Статус задачи после создания tsk: Created
            Статус задачи после запуска tsk: WaitingToRun
            MyTask() №1 запущен
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Exception thrown: 'System.Exception' in CSTest.dll
            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            Test

            Статус задачи после завершения tsk: Faulted
            Основной поток завершен.
            */
        }

        [Test]
        // Возвратить значение из задачи.
        public async Task TestTaskStatus_()
        {
            Debug.WriteLine("Основной поток запущен.");
            CancellationTokenSource cts = new CancellationTokenSource();
            // Сконструировать объекты двух задач.
             cts.CancelAfter(1000);
            try
            {
                tsk = Task.Run(() => _02_TaskIdTest.MyTask(cts.Token), cts.Token);
                Debug.WriteLine("Статус задачи после создания tsk: " + tsk.Status);
                await tsk;
            }
            catch (System.Exception)
            {
                Debug.WriteLine("Статус задачи после завершения tsk: " + tsk.Status);
            }

            Debug.WriteLine("Статус задачи после завершения tsk: " + tsk.Status);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Статус задачи после создания tsk: WaitingToRun
            MyTask() №1 запущен
            Статус задачи задачи tsk: Running
            Статус задачи задачи tsk: Running
            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            An exception of type 'System.OperationCanceledException' occurred in mscorlib.dll but was not handled in user code
            The operation was canceled.

            Exception thrown: 'System.OperationCanceledException' in mscorlib.dll
            Статус задачи после завершения tsk: Canceled
            Статус задачи после завершения tsk: Canceled
            Основной поток завершен.
            */
        }
    }
}
