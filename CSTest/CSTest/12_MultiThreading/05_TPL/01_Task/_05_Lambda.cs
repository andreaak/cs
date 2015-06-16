using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _05_Lambda
    {
        [TestMethod]
        // Применить лямбда-выражение в качестве задачи.
        public void Test()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Далее лямбда-выражение используется для определения задачи. 
            Task tsk = Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Задача запущена");
                for (int count = 0; count < 10; count++)
                {
                    Thread.Sleep(500);
                    Debug.WriteLine("Подсчет в задаче равен " + count);
                }
                Debug.WriteLine("Задача завершена");
            });
            // Ожидать завершения задачи tsk. 
            tsk.Wait();
            // Освободить задачу tsk. 
            tsk.Dispose();
            Debug.WriteLine("Основной поток завершен.");
        }
    }
}
