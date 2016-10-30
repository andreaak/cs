using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._04_Class._07_Inheritance
{
    // Наследование. 

    // Наследование — механизм объектно-ориентированного программирования, позволяющий описать новый (производный) класс
    // на основе уже существующего (базового),
    // при этом свойства и функциональность базового класса заимствуются новым производным классом.

    //        Базовый класс             -                Производный  класс
    //        Супер класс               -                Подкласс или (сабкласс)
    //        Родительский класс        -                Дочерний класс
    //        Родитель                  -                Потомок

    [TestFixture]
    public class Test
    {
        [Test]
        public void TestInheritance1()
        {
            DerivedClass instance = new DerivedClass();

            Debug.WriteLine(instance.publicField);
            Debug.WriteLine(instance.Test());

            /*
            DerivedClass.publicField
            5        
            */
        }

        [Test]
        public void TestInheritance2()
        {
            DerivedClass instance = new DerivedClass();

            Debug.WriteLine(instance.publicField);
            instance.Show();
            /*
            DerivedClass.publicField
            BaseClass.privateField
            */
        }

        [Test]
        public void TestInheritance3()
        {
            DerivedClass instance = new DerivedClass(1, 2);

            Debug.WriteLine(instance.baseNumber);
            Debug.WriteLine(instance.derivedField);

            /*
            1
            2
            */
        }

        [Test]
        public void TestInheritance4()
        {
            DerivedClass instance = new DerivedClass(1, 2, "");

            Debug.WriteLine(instance.baseNumber);
            Debug.WriteLine(instance.derivedField);
            /*
            1
            2
            */
        }

        [Test]
        public void TestInheritance5()
        {
            DerivedClass instance = new DerivedClass();

            instance.field1 = 1;
            instance.field2 = 2;
            instance.field3 = 3;

            instance.field4 = 4;
            instance.field5 = 5;

            // Приведение экземпляра класса DerivedClass к базовому типу BaseClass.
            BaseClass newInstance = instance; // Upcast

            Debug.WriteLine(newInstance.field1);
            Debug.WriteLine(newInstance.field2);
            Debug.WriteLine(newInstance.field3);

            //fields not accessable
            //Debug.WriteLine(newInstance.field4);
            //Debug.WriteLine(newInstance.field5);


            // Проверка.
            Debug.WriteLine("instance Id     {0}", instance.GetHashCode());
            Debug.WriteLine("newInstance Id  {0}", newInstance.GetHashCode());
            /*
            1
            2
            3
            instance Id     26376641
            newInstance Id  26376641
            */
        }

        [Test]
        public void TestInheritance6()
        {
            DerivedClass instance = new DerivedClass();
            instance.Method();

            // UpCast
            BaseClass instanceUp = instance;
            instanceUp.Method();

            // DownCast
            DerivedClass instanceDown = (DerivedClass)instanceUp;
            instanceDown.Method();
            /*
            Method from DerivedClass
            Method from BaseClass
            Method from DerivedClass
            */
        }

        [Test]
        public void TestInheritance7()
        {
            DerivedClass instance = new DerivedClass();
            instance.MethodVirtual();

            // UpCast
            BaseClass instanceUp = instance;
            instanceUp.MethodVirtual();

            // DownCast
            DerivedClass instanceDown = (DerivedClass)instanceUp;
            instanceDown.MethodVirtual();
            /*
            MethodVirtual from DerivedClass
            MethodVirtual from DerivedClass
            MethodVirtual from DerivedClass
            */
        }

        [Test]
        public void TestInheritance8()
        {
            DerivedClass instance = new DerivedClass();
            instance.MethodVirtual2();
            /*
            MethodVirtual2 from BaseClass
            MethodVirtual2 from DerivedClass
            */
        }

        [Test]
        public void TestInheritance9CallingOrder()
        {
            _01_ConstructorsDerived instance = new _01_ConstructorsDerived();
            /*
            Derived.InitStaticVariable
            Derived.StaticCtor
            Derived.InitInstanceVariable
            Base.InitStaticVariable
            Base.StaticCtor
            Base.InitInstanceVariable
            Base.Ctor
            Derived.VirtualMethod
            Derived.Ctor
            */
        }
    }
}