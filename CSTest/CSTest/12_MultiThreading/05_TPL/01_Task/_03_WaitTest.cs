using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _03_WaitTest
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
        // Применить метод Wait()
        public void Test()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объекты двух задач. 
            Task tsk = new Task(_03_WaitTest.MyTask);
            Task tsk2 = new Task(_03_WaitTest.MyTask);
            // Запустить задачи на исполнение. 
            tsk.Start();
            tsk2.Start();
            Debug.WriteLine("Идентификатор задачи tsk: " + tsk.Id);
            Debug.WriteLine("Идентификатор задачи tsk2: " + tsk2.Id);
            // Приостановить выполнение метода Main() до тех пор, 
            // пока не завершатся обе задачи tsk и tsk2 
            tsk.Wait();
            tsk2.Wait();
            Debug.WriteLine("Основной поток завершен.");
        }
    }
}
