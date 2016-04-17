using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using CSTest._01_Elements_CS._01_Types._0_Setup;

namespace CSTest._01_Elements_CS._01_Types
{
    /*
    В C# 4.0 появился новый тип — dynamic. Тип является статическим типом, 
    но объект типа dynamic обходит проверку статического типа. 
    В большинстве случаев он функционирует, как тип object. 
    Во время компиляции предполагается, что элементы с типом dynamic поддерживают любые операции. 
    Поэтому разработчику не нужно следить за тем, откуда объект получает свое значение. 
    В отличие от ключевого слова var, объект, объявленный как dynamic, может менять тип во время выполнения. 
    При использовании ключевого слова var определение типа объекта откладывается. 
    Но как только он определен компилятором, изменять его уже нельзя. 
    Что касается объекта dynamic, то можно не просто изменить его тип, но делать это многократно. 
    Это отличается от приведения объекта от одного типа к другому. 
    При приведении объекта создается новый объект с другим, но совместимым типом. 
    */
    [TestClass]
    public class _07_Dynamic
    {
        // Динамические типы данных. (Локальные переменные)
        [TestMethod]
        public void TestDynamic1()
        {
            dynamic variable = 1;

            Console.WriteLine(variable);

            variable = "Hello world!";

            Console.WriteLine(variable);

            variable = DateTime.Now;

            Console.WriteLine(variable);

            Console.WriteLine((default(dynamic) == null) ? "Null" : "????");//Null

            dynamic calculator = new DynamicClass();

            Console.WriteLine(calculator.Add(2, 3));
            Console.WriteLine(calculator.Add("1", 2));//concatenation
        }

        // Динамические типы данных. (Статические поля)
        static dynamic field = 1;
        [TestMethod]
        public void TestDynamic2()
        {
            Console.WriteLine(field);

            field = "Hello world!";

            Console.WriteLine(field);

            field = DateTime.Now;

            Console.WriteLine(field);
        }

        // Динамические типы данных. (Нестатические поля)
        dynamic field1 = 1, field2 = "Hello", field3 = true;
        [TestMethod]
        public void TestDynamic3()
        {
            dynamic instance = new _07_Dynamic();

            Console.WriteLine(instance.field1);

            instance.field1 = "Hello world!";

            Console.WriteLine(instance.field1);

            instance.field1 = DateTime.Now;

            Console.WriteLine(instance.field1);
        }

        // Динамические типы данных. (Динамические типы аргументов и возвращаемых значений методов.)
        [TestMethod]
        public void TestDynamic4()
        {
            string @string = DynamicClass.StaticMethod("friend");

            Console.WriteLine(@string);
        }

        //DynamicClass
        [TestMethod]
        public void TestDynamic5()
        {
            dynamic my = new DynamicClass("Hello");

            Console.WriteLine(my.Field);

            string variable = my.Method("World");

            Console.WriteLine(variable);

            my[0] = "Zero";
            my[1] = "One";
            my[2] = "Two";

            for (dynamic i = 0; i < 3; i++)
            {
                Console.WriteLine(my[i]);
            }
        }

        // Динамические типы данных. (Динамические типы в делегатах)
        [TestMethod]
        public void TestDynamic6()
        {
            dynamic myDelegate = new DynamicDelegate(DynamicClass.StaticMethod);

            dynamic @string = myDelegate("Hello world!");

            Console.WriteLine(@string);
        }

        // Динамические типы данных. (Динамические типы в параметризированных делегатах)
        [TestMethod]
        public void TestDynamic7()
        {
            dynamic myDelegate = new DynamicGenericDelegate<dynamic, dynamic>(DynamicClass.StaticMethod);

            dynamic @string = myDelegate("Hello world!");

            Console.WriteLine(@string);
        }

        // Динамические типы данных. (События) 
        [TestMethod]
        public void TestDynamic8()
        {
            dynamic my = new DynamicClass();
            my.MyEvent += new EventDelegate(Handler);
            //Console.WriteLine();
            my.Method("user", "mouse");
        }

        // Динамические типы данных. (Наследование)
        [TestMethod]
        public void TestDynamic10()
        {
            dynamic instance = new Derived();

            // Динамические поля должны быть проинициализированны перед использованием.
            instance.field = "Hello";

            Console.WriteLine(instance.Method());
        }

        [TestMethod]
        public void TestDynamic11()
        {
            dynamic instance = DynamicClass.FactoryMethod() as dynamic;

            if (false)
            {
                instance.Method(7);                  // Вызов метода.
                instance.field = instance.Property;  // Получение и установка значений полей и свойств.
                instance["one"] = instance[2];       // Получение и установка значений через индексаторы.
                dynamic variable = instance + 3;     // Вызов операторов.
                variable = instance(5, 7);           // Вызов делегатов.
            }
        }

        // Динамические типы данных. (Анонимные типы)
        [TestMethod]
        public void TestDynamic12()
        {
            dynamic instance = new { Name = "Alex", Age = 18 };

            Console.WriteLine(instance.Name);
            Console.WriteLine(instance.Age);
        }

        // Динамические типы данных. (Динамические типы аргументов и возвращаемых значений методов.)
        [TestMethod]
        public void TestDynamic13()
        {
            dynamic variable1 = 0, variable2;

            DynamicClass.RefOutMethod(ref variable1, out variable2);

            Console.WriteLine(variable1);
            Console.WriteLine(variable2);
        }

        //Перегрузка операторов
        [TestMethod]
        public void TestDynamic14()
        {
            dynamic a = new DynamicClass(1), b = new DynamicClass(2), c = a + b;

            Console.WriteLine(c);
        }

        // Динамические типы данных. (Анонимные типы)
        [TestMethod]
        public void TestDynamic15()
        {
            foreach (dynamic item in DynamicClass.Generator())
            {
                Console.WriteLine("Key = {0}, Value = {1}", item.Key, item.Value);
            }
        }

        static dynamic Handler(dynamic sender, dynamic e)
        {
            Console.WriteLine("sender = {0}, e = {1}", sender, e);
            return default(dynamic);
        }
    }
}
