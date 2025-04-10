using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestFixture]
    public class _07_ResultFromTaskTest
    {
        // Простейший метод, возвращающий результат и не принимающий аргументов, 
        static bool TestTask()
        {
            return true;
        }
        // Этот метод возвращает сумму из положительного целого значения, 
        // которое ему передается в качестве единственного параметра 
        static int Sum(object v)
        {
            int x = (int)v;
            int sum = 0;
            for (; x > 0; x--)
            {
                sum += x;
            }
            return sum;
        }

        [Test]
        // Возвратить значение из задачи.
        public void TestTaskResult()
        {
            Debug.WriteLine("Основной поток запущен.");

            // Сконструировать объект первой задачи. 
            Task<bool> task1 = Task<bool>.Factory.StartNew(TestTask);
            Debug.WriteLine("Результат после выполнения задачи TestTask: " + task1.Result);

            // Сконструировать объект второй задачи. 
            Task<int> task2 = Task<int>.Factory.StartNew(Sum, 3);
            Debug.WriteLine("Результат после выполнения задачи Sum: " + task2.Result);

            task1.Dispose();
            task2.Dispose();
            Debug.WriteLine("Основной поток завершен.");
            /*
            Основной поток запущен.
            Результат после выполнения задачи TestTask: True
            Результат после выполнения задачи Sum: 6
            Основной поток завершен.
            */
        }
    }
}
