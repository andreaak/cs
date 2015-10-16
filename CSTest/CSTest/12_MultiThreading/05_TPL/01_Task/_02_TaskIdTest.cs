using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _02_TaskIdTest
    {
        // Метод, исполняемый как задача,
        static void MyTask()
        {
            Debug.WriteLine("MyTask() №" + Task.CurrentId + " запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("В методе MyTask() #" + Task.CurrentId +
                ", подсчет равен " + count);
            }
            Debug.WriteLine("MyTask №" + Task.CurrentId + " завершен");
        }

        [TestMethod]
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
            for (int i = 0; i < 60; i++)
            {
                //Debug.Write(".");
                Thread.Sleep(100);
            }
            Debug.WriteLine("Основной поток завершен.");
        }
    }
}
