using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    /*
    задача в основном использует потоки из пула потоков которые являются BackGround
    */
    [TestFixture]
    public class _01_CreateTaskTest
    {
        static string temp = new string(' ', 10);
        // Статический метод выполняемый в качестве задачи. 
        public static void TestStaticTask()
        {
            Debug.WriteLine(temp + "MyStaticTask() запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(100);
                Debug.WriteLine(temp + "В методе MyStaticTask(), подсчет равен " + count);
            }
            Debug.WriteLine(temp + "MyStaticTask завершен");
        }

        public void TestTask()
        {
            Debug.WriteLine(temp + "MyTask() запущен");
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(100);
                Debug.WriteLine(temp + "В методе MyTask(), подсчет равен " + count);
            }
            Debug.WriteLine(temp + "MyTask завершен ");
        }

        public void TaskWithParam(object before)
        {
            Debug.WriteLine(temp + "MyTask() запущен");
            for (int count = 0; count < (int)before; count++)
            {
                Thread.Sleep(100);
                Debug.WriteLine(temp + "В методе MyTask(), подсчет равен " + count);
            }
            Debug.WriteLine(temp + "MyTask завершен ");
        }


        [Test]
        // Создать и запустить задачу на исполнение. 
        public void TestTaskStart1()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект задачи. 
            Task task = new Task(_01_CreateTaskTest.TestStaticTask);
            // Запустить задачу на исполнение, 
            task.Start();
            // метод Test1() активным до завершения метода MyStaticTask(). 
            for (int i = 0; i < 11; i++)
            {
                Debug.WriteLine(".");
                Thread.Sleep(100);
            }
            Debug.WriteLine("\nОсновной поток завершен.");
            /*
            Основной поток запущен.
            .
                      MyStaticTask() запущен
            .
                      В методе MyStaticTask(), подсчет равен 0
            .
                      В методе MyStaticTask(), подсчет равен 1
            .
                      В методе MyStaticTask(), подсчет равен 2
            .
                      В методе MyStaticTask(), подсчет равен 3
            .
                      В методе MyStaticTask(), подсчет равен 4
                      В методе MyStaticTask(), подсчет равен 5
            .
                      В методе MyStaticTask(), подсчет равен 6
            .
                      В методе MyStaticTask(), подсчет равен 7
            .
                      В методе MyStaticTask(), подсчет равен 8
            .
            .
                      В методе MyStaticTask(), подсчет равен 9
                      MyStaticTask завершен

            Основной поток завершен.
            */
        }

        [Test]
        // Использовать метод экземпляра в качестве задачи. 
        public void TestTaskStart2()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект типа _01_CreateTask. 
            _01_CreateTaskTest mc = new _01_CreateTaskTest();
            // Сконструировать объект задачи для метода mc.MyTask(). 
            Task task = new Task(mc.TestTask);
            // Запустить задачу на исполнение, 
            task.Start();
            // Сохранить метод Test2() активным до завершения метода MyTask(). 
            for (int i = 0; i < 11; i++)
            {
                Debug.WriteLine(".");
                Thread.Sleep(100);
            }
            Debug.WriteLine("\nОсновной поток завершен.");

            /*
            Основной поток запущен.
            .
                      MyTask() запущен
            .
                      В методе MyTask(), подсчет равен 0
            .
                      В методе MyTask(), подсчет равен 1
            .
                      В методе MyTask(), подсчет равен 2
                      В методе MyTask(), подсчет равен 3
            .
                      В методе MyTask(), подсчет равен 4
            .
                      В методе MyTask(), подсчет равен 5
            .
                      В методе MyTask(), подсчет равен 6
            .
                      В методе MyTask(), подсчет равен 7
            .
                      В методе MyTask(), подсчет равен 8
            .
                      В методе MyTask(), подсчет равен 9
                      MyTask завершен 
            .

            Основной поток завершен.
            */
        }

        [Test]
        // Использовать метод экземпляра в качестве задачи. 
        public void TestTaskStart3()
        {
            Debug.WriteLine("Основной поток запущен.");

            Task task = new Task(TestStaticTask);
            Debug.WriteLine("1. " + task.Status);
            
            task.Start();
            Debug.WriteLine("2. " + task.Status);

            Thread.Sleep(500);
            Debug.WriteLine("3. " + task.Status);
            
            Thread.Sleep(2000);
            Debug.WriteLine("4. " + task.Status);
            
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            1. Created
            2. WaitingToRun
                      MyStaticTask() запущен
                      В методе MyStaticTask(), подсчет равен 0
                      В методе MyStaticTask(), подсчет равен 1
                      В методе MyStaticTask(), подсчет равен 2
                      В методе MyStaticTask(), подсчет равен 3
            3. Running
                      В методе MyStaticTask(), подсчет равен 4
                      В методе MyStaticTask(), подсчет равен 5
                      В методе MyStaticTask(), подсчет равен 6
                      В методе MyStaticTask(), подсчет равен 7
                      В методе MyStaticTask(), подсчет равен 8
                      В методе MyStaticTask(), подсчет равен 9
                      MyStaticTask завершен
            4. RanToCompletion
            Основной поток завершен.
            */
        }

        [Test]
        public void TestTaskStart4Parameter()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект типа _01_CreateTask. 
            _01_CreateTaskTest mc = new _01_CreateTaskTest();
            // Сконструировать объект задачи для метода mc.MyTask(). 
            Task task = new Task(mc.TaskWithParam, 5);
            // Запустить задачу на исполнение, 
            task.Start();
            // Сохранить метод Test2() активным до завершения метода MyTask(). 
            for (int i = 0; i < 6; i++)
            {
                Debug.WriteLine(".");
                Thread.Sleep(100);
            }
            Debug.WriteLine("\nОсновной поток завершен.");

            /*
            Основной поток запущен.
            .
                      MyTask() запущен
            .
                      В методе MyTask(), подсчет равен 0
            .
                      В методе MyTask(), подсчет равен 1
                      В методе MyTask(), подсчет равен 2
            .
            .
                      В методе MyTask(), подсчет равен 3
                      В методе MyTask(), подсчет равен 4
                      MyTask завершен 
            .

            Основной поток завершен.
            */
        }
    }
}
