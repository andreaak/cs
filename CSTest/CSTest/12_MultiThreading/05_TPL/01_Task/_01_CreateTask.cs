using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _01_CreateTask
    {
        // Статический метод выполняемый в качестве задачи. 
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

        public void MyTask()
        {
            Debug.WriteLine("MyTask() запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("В методе MyTask(), подсчет равен " + count);
            }
            Debug.WriteLine("MyTask завершен ");
        }


        [TestMethod]
        // Создать и запустить задачу на исполнение. 
        public void Test1()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект задачи. 
            Task tsk = new Task(_01_CreateTask.MyStaticTask);
            // Запустить задачу на исполнение, 
            tsk.Start();
            // метод Test1() активным до завершения метода MyStaticTask(). 
            for (int i = 0; i < 60; i++)
            {
                Debug.Write(".");
                Thread.Sleep(100);
            }
            Debug.WriteLine("\nОсновной поток завершен.");
        }

        [TestMethod]
        // Использовать метод экземпляра в качестве задачи. 
        public void Test2()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект типа _01_CreateTask. 
            _01_CreateTask mc = new _01_CreateTask();
            // Сконструировать объект задачи для метода mc.MyTask(). 
            Task tsk = new Task(mc.MyTask);
            // Запустить задачу на исполнение, 
            tsk.Start();
            // Сохранить метод Test2() активным до завершения метода MyTask(). 
            for (int i = 0; i < 60; i++)
            {
                Debug.Write(".");
                Thread.Sleep(100);
            }
            Debug.WriteLine("\nОсновной поток завершен.");
        }
    }
}
