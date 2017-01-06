using CSTest._07_Generics._0_Setup;
using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._07_Generics
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestGeneric1()
        {
            // Создаем экземпляр класса MyClass и в качестве параметра типа (тип MyClass) передаем тип int.
            GenericClass<int> instance1 = new GenericClass<int>();
            instance1.Method();

            // Создаем экземпляр класса MyClass и в качестве параметра типа (тип MyClass) передаем тип long.
            GenericClass<long> instance2 = new GenericClass<long>();
            instance2.Method();

            // Создаем экземпляр класса MyClass и в качестве параметра типа (тип MyClass) передаем тип string.
            GenericClass<string> instance3 = new GenericClass<string>();
            instance3.field = "ABC";
            instance3.Method();

            GenericClass<int> instance4 = new GenericClass<int>();
            instance4.Method();

            Debug.WriteLine("instance1 == instance2 - " + (instance1.GetType() == instance2.GetType()));
            Debug.WriteLine("instance1 == instance3 - " + (instance1.GetType() == instance3.GetType()));
            Debug.WriteLine("instance1 == instance4 - " + (instance1.GetType() == instance4.GetType()));

            /*
            System.Int32
            System.Int64
            System.String
            System.Int32
            instance1 == instance2 - False
            instance1 == instance3 - False
            instance1 == instance4 - True 
             */
        }

        [Test]
        public void TestGeneric2()
        {
            GenericClass<int> instance1 = new GenericClass<int>(1);
            Debug.WriteLine(instance1.Field);

            GenericClass<string> instance2 = new GenericClass<string>("Number ");
            Debug.WriteLine(instance2.Field);

            /*
            1
            Number
            */
        }

        [Test]
        public void TestGeneric3Method()
        {
            _02_GenericMethod instance = new _02_GenericMethod();

            instance.Method<string>("Hello world!");

            instance.Method("Привет мир!");
        }

        [Test]
        public void TestGeneric4Delegate()
        {
            GenericDelegate<int, int> myDelegate1 =
                new GenericDelegate<int, int>(_03_GenericDelegate.Add);
            int i = myDelegate1.Invoke(1);
            Debug.WriteLine(i);

            GenericDelegate<string, string> myDelegate2 =
                new GenericDelegate<string, string>(_03_GenericDelegate.Concatenation);
            string s = myDelegate2("Alex");
            Debug.WriteLine(s);
        }

        [Test]
        public void TestGeneric5Interface()
        {
            Circle circle = new Circle();
            IContainer<Circle> container = new Container<Circle>(circle);
            Debug.WriteLine(container.Figure.ToString());

            Circle circle2 = new Circle();
            IContainer<Shape> container2 = new Container<Shape>(circle2);
            Debug.WriteLine(container.Figure.ToString());
        }
    }
}
