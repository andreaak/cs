using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    // WaitAll() - Ожидает завершения выполнения всех указанных объектов Task.
    // WaitAny() - Ожидает завершения выполнения любого из указанных объектов Task.
    [TestClass]
    public class _03_WaitTest
    {
        // Метод, исполняемый как задача, 
        static void MyTask(object parameter)
        {
            int interval = (int)(parameter ?? 500);
            Debug.WriteLine("MyTask() №" + Task.CurrentId + " запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(interval);
                Debug.WriteLine("В методе MyTask() #" + Task.CurrentId + ", подсчет равен " + count);
            }
            Debug.WriteLine("MyTask №" + Task.CurrentId + " завершен");
        }

        [TestMethod]
        // Применить метод Wait()
        public void TestTaskWait1()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объекты двух задач. 
            Task task1 = new Task(MyTask, null);
            Task task2 = new Task(MyTask, null);
            // Запустить задачи на исполнение. 
            task1.Start();
            task2.Start();
            Debug.WriteLine("Идентификатор задачи tsk: " + task1.Id);
            Debug.WriteLine("Идентификатор задачи tsk2: " + task2.Id);

            task2.Wait();
            
            // Вариант 1
            // Приостановить выполнение метода Main() до тех пор, 
            // пока не завершатся обе задачи task1 и task2 
            task1.Wait();
            
            //// Вариант 2
            //while (!task1.IsCompleted)
            //{
            //    Thread.Sleep(100);
            //}

            //// Вариант 3
            //IAsyncResult asyncResult = task1;
            //ManualResetEvent waitHandle = asyncResult.AsyncWaitHandle as ManualResetEvent;
            //waitHandle.WaitOne();

            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Идентификатор задачи tsk: 1
            Идентификатор задачи tsk2: 2
            MyTask() №1 запущен
            MyTask() №2 запущен
            В методе MyTask() #2, подсчет равен 0
            В методе MyTask() #1, подсчет равен 0
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
            В методе MyTask() #1, подсчет равен 9
            MyTask №2 завершен
            MyTask №1 завершен
            Основной поток завершен.
            */
        }

        [TestMethod]
        public void TestTaskWait2Any()
        {
            Debug.WriteLine("Основной поток запущен.");

            Task task1 = new Task(MyTask, 200);
            Task task2 = new Task(MyTask, 300);

            // Выполнение задач.
            task1.Start();
            task2.Start();

            Debug.WriteLine("Идентификатор задачи task1: " + task1.Id);
            Debug.WriteLine("Идентификатор задачи task2: " + task2.Id);

            Task.WaitAny(task1, task2);

            Debug.WriteLine("Основной поток завершен.");
            
            /*
            Основной поток запущен.
            Идентификатор задачи task1: 1
            Идентификатор задачи task2: 2
            MyTask() №1 запущен
            MyTask() №2 запущен
            В методе MyTask() #1, подсчет равен 0
            В методе MyTask() #2, подсчет равен 0
            В методе MyTask() #1, подсчет равен 1
            В методе MyTask() #2, подсчет равен 1
            В методе MyTask() #1, подсчет равен 2
            В методе MyTask() #1, подсчет равен 3
            В методе MyTask() #2, подсчет равен 2
            В методе MyTask() #1, подсчет равен 4
            В методе MyTask() #2, подсчет равен 3
            В методе MyTask() #1, подсчет равен 5
            В методе MyTask() #1, подсчет равен 6
            В методе MyTask() #2, подсчет равен 4
            В методе MyTask() #1, подсчет равен 7
            В методе MyTask() #2, подсчет равен 5
            В методе MyTask() #1, подсчет равен 8
            В методе MyTask() #1, подсчет равен 9
            MyTask №1 завершен
            Основной поток завершен.
            */
        }

        [TestMethod]
        public void TestTaskWait2All()
        {
            Debug.WriteLine("Основной поток запущен.");

            Task task1 = new Task(MyTask, 200);
            Task task2 = new Task(MyTask, 300);

            // Выполнение задач.
            task1.Start();
            task2.Start();

            Debug.WriteLine("Идентификатор задачи task1: " + task1.Id);
            Debug.WriteLine("Идентификатор задачи task2: " + task2.Id);

            Task.WaitAll(task1, task2);

            Debug.WriteLine("Основной поток завершен.");
            
            /*
            Основной поток запущен.
            Идентификатор задачи task1: 1
            Идентификатор задачи task2: 2
            MyTask() №2 запущен
            MyTask() №1 запущен
            В методе MyTask() #1, подсчет равен 0
            В методе MyTask() #2, подсчет равен 0
            В методе MyTask() #1, подсчет равен 1
            В методе MyTask() #1, подсчет равен 2
            В методе MyTask() #2, подсчет равен 1
            В методе MyTask() #1, подсчет равен 3
            В методе MyTask() #2, подсчет равен 2
            В методе MyTask() #1, подсчет равен 4
            В методе MyTask() #2, подсчет равен 3
            В методе MyTask() #1, подсчет равен 5
            В методе MyTask() #1, подсчет равен 6
            В методе MyTask() #2, подсчет равен 4
            В методе MyTask() #1, подсчет равен 7
            В методе MyTask() #2, подсчет равен 5
            В методе MyTask() #1, подсчет равен 8
            В методе MyTask() #1, подсчет равен 9
            MyTask №1 завершен
            В методе MyTask() #2, подсчет равен 6
            В методе MyTask() #2, подсчет равен 7
            В методе MyTask() #2, подсчет равен 8
            В методе MyTask() #2, подсчет равен 9
            MyTask №2 завершен
            Основной поток завершен.
            */
        }
    }
}
