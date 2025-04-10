using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSTest._04_Class._17_OperatorOverload._0_Setup;
using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._04_Class._17_OperatorOverload
{

    [TestFixture]
    public class _07_CastOperatorsTest
    {
        [Test]
        public void TestCastOperators1Implicit()
        {
            _07_CastOperatorsSetup a = new _07_CastOperatorsSetup(1, 2, 3);
            _07_CastOperatorsSetup b = new _07_CastOperatorsSetup(10, 10, 10);

            Debug.Write("Координаты точки а: ");
            a.Show();

            Debug.Write("Координаты точки b: ");
            b.Show();

            int i = a; // преобразовать в тип int 
            Debug.WriteLine("Результат присваивания i = a: " + i);

            i = a * 2 - b; // преобразовать в тип int 
            Debug.WriteLine("Результат вычисления выражения a * 2 - b: " + i);

            /*
            Координаты точки а: 1, 2, 3
            Координаты точки b: 10, 10, 10
            Результат присваивания i = a: 6
            Результат вычисления выражения a * 2 - b: -988
            */
        }

        [Test]
        public void TestCastOperators1Explicit()
        {
            _07_CastOperatorsSetup a = new _07_CastOperatorsSetup(1, 2, 3);
            _07_CastOperatorsSetup b = new _07_CastOperatorsSetup(10, 10, 10);

            Debug.Write("Координаты точки а: ");
            a.Show();

            Debug.Write("Координаты точки b: ");
            b.Show();

            short i = (short)a; // преобразовать в тип short явно, 
                                // поскольку указано приведение типов
            Debug.WriteLine("Результат присваивания i = a: " + i);

            i = (short)((short)a * 2 - (short)b); // явно требуется приведение типов 
            Debug.WriteLine("Результат вычисления выражения a * 2 - b: " + i);

            /*
            Координаты точки а: 1, 2, 3
            Координаты точки b: 10, 10, 10
            Результат присваивания i = a: 6
            Результат вычисления выражения a * 2 - b: -988
            */
        }
    }
}
