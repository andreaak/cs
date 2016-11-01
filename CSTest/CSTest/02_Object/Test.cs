using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CSTest._02_Object
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestObject1ToString()
        {
            TestClass instance = new TestClass(1, 2);
            Debug.WriteLine(instance.ToString());
            /*
            TestClass x = 1; y = 2
            */
        }

        [Test]
        public void TestObject2GetHashCode()
        {
            TestClass instance = new TestClass(1, 2);

            Debug.WriteLine(instance.GetHashCode());
            /*
            3
            */
        }

        [Test]
        public void TestObject3Equals()
        {
            object obj1 = new object();
            object obj2 = new object();

            Debug.WriteLine(obj1.Equals(obj2));

            obj1 = obj2;

            Debug.WriteLine(obj1.Equals(obj2));

            /*
            False
            True
            */
        }

        [Test]
        public void TestObject4Equals()
        {
            TestClass a = new TestClass(1, 2);
            TestClass b = new TestClass(1, 2);
            TestClass c = new TestClass(0, 0);

            Debug.WriteLine("a == b : {0}", a.Equals(b));
            Debug.WriteLine("a == c : {0}", a.Equals(c));

            /*
            a == b : True
            a == c : False
            */
        }

        [Test]
        public void TestObject5ReferenceEquals()
        {
            object obj1 = new object();
            object obj2 = new object();

            Debug.WriteLine(ReferenceEquals(obj1, obj2));

            obj1 = obj2;

            Debug.WriteLine(ReferenceEquals(obj1, obj2));

            /*
            False
            True
            */
        }

        [Test]
        public void TestObject6StaticEquals()
        {
            object obj1 = new object();
            object obj2 = new object();

            Debug.WriteLine(Equals(obj1, obj2));

            obj1 = obj2;

            Debug.WriteLine(Equals(obj1, obj2));

            /*
            False
            True
            */
        }

        [Test]
        public void TestObject7EqualsVsStaticEquals()
        {
            TestClass a = new TestClass(1, 2);
            TestClass b = new TestClass(1, 2);
            TestClass c = new TestClass(0, 0);

            Debug.WriteLine("a == b : {0}", a.Equals(b));
            Debug.WriteLine("a == c : {0}", a.Equals(c));

            Debug.WriteLine("a == b : {0}", Equals(a, b));
            Debug.WriteLine("a == c : {0}", Equals(a, c));

            /*
            a == b : True
            a == c : False
            a == b : True
            a == c : False
            */
        }

        [Test]
        public void TestObject8GetType()
        {
            object obj = new object();
            Type type = obj.GetType();
            Debug.WriteLine(type.ToString());
            /*
            System.Object
            */
        }

        [Test]
        public void TestObject9GetType()
        {
            BaseClass baseInstance = new BaseClass();
            DerivedClass instance = new DerivedClass();
            /*
            This is a CSTest._02_Object.MyBaseClass,
            This is a CSTest._02_Object.MyDerivedClass,
            */
        }

        [Test]
        public void TestObject10Clone()
        {
            DerivedClass original = new DerivedClass();
            original.age = 42;
            original.name = "Alex";

            Debug.WriteLine(original.age + " " + original.name);

            // Клонирование.
            DerivedClass clone = original.Clone();
            Debug.WriteLine(clone.age + " " + clone.name);

            // Проверка. 
            clone.age = 23;
            clone.name = "Konstantin";

            Debug.WriteLine(original.age + " " + original.name);
            Debug.WriteLine(clone.age + " " + clone.name);

            /*
            This is a CSTest._02_Object.DerivedClass,
            42 Alex
            42 Alex
            42 Alex
            23 Konstantin
            */
        }

        [Test]
        public void TestObject11Clone()
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
        public void TestObject12Clone()
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
    }
}
