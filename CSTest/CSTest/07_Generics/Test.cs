using CSTest._07_Generics._0_SetUp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._07_Generics
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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
        }

        [TestMethod]
        public void TestGeneric2()
        {
            GenericClass<int> instance1 = new GenericClass<int>(1);
            Debug.WriteLine(instance1.Field);

            GenericClass<string> instance2 = new GenericClass<string>("Number ");
            Debug.WriteLine(instance2.Field);
        }

        [TestMethod]
        public void TestGeneric3Method()
        {
            _02_GenericMethod instance = new _02_GenericMethod();

            instance.Method<string>("Hello world!");

            instance.Method("Привет мир!");

        }

        [TestMethod]
        public void TestGeneric4Delegate()
        {
            GenericDelegate<int, int> myDelegate1 =
                new GenericDelegate<int, int>(_03_GenericDelegateClass.Add);
            int i = myDelegate1.Invoke(1);
            Debug.WriteLine(i);

            GenericDelegate<string, string> myDelegate2 = 
                new GenericDelegate<string, string>(_03_GenericDelegateClass.Concatenation);
            string s = myDelegate2("Alex");
            Debug.WriteLine(s);
        }
        
        [TestMethod]
        public void TestGeneric5Interface()
        {
            Circle circle = new Circle();
            IContainer<Circle> container = new Container<Circle>(circle);
            Debug.WriteLine(container.Figure.ToString());

            Circle circle2 = new Circle();
            IContainer<Shape> container2 = new Container<Shape>(circle2);
            Debug.WriteLine(container.Figure.ToString());
        }

        [TestMethod]
        public void TestGeneric6CovarianceInterface()
        {
            Circle circle = new Circle();
            IContainerCovariance<Shape> container = new ContainerCovariance<Circle>(circle);
            Debug.WriteLine(container.Figure.ToString());
        }

        [TestMethod]
        public void TestGeneric7ContrvarianceInterface()
        {
            Shape shape = new Circle();
            IContainerContrvariance<Circle> container = new ContainerContrvariance<Shape>(shape);
            Debug.WriteLine(container.ToString());
        }

        [TestMethod]
        public void TestGeneric8CovarianceDelegate()
        {
            CovarianceDelegate<Cat> delegateCat = new CovarianceDelegate<Cat>(_07_DelegateCovariance.CatCreator);
            CovarianceDelegate<Animal> delegateAnimal = delegateCat;    // От производного к базовому.                      
            Animal animal = delegateAnimal.Invoke();
            Debug.WriteLine(animal.GetType().Name);
        }


        [TestMethod]
        public void TestGeneric9ContrvarianceDelegate()
        {
            ContrvarianceDelegate<Animal> delegateAnimal = new ContrvarianceDelegate<Animal>(_08_DelegateContrvariance.CatUser);
            ContrvarianceDelegate<Cat> delegateCat = delegateAnimal;    // От базового к производному.

            delegateAnimal(new Animal());
            delegateCat(new Cat());

            //delegateCat(new Animal()); // Невозможно.
        }

        [TestMethod]
        public void TestGeneric10Constraints()
        {
            GenericClassWithConstraint<string> instance1 = new GenericClassWithConstraint<string>();
            //GenericClassWithConstraint<int> instance1 = new GenericClassWithConstraint<int>();                // Ошибка.    int - структурный тип.

            GenericStructWithConstraint<int> instance2 = new GenericStructWithConstraint<int>();
            //GenericStructWithConstraint<string> instance2 = new GenericStructWithConstraint<string>();          // Ошибка.    string - ссылочный тип.

            GenericClassWithBaseConstraint<Base> mc1 = new GenericClassWithBaseConstraint<Base>();
            GenericClassWithBaseConstraint<Derived> mc2 = new GenericClassWithBaseConstraint<Derived>();
            //GenericClassWithBaseConstraint<string> mc3 = new GenericClassWithBaseConstraint<string>();     // Ошибка.
        }

        [TestMethod]
        public void TestGeneric11Constraints()
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

        [TestMethod]
        public void TestGeneric12Constraints()
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
