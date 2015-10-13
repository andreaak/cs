using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _08_CancelTaskTest
    {
        // Метод, исполняемый как задача, 
        static void MyTask(object ct)
        {
            CancellationToken cancelTok = (CancellationToken)ct;
            // Проверить, отменена ли задача, прежде чем запускать ее. 
            cancelTok.ThrowIfCancellationRequested();
            Debug.WriteLine("MyTask() запущен");
            for (int count = 0; count < 10; count++)
            {
                // В данном примере для отслеживания отмены задачи применяется опрос, 
                if (cancelTok.IsCancellationRequested)
                {
                    Debug.WriteLine("Получен запрос на отмену задачи.");
                    cancelTok.ThrowIfCancellationRequested();
                }
                Thread.Sleep(500);
                Debug.WriteLine("В методе MyTask() подсчет равен " + count);
            }
            Debug.WriteLine("MyTask завершен");
        }

        [TestMethod]
        // Простой пример отмены задачи с использованием опроса.
        public void Test()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Создать объект источника признаков отмены. 
            CancellationTokenSource cancelTokSrc = new CancellationTokenSource();
            // Запустить задачу, передав признак отмены ей самой и делегату. 
            Task tsk = Task.Factory.StartNew(MyTask, cancelTokSrc.Token,
            cancelTokSrc.Token);
            // Дать задаче возможность исполняться вплоть до ее отмены. 
            Thread.Sleep(2000);
            try
            {
                // Отменить задачу. 
                cancelTokSrc.Cancel();
                // Приостановить выполнение метода Test() до тех пор, 
                // пока не завершится задача tsk. 
                tsk.Wait();
            }
            catch (AggregateException /*exc*/)
            {
                if (tsk.IsCanceled)
                    Debug.WriteLine("\nЗадача tsk отменена\n");
                //Для просмотра исключения снять комментарии со следующей строки кода: 
                // Debug.WriteLine(exc); 
            }
            finally
            {
                tsk.Dispose();
                cancelTokSrc.Dispose();
            }
            Debug.WriteLine("Основной поток завершен.");
        }
    }
}
