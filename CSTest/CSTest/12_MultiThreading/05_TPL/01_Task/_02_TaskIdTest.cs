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

        [Test]
        // Возвратить значение из задачи.
        public void TestTaskId()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объекты двух задач.
            Task tsk = new Task(_02_TaskIdTest.MyTask);
            Task tsk2 = new Task(_02_TaskIdTest.MyTask);// Запустить задачи на исполнение,
            tsk.Start();
            tsk2.Start();
            Debug.WriteLine("Идентификатор задачи tsk: " + tsk.Id);
            Debug.WriteLine("Идентификатор задачи tsk2: " + tsk2.Id);

            // Сохранить метод Main() активным до завершения остальных задач,
            Thread.Sleep(6000);
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
    }
}
