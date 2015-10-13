using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._06_Interface
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestInterface1()
        {
            _01_Base my = new _01_Base();

            my.Method();
        }

        [TestMethod]
        public void TestInterface2()
        {
            Interface1 instance1 = new _02_DerivedClass();
            Interface2 instance2 = new _02_DerivedClass();

            instance1.Method1();
            // instance1.Method2();

            instance2.Method2();
            // instance2.Method1();
        }

        [TestMethod]
        public void TestInterface3()
        {
            // Явное указание имени интерфейса в имени метода.
            _02_DerivedClass instance = new _02_DerivedClass();

            //instance. -- // На экземпляре не видим методов интерфейсов.

            // Приведем экземпляр класса DerivedClass - instance, к базовому интерфейсному типу Interface1

            Interface1 instance1 = instance;
            instance1.Method();

            Interface2 instance2 = instance;
            instance2.Method();
        }

        // Множественное наследование от одного класса и нескольких интерфейсов.
        [TestMethod]
        public void TestInterface4()
        {
            _02_DerivedClass instance = new _02_DerivedClass();
            instance.Method();
            instance.Method1();
            instance.Method2();

            Debug.WriteLine(new string('-', 40));

            BaseClass instance0 = instance;
            instance0.Method();

            Interface1 instance1 = instance;
            instance1.Method1();

            Interface2 instance2 = instance;
            instance2.Method2();
        }

        [TestMethod]
        public void TestInterface5()
        {
            _03_InterfaceInheritance instance = new _03_InterfaceInheritance();
            instance.Method1();
            instance.Method2();

            IInterface1 instance1 = instance;
            instance1.Method1();

            IInterface2 instance2 = instance;
            instance2.Method1();
            instance2.Method2();
        }

        [TestMethod]
        public void TestInterface6()
        {
            _04_InterfaceInheritance instance = new _04_InterfaceInheritance();
            
            instance.Method();

            IInterface3 instance1 = instance;
            instance1.Method();

            IInterface4 instance2 = instance;
            instance2.Method();

            /*Method - реализация _04_InterfaceInheritance
                Method - реализация интерфейса IInterface3
                Method - реализация интерфейса IInterface4
             */
        }

        [TestMethod]
        public void TestInterface7()
        {
            _05_InterfaceUnion instance = new _05_InterfaceUnion();
            instance.Method();

            IInterface5 instance1 = instance;
            instance1.Method();

            IInterface6 instance2 = instance;
            instance2.Method();
        }

        [TestMethod]
        public void TestInterface8()
        {
            _06_AbstractClass instance = new _06_AbstractClass();
            instance.Method();
            instance.Method2();
        }

        [TestMethod]
        public void TestInterface9()
        {
            _07_InterfaceWithBaseClass instance = new _07_InterfaceWithBaseClass();
            instance.Method();

            IInterface7 instance1 = instance;
            instance1.Method();
        }

        [TestMethod]
        public void TestInterface10()
        {
            _07_InterfaceWithBaseClass instance = new _07_InterfaceWithBaseClass();
            instance.Method2();

            IInterface8 instance1 = instance;
            instance1.Method2();

            instance1.Method3();
            /*
            DerivedClass.Method2()
            BaseClass.Method2()
            DerivedClass.Method3()
             */
        }

        [TestMethod]
        public void TestInterface11()
        {
            _08_InterfaceWithBaseClass instance = new _08_InterfaceWithBaseClass();
            instance.Method2();

            IInterface8 instance1 = instance;
            instance1.Method2();

            instance1.Method3();
            /*
            DerivedClass.Method2()
            DerivedClass.Method2()
            DerivedClass.Method3()
             */
        }
    }
}
