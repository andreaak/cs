﻿using CSTest._13_Exceptions._0_Setup;
using NUnit.Framework;
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

    [TestFixture]
    public class _01_Test
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

        [Test]
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
            /*
            Exception thrown: 'System.DivideByZeroException' in CSTest.dll
            Обработка исключения.
            Attempted to divide by zero. 
            */
        }

        [Test]
        public void TestExceptions2ThrowException()
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
            /*
            Exception thrown: 'System.Exception' in CSTest.dll
            Обработка исключения.
            Мое Исключение
            */
        }

        [Test]
        public void TestExceptions3ExceptionProperties()
        {
            try
            {
                ClassWithException instance = new ClassWithException();
                instance.MyMethod();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("GetType(): " + e.GetType());
                Debug.WriteLine("Message: " + e.Message);
                Debug.WriteLine("Имя члена:               {0}", e.TargetSite);
                Debug.WriteLine("Класс определяющий член: {0}", e.TargetSite.DeclaringType);
                Debug.WriteLine("Тип члена:               {0}", e.TargetSite.MemberType);
                Debug.WriteLine("Source:                  " + e.Source);
                Debug.WriteLine("HelpLink:               " + e.HelpLink);
                Debug.WriteLine("StackTrace:                   " + e.StackTrace);

                foreach (DictionaryEntry de in e.Data)
                {
                    Debug.WriteLine("{0} : {1}", de.Key, de.Value);
                }
            }

            /*
            System.ApplicationException: Мое исключение
               at CSTest._13_Exceptions._0_Setup.ClassWithException.MyMethod() in D:\My\cs\CSTest\CSTest\13_Exceptions\0_Setup\ClassWithException.cs:line 16
               at CSTest._13_Exceptions._01_Test.TestExceptions3ExceptionProperties() in D:\My\cs\CSTest\CSTest\13_Exceptions\01_Test.cs:line 96
            GetType(): System.ApplicationException
            Message: Мое исключение
            Имя члена:               Void MyMethod()
            Класс определяющий член: CSTest._13_Exceptions._0_Setup.ClassWithException
            Тип члена:               Method
            Source:                  CSTest
            HelpLink:               http://MyCompany.com/ErrorService
            StackTrace:                      at CSTest._13_Exceptions._0_Setup.ClassWithException.MyMethod() in D:\My\cs\CSTest\CSTest\13_Exceptions\0_Setup\ClassWithException.cs:line 16
               at CSTest._13_Exceptions._01_Test.TestExceptions3ExceptionProperties() in D:\My\cs\CSTest\CSTest\13_Exceptions\01_Test.cs:line 96
            Причина исключения:  : Тестовое исключение
            Время возникновения исключения:  : 06/18/2018 13:04:38
            */
        }

        [Test]
        public void TestExceptions4ThrowUserException()
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
            /*
            Exception thrown: 'CSTest._13_Exceptions._0_Setup.UserException' in CSTest.dll
            Обработка исключения.
            Мое Исключение! 
            */
        }

        [Test]
        public void TestExceptions6InnerException()
        {
            ClassWithException instance = new ClassWithException();
            //instance.CatchInner(); // Попробовать вызвать.
            try
            {
                instance.CatchInner();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception caught: " + exception.Message);
                Debug.WriteLine("Inner Exception : " + exception.InnerException.Message);
            }
            /*
            Exception thrown: 'System.Exception' in CSTest.dll
            Exception thrown: 'System.Exception' in CSTest.dll
            Exception caught: Это внешнее исключение!
            Inner Exception : Это внутреннее исключение!
            */
        }

        [Test]
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
            /*
            Exception thrown: 'System.NullReferenceException' in CSTest.dll
            Object reference not set to an instance of an object.
            */
        }

        /*
        Операторы catch выполняются по порядку их следования в программе. 
        Но при этом выполняется только один блок catch, в котором тип исключения 
        совпадает с типом генерируемого исключения. А все остальные блоки catch пропускаются.
        */
        [Test]
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
            /*
            Exception thrown: 'CSTest._13_Exceptions._0_Setup.MyExceptionA' in CSTest.dll
            MyExceptionA
            */
        }

        [Test]
        public void TestExceptions12()
        {
            try
            {
                try
                {
                    //throw new Exception("Exception");
                    throw new MyExceptionA("MyExceptionA");
                    //throw new MyExceptionB("MyExceptionB");
                }
                catch (MyExceptionB e)
                {
                    throw e;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            /*
            CSTest._13_Exceptions._0_Setup.MyExceptionA: MyExceptionA
                at CSTest._13_Exceptions._01_Test.TestExceptions12() in D:\Projects\My\cs\CSTest.NET\CSTest\13_Exceptions\01_Test.cs:line 232
            */
        }

        [Test]
        public void TestExceptions13()
        {
            try
            {
                try
                {
                    //throw new Exception("Exception");
                    throw new MyExceptionA("MyExceptionA");
                    //throw new MyExceptionB("MyExceptionB");
                }
                catch (MyExceptionB e)
                {
                    throw e;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }



            /*
            CSTest._13_Exceptions._0_Setup.MyExceptionA: MyExceptionA
                at CSTest._13_Exceptions._01_Test.TestExceptions13() in D:\Projects\My\cs\CSTest.NET\CSTest\13_Exceptions\01_Test.cs:line 259
            */
        }
    }
}
