using System;
using System.Diagnostics;

namespace CSTest._13_Exceptions._0_Setup
{
    class ClassWithException
    {
        public void MyMethod()
        {
            Exception exception = new ApplicationException("Мое исключение");

            exception.HelpLink = "http://MyCompany.com/ErrorService";
            exception.Data.Add("Причина исключения: ", "Тестовое исключение");
            exception.Data.Add("Время возникновения исключения: ", DateTime.Now);

            throw exception;
        }

        public void ThrowInner()
        {
            throw new Exception("Это внутреннее исключение!");
        }

        public void CatchInner()
        {
            try
            {
                this.ThrowInner();
            }
            catch (Exception e)
            {
                throw new Exception("Это внешнее исключение!", e);
            }
        }

        public void RecurseMethod()
        {
            int[] arr = new int[10];
            //Debug.WriteLine(arr);
            RecurseMethod();
        }
    }

    // Для создания пользовательского исключения, требуется наследование от System.Exception.
    class UserException : Exception
    {
        public void Method()
        {
            Debug.WriteLine("Мое Исключение!");
        }
    }

    class MyExceptionA : Exception
    {
        public MyExceptionA(string message)
            : base(message)
        {
        }
    }

    class MyExceptionB : MyExceptionA
    {
        public MyExceptionB(string message)
            : base(message)
        {
        }
    }
}
