using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._02_Object
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestObject1()
        {
            TestClass instance = new TestClass(1, 2);
            Debug.WriteLine(instance.ToString());
        }

        [Test]
        public void TestObject2()
        {
            TestClass instance = new TestClass(1, 2);

            Debug.WriteLine(instance.GetHashCode());

        }

        [Test]
        public void TestObject3()
        {
            object obj1 = new object();
            object obj2 = new object();

            Debug.WriteLine(obj1.Equals(obj2));

            obj1 = obj2;

            Debug.WriteLine(obj1.Equals(obj2)); 
        }

        [Test]
        public void TestObject4()
        {
            TestClass a = new TestClass(1, 2);
            TestClass b = new TestClass(1, 2);
            TestClass c = new TestClass(0, 0);

            Debug.WriteLine("a == b : {0}", a.Equals(b));
            Debug.WriteLine("a == c : {0}", a.Equals(c));
        }

        [Test]
        public void TestObject5()
        {
            object obj1 = new object();
            object obj2 = new object();

            Debug.WriteLine(ReferenceEquals(obj1, obj2));

            obj1 = obj2;

            Debug.WriteLine(ReferenceEquals(obj1, obj2));
        }

        [Test]
        public void TestObject6()
        {
            object obj1 = new object();
            object obj2 = new object();

            Debug.WriteLine(Equals(obj1, obj2));

            obj1 = obj2;

            Debug.WriteLine(Equals(obj1, obj2));
        }

        [Test]
        public void TestObject7()
        {
            TestClass a = new TestClass(1, 2);
            TestClass b = new TestClass(1, 2);
            TestClass c = new TestClass(0, 0);

            Debug.WriteLine("a == b : {0}", a.Equals(b));
            Debug.WriteLine("a == c : {0}", a.Equals(c));

            Debug.WriteLine("a == b : {0}", Equals(a, b));
            Debug.WriteLine("a == c : {0}", Equals(a, c));
        }

        [Test]
        public void TestObject8()
        {
            object obj = new object();
            Type type = obj.GetType();
            Debug.WriteLine(type.ToString());
        }

        [Test]
        public void TestObject9Clone()
        {
            MyDerivedClass original = new MyDerivedClass();
            original.age = 42;
            original.name = "Alex";

            //Статические поля и методы наследуются и доступны через имя класса наследника
            Debug.WriteLine(MyDerivedClass.CompanyName);
            MyDerivedClass.Test();

            Debug.WriteLine(original.age + " " + original.name + " " + MyDerivedClass.CompanyName);

            // Клонирование.
            MyDerivedClass clone = original.Clone();
            Debug.WriteLine(clone.age + " " + clone.name + " " + MyDerivedClass.CompanyName + "\n");

            // Проверка. 
            clone.age = 23;
            clone.name = "Konstantin";
            MyBaseClass.CompanyName = "CyberBionic Systematics";

            Debug.WriteLine(original.age + " " + original.name + " " + MyDerivedClass.CompanyName);
            Debug.WriteLine(clone.age + " " + clone.name + " " + MyDerivedClass.CompanyName);

            /*
            Microsoft
            MyBaseClass.Test()
            42 Alex Microsoft
            42 Alex Microsoft

            42 Alex CyberBionic Systematics
            23 Konstantin CyberBionic Systematics
            */
        }

        [Test]
        public void TestObject10Clone()
        {
            Z original = new Z();
            Debug.WriteLine("Оригинал : " + original.a + " " + original.b + " " + original.c + " " + original.aa.a);

            // Клонирование объекта. 
            Z clone = original.Clone();
            Debug.WriteLine("Клон : " + clone.a + " " + clone.b + " " + clone.c + " " + clone.aa.a);

            // Проверка на глубокое клонирование.
            clone.a = clone.b = clone.c = 7;
            clone.aa.a = 8;
            Debug.WriteLine("Оригинал : " + original.a + " " + original.b + " " + original.c + " " + original.aa.a);
            Debug.WriteLine("Клон : " + clone.a + " " + clone.b + " " + clone.c + " " + clone.aa.a);
            /*
            Оригинал : 1 2 3 1
            Клон : 1 2 3 1
            Оригинал : 1 2 3 8
            Клон : 7 7 7 8
             */
        }

        [Test]
        public void TestObject11Clone()
        {
            Z2 original = new Z2();
            Debug.WriteLine("Оригинал : " + original.A.a + " " + original.C.B.b);

            // Клонирование объекта. 
            Z2 clone = original.Clone();
            Debug.WriteLine("Клон : " + clone.A.a + " " + clone.C.B.b + "\n");

            // Проверка на глубокое клонирование.
            clone.A.a = clone.C.B.b = 7;

            Debug.WriteLine("Оригинал : " + original.A.a + " " + original.C.B.b);
            Debug.WriteLine("Клон : " + clone.A.a + " " + clone.C.B.b);
            /*
            Оригинал : 1 2
            Клон : 1 2

            Оригинал : 7 7
            Клон : 7 7
            */
        }

        [Test]
        public void TestObject12()
        {

        }
    }
}
