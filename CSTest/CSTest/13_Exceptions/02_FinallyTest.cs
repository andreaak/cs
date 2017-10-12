using System;
using System.Diagnostics;
using CSTest._13_Exceptions._0_Setup;
using NUnit.Framework;

namespace CSTest._13_Exceptions
{
    [TestFixture]
    public class _02_FinalyTest
    {
        [Test]
        public void TestExceptions5Finally()
        {
            int a = 1, n = 2;

            try
            {
                Debug.WriteLine("Попытка деления на ноль.");
                Debug.WriteLine("a / (2 - n) = {0}", a / (2 - n));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Обработка исключения.");
                Debug.WriteLine(e.Message);
            }
            finally
            {
                Debug.WriteLine("Finally");
                //return; Control cannot leave the body of a finally clause
            }
            /*
            Попытка деления на ноль.
            Exception thrown: 'System.DivideByZeroException' in CSTest.dll
            Обработка исключения.
            Attempted to divide by zero.
            Finally 
            */
        }

        [Test]
        public void TestExceptions7StackOverflowException()
        {
            try
            {
                ClassWithException instance = new ClassWithException();
                Debug.WriteLine("Process Recurse Method");
                instance.RecurseMethod();

                //Если генерировать StackOverflowException то finally сработает
                //throw new StackOverflowException();
            }
            catch (Exception ex)//Не обрабатывает StackOverflowException
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                // finally - не сработает при StackOverflowException.
                Debug.WriteLine("Finally");
            }
            /*
            Process Recurse Method 
            */
        }

        [Test]
        public void TestExceptions8FailFast()
        {
            try
            {
                Debug.WriteLine("Process FailFast");
                Environment.FailFast("Error");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                // finally - не сработает при FailFast.
                Debug.WriteLine("Finally");
            }
            /*
            Process FailFast
            */
        }

        [Test]
        public void TestExceptions8NestedExceptions()
        {
            try
            {
                SomeMethod();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Catch 3: " + e.Message);
            }
            finally
            {
                Debug.WriteLine("Finally 3:");
            }
            /*
            Exception thrown: 'System.Exception' in CSTest.dll
            Catch 1: Exception
            Exception thrown: 'System.Exception' in CSTest.dll
            Finally 1:
            Catch 2: Exception
            Exception thrown: 'System.Exception' in CSTest.dll
            Finally 2:
            Exception thrown: 'System.Exception' in CSTest.dll
            
            
            Catch 3: New Exception
            Finally 3:
            */
        }

        private static void SomeMethod()
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
    }
}
