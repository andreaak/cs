using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestClass]
    public class _09_ExceptionTest
    {
        [TestMethod]
        //Задача с необработанным исключением может приводить к генерированию исключения в GC
        public void TestTaskException1()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Запустить задачу, которая генерирует исключение NullReferenceException:
            Task task = Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Генерация исключения.");
                throw null;
            });
            Thread.Sleep(2000);
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
        //Обработка исключения через Wait
        public void TestTaskException2()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Запустить задачу, которая генерирует исключение NullReferenceException:
            Task task = Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Генерация исключения.");
                throw null;
            });
            try
            {
                task.Wait();
            }
            catch (AggregateException aex)
            {
                if (aex.InnerException is NullReferenceException)
                {
                    Debug.WriteLine("Null!");
                }
                else
                {
                    Debug.WriteLine("Unknown!");
                }
            }
            Thread.Sleep(2000);
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
        //Обработка исключения через свойство Exception
        public void TestTaskException3()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Запустить задачу, которая генерирует исключение NullReferenceException:
            Task task = Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Генерация исключения.");
                throw null;
            });
            Thread.Sleep(2000);
            if (task.Exception != null)
            {
                Debug.WriteLine("Exception: " + task.Exception.InnerException.Message);
            }
            Debug.WriteLine("Основной поток завершен.");
        }

        [TestMethod]
        //Обработка исключения через свойство Exception
        public void TestTaskException4()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Запустить задачу, которая генерирует исключение NullReferenceException:
            Task task = Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Генерация исключения.");
                throw null;
            });
            task.ContinueWith((t) =>
            {
                Debug.WriteLine("Продолжение задачи.");
                if (t.Exception != null)
                {
                    Debug.WriteLine("Exception: " + t.Exception.InnerException.Message);
                }
            }, TaskContinuationOptions.OnlyOnFaulted);
            Thread.Sleep(2000);
            Debug.WriteLine("Основной поток завершен.");
        }
    }
}
