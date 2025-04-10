using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._04_Class._16_Polymorfizm
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestPolymorfizm1()
        {
            BaseClass instance = new DerivedClass();
            instance.DoWork();
            /*
            Derived.DoWork()
            */
        }

        
        [Test]
        public void TestPolymorfizm2NVI()
        {
            BaseClass instance = new DerivedClass();
            instance.NonVirtualInterface();
            /*
            Base.PreDoWork();
            Derived.CoreDoWork()
            */
        }

        [Test]
        public void TestPolymorfizm3()
        {
            BaseClass instance = new DerivedClass();
            instance.SomeMetod1();

            DerivedClass ins2 = instance as DerivedClass;
            ins2.SomeMetod1();
            /*
            Base.1
            Derived.1
            */
        }

        [Test]
        public void TestPolymorfizm4()
        {
            BaseClass instance = new DerivedClass();

            instance.SomeMetod3();
            instance.SomeMetod4();
            /*
            Base.3
            Derived.4
            */
        }

        [Test]
        public void TestPolymorfizm5()
        {
            BaseClass instance = new DerivedClass();

            instance.SomeMetod3();
            instance.SomeMetod4();
            /*
            Base.3
            Derived.4
            */
            instance.BaseClassMethod();
            /*
            Base.3
            Derived.4
            */
        }

        [Test]
        public void TestPolymorfizm6()
        {
            DerivedClass instance = new DerivedClass();

            instance.SomeMetod3();
            instance.SomeMetod4();
            /*
            Derived.3
            Derived.4
            */
        }

        [Test]
        public void TestPolymorfizm7()
        {
            DerivedFromDerivedClass c3 = new DerivedFromDerivedClass();
            c3.SomeMetod3();
            c3.SomeMetod4();
            /*
            Derived.3
            Derived.4
            */
        }

        [Test]
        public void TestPolymorfizm8()
        {
            BaseClass baseClass = new BaseClass();
            baseClass.SomeVirtualMethods();
            Debug.WriteLine("");
            baseClass = new DerivedClass();
            baseClass.SomeVirtualMethods();
            Debug.WriteLine("");
            DerivedClass derived = new DerivedClass();
            derived.SomeVirtualMethods();
            /*
            Base.SomeVirtualMethods
            Base.3
            Base.4

            Derived.SomeVirtualMethods
            Base.3
            Base.4
            Derived.4
            Derived.3
            Derived.4

            Derived.SomeVirtualMethods
            Base.3
            Base.4
            Derived.4
            Derived.3
            Derived.4
            */
        }
    }
}
