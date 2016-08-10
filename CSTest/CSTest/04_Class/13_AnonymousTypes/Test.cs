using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._04_Class._13_AnonymousTypes
{

    /*
    Анонимные типы предлагают удобный способ инкапсулирования набора свойств в один объект 
    без необходимости предварительного явного определения типа.
    Имя типа создается компилятором и недоступно на уровне исходного кода.
    Анонимные типы являются ссылочными типами, которые происходят непосредственно от класса object.
    Компилятор присваивает им имена, несмотря на то что эти имена недоступны для приложения.
    В анонимных типах можно использовать анонимные типы 
    */

    [TestFixture]
    public class Test
    {
        [Test]
        public void TestClassAnonymous1()
        {
            var instance = new { Name = "Alex", Age = 27 };
            Debug.WriteLine("Name = {0}, Age = {1}", instance.Name, instance.Age);
            //instance.Name = "Test"; //Property or indexer 'AnonymousType#1.Name' cannot be assigned to -- it is read only
            
            Type type = instance.GetType();
            Debug.WriteLine(type.ToString());

            /*
            Name = Alex, Age = 27
            <>f__AnonymousType0`2[System.String,System.Int32]
            */
        }

        /*
        В анонимных типах можно использовать анонимные типы 
        */
        [Test]
        public void TestClassAnonymous2()
        {
            var instance = new { Name = "Alex", Age = 27, Id = new { Number = 123 } };
            Debug.WriteLine("Name = {0}, Age = {1}, Id = {2}", instance.Name, instance.Age, instance.Id.Number);
            /*
            Name = Alex, Age = 27, Id = 123
            */
        }

        // Анонимные типы. (Сильная ссылка)
        [Test]
        public void TestClassAnonymous3()
        {
            var instance = new { My = new MyClass() };

            instance.My.field = 1;
            instance.My.Method();
        }

        // Анонимные типы. (Слабая ссылка)
        [Test]
        public void TestClassAnonymous4()
        {
            new
            {

                My = new MyClass { field = 1 }

            }.My.Method();
        }

        [Test]
        public void TestClassAnonymous5()
        {
            var instance = new
            {
                MyDel = new MyDelegate((string @string) => Debug.WriteLine(@string))
            };

            instance.MyDel("Hello world!");
        }

    }

    delegate void MyDelegate(string @string);

    class MyClass
    {
        public int field;

        public void Method()
        {
            Debug.WriteLine(field);
        }
    }
}
