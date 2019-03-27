using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._01_Elements_CS._02_Variables
{
    [TestFixture]
    public class _05_var
    {
        [Test]
        public void TestVar1()
        {
            // Неявно типизированная локальная переменная.
            var myVar = 7;
            Debug.WriteLine(myVar);

            // Следующий оператор не может быть скомпилирован,
            // поскольку переменная myVar имеет тип int и ей нельзя присвоить десятичное значение.
            //myVar = 12.2m; // Ошибка!

            // Неявно типизированные локальные переменные не допускают множественного объявления.
            // var a = 1, b = 2, c = 3;

            // Неявно типизированные локальные переменные должны быть инициализированы.
            // var a; 

            // Константа не может быть неявно типизированная.
            // const var a = 3.14; 

            bool res = false;
            //Type of conditional expression cannot be determined because there is no implicit conversion between
            //var a = res ? new A() : new B();

            var b = res ? new A() : new C();
            Debug.WriteLine(b.GetType());
            b = new C();
            Debug.WriteLine(b.GetType());

            //class A reference
            //b.Test();

            //Type of conditional expression cannot be determined because there is no implicit conversion between 
            //'CSTest._01_Elements_CS._02_Variables._05_var.C' and 'CSTest._01_Elements_CS._02_Variables._05_var.D'
            //var c = res ? new C() : new D();

            /*
            7
            CSTest._01_Elements_CS._02_Variables.C
            CSTest._01_Elements_CS._02_Variables.C
            */
        }
    }

    public class A
    { }

    public class B
    { }

    public class C : A
    {
        public void Test()
        { }
    }

    public class D : A
    { }
}
