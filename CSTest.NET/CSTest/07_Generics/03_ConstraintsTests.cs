using CSTest._07_Generics._0_Setup;
using CSTest._07_Generics._0_Setup.Constraints;
using NUnit.Framework;

namespace CSTest._07_Generics
{
    [TestFixture]
    public class _03_ConstraintsTests
    {
        [Test]
        public void TestGenericConstraints1FirstLevel()
        {
            GenericClassWithConstraint<string> instance1 = new GenericClassWithConstraint<string>();
            //GenericClassWithConstraint<int> instance1 = new GenericClassWithConstraint<int>(); // Ошибка.    int - структурный тип.

            GenericStructWithConstraint<int> instance2 = new GenericStructWithConstraint<int>();
            //GenericStructWithConstraint<string> instance2 = new GenericStructWithConstraint<string>(); // Ошибка.    string - ссылочный тип.

            GenericClassWithBaseConstraint<Base> mc1 = new GenericClassWithBaseConstraint<Base>();
            GenericClassWithBaseConstraint<Derived> mc2 = new GenericClassWithBaseConstraint<Derived>();
            //GenericClassWithBaseConstraint<string> mc3 = new GenericClassWithBaseConstraint<string>(); // Ошибка.
        }

        [Test]
        public void TestGenericConstraints2SecondLevel()
        {
            // В качестве аргумента типа подходит Derived, т.к., он наследуется от обоих интерфейсов.
            GenericInterfaceConstraint<DerivedInterfaceConstraints> my1 =
                new GenericInterfaceConstraint<DerivedInterfaceConstraints>();
            // GenericInterfaceConstraint<IInterfaceConstraint> my1 =
            //new GenericInterfaceConstraint<IInterfaceConstraint>();   // Ошибка.
            // GenericInterfaceConstraint<IInterfaceConstraint<object>> my1 =
            //new GenericInterfaceConstraint<IInterfaceConstraint<object>>();   // Ошибка.

            GenericInterfaceConstraint2<IInterfaceConstraint> my2 =
                new GenericInterfaceConstraint2<IInterfaceConstraint>();
            GenericInterfaceConstraint2<DerivedInterfaceConstraints> my3 =
                new GenericInterfaceConstraint2<DerivedInterfaceConstraints>();

            GenericInterfaceConstraint3<IInterfaceConstraint<object>> my4 =
                new GenericInterfaceConstraint3<IInterfaceConstraint<object>>();
            GenericInterfaceConstraint3<DerivedInterfaceConstraints> my5 =
                new GenericInterfaceConstraint3<DerivedInterfaceConstraints>();

            // В качестве аргумента типа подходит Derived, т.к., он наследуется от обоих интерфейсов.
            GenericInterfaceConstraint4<DerivedInterfaceConstraints2> my6 =
                new GenericInterfaceConstraint4<DerivedInterfaceConstraints2>();
            //GenericInterfaceConstraint4<IInterfaceConstraint2> my1 = 
            //new GenericInterfaceConstraint4<IInterfaceConstraint2>();   // Ошибка.

            //  Аргумент типа подходит, т.к., IInterfaceConstraint2<object> наследуется от IInterfaceConstraint2
            GenericInterfaceConstraint4<IInterfaceConstraint2<object>> my7 =
                new GenericInterfaceConstraint4<IInterfaceConstraint2<object>>();

            GenericInterfaceConstraint5<DerivedInterfaceConstraints2> my8 =
                new GenericInterfaceConstraint5<DerivedInterfaceConstraints2>();
            GenericInterfaceConstraint5<DerivedInterfaceConstraints3> my9 =
                new GenericInterfaceConstraint5<DerivedInterfaceConstraints3>();
            GenericInterfaceConstraint5<IInterfaceConstraint2<object>> my10 =
                new GenericInterfaceConstraint5<IInterfaceConstraint2<object>>();

            GenericNakedConstraint<int, object, int> my11 =
                new GenericNakedConstraint<int, object, int>();

            GenericNakedConstraint<string, object, string> my12 =
                new GenericNakedConstraint<string, object, string>();

            // Не совпадают первый и третий аргументы типов - T и U (string и int).
            //MyClass<string, Object, int> my2 = new MyClass<string, Object, int>();  // ОШИБКА!  
        }

        [Test]
        public void TestGenericConstraints2ThirdLevel()
        {
            GenericWithConstraintNew<_11_ThirdLevelConstraints> foo = new GenericWithConstraintNew<_11_ThirdLevelConstraints>();
            foo.instance.MyIntProperty = 1;
            foo.instance.MyStringProperty = "Hello World!";
            foo.GetValues();

            // Нельзя использовать с ограничением new абстрактные классы
            // Нельзя вызывать конструктор абстракт класса напрямую
            //GenericWithConstraintNew<_11_AbstractClass> foo2 = new GenericWithConstraintNew<_11_AbstractClass>();
        }
    }
}
