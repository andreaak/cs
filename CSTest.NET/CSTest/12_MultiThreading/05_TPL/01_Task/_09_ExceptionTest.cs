using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSTest._12_MultiThreading._05_TPL._01_Task
{
    [TestFixture]
    public class _09_ExceptionTest
    {
        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        public void TestTaskException5AggregateException()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Запустить задачу, которая генерирует исключение NullReferenceException:
            Task task1 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                throw new Exception("Exception 1");

            });
            Task task2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                throw new Exception("Exception 2");
            });

            try
            {
                Task.WaitAll(task1, task2);
            }
            catch (AggregateException ex)
            {
                PrintExceptions(ex);
            }
            Thread.Sleep(2000);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Exception thrown: 'System.Exception' in CSTest.dll
            Exception thrown: 'System.Exception' in CSTest.dll
            Exception thrown: 'System.AggregateException' in mscorlib.dll
            ExceptionType: System.Exception, Message: Exception 1
            ExceptionType: System.Exception, Message: Exception 2
            Основной поток завершен. 
            */


        }

        public static bool Contains(string str, string substring,
                                    StringComparison comp)
        {
            return str?.IndexOf(substring, comp) >= 0;
        }

        [Test]
        public void TestTaskException6AggregateException()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Запустить задачу, которая генерирует исключение NullReferenceException:
            Task task1 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                throw new Exception("Exception 1");

            });
            Task task2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
            });

            try
            {
                Task.WaitAny(task1, task2);
            }
            catch (AggregateException ex)
            {
                PrintExceptions(ex);
            }
            Thread.Sleep(2000);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Exception thrown: 'System.Exception' in CSTest.dll
            Основной поток завершен.
            */
        }

        [Test]
        public void TestTaskException7Handle()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Запустить задачу, которая генерирует исключение NullReferenceException:
            Task task1 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                throw new Exception("Exception 1");
            });
            Task task2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                throw new HandledException("Exception 2");
            });

            try
            {
                Task.WaitAll(task1, task2);
            }
            catch (AggregateException ex)
            {
                Debug.WriteLine("Exception before:");

                PrintExceptions(ex);
                try
                {
                    /*
                    Считает обработанным исключения HandledException
                    Все остальные исключения попадают в новый объект AggregateException 
                    и генерируется новое исключение 
                    */
                    ex.Handle(e => e is HandledException);
                }
                catch (AggregateException ex2)
                {
                    Debug.WriteLine("Exception not handled:");
                    PrintExceptions(ex2);
                }
            }
            Thread.Sleep(2000);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Exception thrown: 'System.Exception' in CSTest.dll
            Exception thrown: 'System.AggregateException' in mscorlib.dll
            Exception before:
            ExceptionType: System.Exception, Message: Exception 1
            ExceptionType: CSTest._12_MultiThreading._05_TPL._01_Task.HandledException, Message: Exception 2
            Exception thrown: 'System.AggregateException' in mscorlib.dll
            Exception not handled:
            ExceptionType: System.Exception, Message: Exception 1
            Основной поток завершен.
            */
        }

        [Test]
        /*
        Если задача имеет присоединенную дочернюю задачу, которая создает исключение, 
        это исключение заключается в AggregateException исключение перед распространением в родительскую задачу, 
        которая заключает его в собственное исключение AggregateException исключение перед распространением обратно в вызывающий поток. 
        В таких случаях InnerExceptions свойство AggregateException исключение, 
        перехваченное блоком Task.Wait, Task<TResult>.Wait, Task.WaitAny или Task.WaitAll метод 
        содержит один или несколько AggregateException экземпляров не исходные исключения, которые вызвали сбой. 
        
        Чтобы избежать необходимости выполнения итерации по вложенным AggregateException исключения, 
        можно использовать Flatten метод для удаления всех вложенных AggregateException исключения, 
        чтобы InnerExceptions свойство возвращаемого AggregateException объекта содержало исходные исключения.

        Этот метод рекурсивно выполняет сведение всех экземпляров AggregateException исключения, 
        которые являются внутренние исключения текущего AggregateException экземпляра. 
        Внутренние исключения возвращается в новом AggregateException объединение всех внутренних исключений из исключения дерева, 
        корнем текущего являются AggregateException экземпляр. 
        */
        public void TestTaskException7Flatten()
        {
            Debug.WriteLine("Основной поток запущен.");
            var task1 = Task.Factory.StartNew(() =>
            {
                var child1 = Task.Factory.StartNew(() =>
                {
                    var child2 = Task.Factory.StartNew(() =>
                    {
                        // This exception is nested inside three AggregateExceptions.
                        throw new HandledException("Attached child2 faulted.");
                    }, TaskCreationOptions.AttachedToParent);

                    // This exception is nested inside two AggregateExceptions.
                    throw new HandledException("Attached child1 faulted.");
                }, TaskCreationOptions.AttachedToParent);
            });

            try
            {
                task1.Wait();
            }
            catch (AggregateException ae)
            {
                Debug.WriteLine("Exception before:");
                PrintExceptions(ae);

                Debug.WriteLine("Exception after:");
                AggregateException newEx = ae.Flatten();
                PrintExceptions(newEx);
            }
            Thread.Sleep(2000);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Exception thrown: 'CSTest._12_MultiThreading._05_TPL._01_Task.HandledException' in CSTest.dll
            Exception thrown: 'CSTest._12_MultiThreading._05_TPL._01_Task.HandledException' in CSTest.dll
            Exception thrown: 'System.AggregateException' in mscorlib.dll

            Exception before:
            ExceptionType: System.AggregateException, Message: One or more errors occurred.
	            ExceptionType: CSTest._12_MultiThreading._05_TPL._01_Task.HandledException, Message: Attached child1 faulted.
	            ExceptionType: System.AggregateException, Message: One or more errors occurred.
		            ExceptionType: CSTest._12_MultiThreading._05_TPL._01_Task.HandledException, Message: Attached child2 faulted.

            Exception after:
            ExceptionType: CSTest._12_MultiThreading._05_TPL._01_Task.HandledException, Message: Attached child1 faulted.
            ExceptionType: CSTest._12_MultiThreading._05_TPL._01_Task.HandledException, Message: Attached child2 faulted.
            Основной поток завершен.
            */
        }

        private static void PrintExceptions(AggregateException ex, int level = 0)
        {
            foreach (var e in ex.InnerExceptions)
            {
                Debug.WriteLine(new string('\t', level) + "ExceptionType: {0}, Message: {1}", e.GetType(), e.Message);
                var exception = e as AggregateException;
                if (exception != null)
                    PrintExceptions(exception, ++level);
            }
        }

        private static void PrintException(Exception ex)
        {
            Debug.WriteLine("ExceptionType: {0}, Message: {1}", ex.GetType(), ex.Message);
        }
    }

    class HandledException : Exception
    {
        public HandledException(string message)
            : base(message)
        { }
    }
}
