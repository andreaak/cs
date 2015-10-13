using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _04_TaskFactoryTest
    {
        public static void MyStaticTask()
        {
            Debug.WriteLine("MyStaticTask() запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("В методе MyStaticTask(), подсчет равен " + count);
            }
            Debug.WriteLine("MyStaticTask завершен");
        }

        [TestMethod]
        // Создать и запустить задачу на исполнение. 
        public void Test()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект задачи и Запустить задачу на исполнение 
            Task tsk = Task.Factory.StartNew(_04_TaskFactoryTest.MyStaticTask);
            // метод Test1() активным до завершения метода MyStaticTask(). 
            for (int i = 0; i < 60; i++)
            {
                Debug.Write(".");
                Thread.Sleep(100);
            }
            Debug.WriteLine("\nОсновной поток завершен.");
        }
    }
}
