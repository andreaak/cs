using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    // Применение класса TaskFactory для создания и запуска задачи.

    [TestFixture]
    public class _04_TaskFactoryTest
    {
        public static void TestStaticTask()
        {
            Debug.WriteLine("MyStaticTask() запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Debug.WriteLine("В методе MyStaticTask(), подсчет равен " + count);
            }
            Debug.WriteLine("MyStaticTask завершен");
        }

        [Test]
        // Создать и запустить задачу на исполнение. 
        public void TestTaskFactory()
        {
            Debug.WriteLine("Основной поток запущен.");
            
            // Создание экземпляра задачи с использованием свойства Factory, типа TaskFactory.
            // Сконструировать объект задачи и Запустить задачу на исполнение 
            // В случае запуска задачи через TaskFactory, вызов метода task.Start() не требуется.
            Task tsk = Task.Factory.StartNew(_04_TaskFactoryTest.TestStaticTask);
            
            for (int i = 0; i < 60; i++)
            {
                Debug.WriteLine(".");
                Thread.Sleep(100);
            }
            Debug.WriteLine("\nОсновной поток завершен.");
        }
    }
}
