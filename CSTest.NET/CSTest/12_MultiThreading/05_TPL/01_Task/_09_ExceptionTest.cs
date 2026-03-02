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
        public void TestTaskException1NotHandle()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Запустить задачу, которая генерирует исключение NullReferenceException:
            Task task = Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Генерация исключения.");
                throw null;
            });
            Thread.Sleep(10000);
            Debug.WriteLine("Основной поток завершен.");

            /*
            Основной поток запущен.
            Генерация исключения.
            Exception thrown: 'System.Exception' in CSTest.dll
            Основной поток завершен.
            */
        }

        [Test]
        //Обработка исключения через Wait
        public void TestTaskException2Wait()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Запустить задачу, которая генерирует исключение NullReferenceException:
            Task task = Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Генерация исключения.");
                Thread.Sleep(1000);
                throw new NullReferenceException();
            });
            try
            {
                Debug.WriteLine("Ожидание!");

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

            /*
            Основной поток запущен.
            Ожидание!
            Генерация исключения.
            Null!
            Основной поток завершен. 
             
             */
        }

        [Test]
        //Обработка исключения через свойство Exception
        public void TestTaskException3ExceptionProperty()
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
        public void TestTaskException4ContinueOnFaulted()
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
        public void TestTaskException5WaitAll()
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

        [Test]
        public void TestTaskException6WaitAny()
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
        

        [Test]
        public void TestTaskException8ParentTask()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Дочерние задачи

            Task parent = new Task(() =>
            {
                new Task(() =>
                {
                    Thread.Sleep(506);
                    Debug.WriteLine("Дочерняя задача #1 завершила свою работу");
                }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() =>
                {
                    Thread.Sleep(600);
                    Debug.WriteLine("Дочерняя задача #2 завершила свою работу");
                }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() =>
                {
                    Thread.Sleep(700);
                    throw new Exception("Ошибка в дочерней задаче #3");
                }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() =>
                {
                    Thread.Sleep(800);
                    Debug.WriteLine("Дочерняя задача #4 завершила свою работу");
                }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() =>
                {
                    new Task(() => { 
                        throw new Exception("Ошибка в дочерней задаче #5.1 второго уровня вложенности"); 
                    }, TaskCreationOptions.AttachedToParent).Start();
                    
                    Thread.Sleep(900);
                    //throw new Exception("Ошибка в дочерней задаче #5");
                }, TaskCreationOptions.AttachedToParent).Start();

            });
            parent.Start();

            try
            {
                parent.Wait(); // Родитель ждет завершения всех дочерних задач, даже если завершение обусловлено исключением.

            }
            catch (AggregateException ex)
            {

                // Вложенные исключения родительской задачи
                foreach (var item in ex.InnerExceptions)
                {
                    //// Вложенные исключения дочерних задач
                    //if (item is AggregateException aggregateException)
                    //{
                    //    foreach (var innerException in aggregateException.InnerExceptions)
                    //    {
                    //        Debug.WriteLine($"Сообщение из исключения дочерней задачи - {innerException.Message}");
                    //    }
                    //}
                    //else
                    //{
                    //    Debug.WriteLine($"Сообщение из исключения родительской задачи - {item.Message}");
                    //}
                    HandleTaskExceptions(ex);
                }
            }
            Debug.WriteLine("");
            Debug.WriteLine($"Cтaтyc родительской задачи - {parent.Status}");
            Debug.WriteLine("Метод Main завершил свою работу");
            Thread.Sleep(5000);

            /*
            Основной поток запущен.
            Exception thrown: 'System.Exception' in CSTest.dll
            Ошибка в дочерней задаче #5

            Дочерняя задача #1 завершила свою работу
            
            Дочерняя задача #2 завершила свою работу
            
            Exception thrown: 'System.Exception' in CSTest.dll
            Ошибка в дочерней задаче #3

            Дочерняя задача #4 завершила свою работу

            Exception thrown: 'System.AggregateException' in System.Private.CoreLib.dll
            One or more errors occurred.

            Сообщение из исключения дочерней задачи - Ошибка в дочерней задаче #5
            Сообщение из исключения дочерней задачи - Ошибка в дочерней задаче #3

            Cтaтyc родительской задачи - Faulted
            Метод Main завершил свою работу
            */
        }

        [Test]
        public void TestTaskException9InnerTask()
        {
            Debug.WriteLine("Основной поток запущен.");
            // Дочерние задачи

            Task parent = new Task(() =>
            {
                new Task(() =>
                {
                    Thread.Sleep(500);
                    Debug.WriteLine("Дочерняя задача #1 завершила свою работу");
                }).Start();
                new Task(() =>
                {
                    Thread.Sleep(600);
                    Debug.WriteLine("Дочерняя задача #2 завершила свою работу");
                }).Start();
                new Task(() =>
                {
                    Thread.Sleep(700);
                    throw new Exception("Ошибка в дочерней задаче #3");
                }).Start();
                new Task(() =>
                {
                    Thread.Sleep(800);
                    Debug.WriteLine("Дочерняя задача #4 завершила свою работу");
                }).Start();
                new Task(() =>
                {
                    new Task(() => {
                        throw new Exception("Ошибка в дочерней задаче #5.1 второго уровня вложенности");
                    }).Start();

                    Thread.Sleep(900);
                    //throw new Exception("Ошибка в дочерней задаче #5");
                }).Start();

            });
            parent.Start();

            try
            {
                parent.Wait(); // Родитель ждет завершения всех дочерних задач, даже если завершение обусловлено исключением.

            }
            catch (AggregateException ex)
            {
                // Вложенные исключения родительской задачи
                foreach (var item in ex.InnerExceptions)
                {
                    HandleTaskExceptions(ex);
                }
            }

            Debug.WriteLine("");
            Debug.WriteLine($"Cтaтyc родительской задачи - {parent.Status}");
            Debug.WriteLine("Метод Main завершил свою работу");
            Thread.Sleep(5000); 

            /*
            Основной поток запущен.
            
            Exception thrown: 'System.Exception' in CSTest.dll
            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            Ошибка в дочерней задаче #5.1 второго уровня вложенности

            Дочерняя задача #1 завершила свою работу
            
            Exception thrown: 'System.Exception' in CSTest.dll
            An exception of type 'System.Exception' occurred in CSTest.dll but was not handled in user code
            Ошибка в дочерней задаче #3

            Дочерняя задача #2 завершила свою работу
            Дочерняя задача #4 завершила свою работу
            */
        }

        private static void HandleTaskExceptions(AggregateException parentException)
        {
            foreach (var innerException in parentException.InnerExceptions)
            {
                if (innerException is AggregateException aggregateException)
                {
                    HandleTaskExceptions(aggregateException);
                }
                else
                {
                    Console.WriteLine($"Сообщение из исключения - {innerException.Message}");
                }
            }
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
    }

    class HandledException : Exception
    {
        public HandledException(string message)
            : base(message)
        { }
    }
}
