using CSTest._13_Exceptions._0_Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Diagnostics;

namespace CSTest._13_Exceptions
{
    /*
    Обработка исключительных ситуаций (exception handling) — механизм, 
    предназначенный для описания реакции программы на ошибки времени 
    выполнения и другие возможные проблемы (исключения), которые могут 
    возникнуть при выполнении программы и приводят к невозможности 
    (бессмысленности) дальнейшей отработки программой её базового алгоритма.
 
    Главное преимущество обработки исключительных ситуаций заключается в том, 
    что она позволяет автоматизировать получение большей части кода, 
    который раньше приходилось вводить в любую крупную программу вручную 
    для обработки ошибок. Так, если программа написана на языке программирования 
    без обработки исключительных ситуаций, то при неудачном выполнении методов 
    приходится возвращать коды ошибок, которые необходимо проверять вручную при 
    каждом вызове метода. Это не только трудоемкий, но и чреватый ошибками процесс. 
    */

    [TestClass]
    public class Test
    {
        /*
        Основу обработки исключительных ситуаций в С# составляет 
        пара ключевых слов try и catch. Эти ключевые слова действуют 
        совместно и не могут быть использованы порознь. 
        Когда исключение генерируется оператором try, оно перехватывается 
        составляющим ему пару оператором catch, который затем обрабатывает 
        это исключение. В зависимости от типа исключения выполняется и 
        соответствующий оператор catch. Так, если типы генерируемого исключения и того, 
        что указывается в операторе catch, совпадают, то выполняется именно этот оператор, 
        а все остальные пропускаются. 
        Если исключение не генерируется, то блок оператора try завершается как обычно, 
        и все его операторы catch пропускаются. Выполнение программы возобновляется 
        с первого оператора, следующего после завершающего оператора catch. 
        Таким образом, оператор catch выполняется лишь в том случае, если генерируется исключение. 

        */

        [TestMethod]
        public void TestExceptions1()
        {
            int a = 1, n = 2;

            try
            {
                // Попытка деления на ноль.
                a = a / (2 - n);

                Debug.WriteLine("a = {0}", a);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Обработка исключения.");
                Debug.WriteLine(e.Message);
            }
        }

        [TestMethod]
        public void TestExceptions2()
        {
            Exception ex = new Exception("Мое Исключение");

            try
            {
                throw ex;
                //или throw new Exception("Мое Исключение");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Обработка исключения.");
                Debug.WriteLine(e.Message);
            }
        }

        [TestMethod]
        public void TestExceptions3()
        {
            try
            {
                ClassWithException instance = new ClassWithException();
                instance.MyMethod();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Имя члена:               {0}", e.TargetSite);
                Debug.WriteLine("Класс определяющий член: {0}", e.TargetSite.DeclaringType);
                Debug.WriteLine("Тип члена:               {0}", e.TargetSite.MemberType);
                Debug.WriteLine("Message:                 {0}", e.Message);
                Debug.WriteLine("Source:                  {0}", e.Source);
                Debug.WriteLine("Help Link:               {0}", e.HelpLink);
                Debug.WriteLine("Stack:                   {0}", e.StackTrace);

                foreach (DictionaryEntry de in e.Data)
                {
                    Debug.WriteLine("{0} : {1}", de.Key, de.Value);
                }
            }
        }

        [TestMethod]
        public void TestExceptions4()
        {
            try
            {
                throw new UserException();
            }
            catch (UserException e)
            {
                Debug.WriteLine("Обработка исключения.");
                e.Method();
            }
        }

        [TestMethod]
        public void TestExceptions5Finally()
        {
            int a = 1, n = 2;

            try
            {
                //Console.ForegroundColor = ConsoleColor.Yellow;
                Debug.WriteLine("Попытка деления на ноль.");
                Debug.WriteLine("a / (2 - n) = {0}", a / (2 - n));
            }
            catch (Exception e)
            {
                //Console.ForegroundColor = ConsoleColor.White;
                //Console.BackgroundColor = ConsoleColor.Red;
                Debug.WriteLine("Обработка исключения.");
                Debug.WriteLine(e.Message);
            }
            finally
            {
                //Console.ForegroundColor = ConsoleColor.Gray;
                //Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        [TestMethod]
        public void TestExceptions6Inner()
        {
            ClassWithException instance = new ClassWithException();
            //instance.CatchInner(); // Попробовать вызвать.
            try
            {
                instance.CatchInner();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception caught: {0}", exception.Message);
                Debug.WriteLine("Inner Exception : {0}", exception.InnerException.Message);
            }
        }

        [TestMethod]
        public void TestExceptions7StackOverflowException()
        {
            try
            {
                ClassWithException instance = new ClassWithException();
                instance.RecurseMethod();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                // finally - не сработает при StackOverflowException.
                while (true)
                {
                    Debug.WriteLine("Finally");
                }
            }
        }

        [TestMethod]
        public void TestExceptions8FailFast()
        {
            try
            {
                Environment.FailFast("Error");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                // finally - не сработает при FailFast.
                while (true)
                {
                    Debug.WriteLine("Finally");
                }
            }
        }

        [TestMethod]
        public void TestExceptions8NestedExceptions()
        {
            try
            {
                try
                {
                    try
                    {
                        throw new Exception("Exception");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("Catch 1: " + e.Message);
                        throw;
                    }
                    finally
                    {
                        Debug.WriteLine("Finally 1:");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Catch 2: " + e.Message);
                    throw;
                }
                finally
                {
                    Debug.WriteLine("Finally 2:");
                    throw new Exception("New Exception");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Catch 3: " + e.Message);
            }
            finally
            {
                Debug.WriteLine("Finally 3:");
            }
        }

        [TestMethod]
        public void TestExceptions10Null()
        {
            try
            {
                throw null;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /*
        Операторы catch выполняются по порядку их следования в программе. 
        Но при этом выполняется только один блок catch, в котором тип исключения 
        совпадает с типом генерируемого исключения. А все остальные блоки catch пропускаются.
        */
        [TestMethod]
        public void TestExceptions11()
        {
            try
            {
                //throw new Exception("Exception");
                throw new MyExceptionA("MyExceptionA");
                //throw new MyExceptionB("MyExceptionB");
            }
            catch (MyExceptionB e)
            {
                Debug.WriteLine(e.Message);
            }
            catch (MyExceptionA e)
            {
                Debug.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
