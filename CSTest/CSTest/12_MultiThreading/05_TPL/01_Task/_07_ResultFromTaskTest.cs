using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _07_ResultFromTaskTest
    {
        // Простейший метод, возвращающий результат и не принимающий аргументов, 
        static bool MyTask()
        {
            return true;
        }
        // Этот метод возвращает сумму из положительного целого значения, 
        // которое ему передается в качестве единственного параметра 
        static int SumIt(object v)
        {
            int x = (int)v;
            int sum = 0;
            for (; x > 0; x--)
                sum += x;
            return sum;
        }

        [TestMethod]
        // Возвратить значение из задачи.
        public void Test()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Сконструировать объект первой задачи. 
            Task<bool> tsk = Task<bool>.Factory.StartNew(MyTask);
            Debug.WriteLine("Результат после выполнения задачи MyTask: " +
            tsk.Result);
            // Сконструировать объект второй задачи. 
            Task<int> tsk2 = Task<int>.Factory.StartNew(SumIt, 3);
            Debug.WriteLine("Результат после выполнения задачи SumIt: " +
            tsk2.Result);
            tsk.Dispose();
            tsk2.Dispose();
            Debug.WriteLine("Основной поток завершен.");
        }
    }
}
