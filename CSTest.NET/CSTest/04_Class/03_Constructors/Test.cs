using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._04_Class._03_Constructors
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestConstructors1WithoutParameters()
        {
            // Применяем конструктор по умолчанию.
            Point pointA = new Point();
            Debug.WriteLine("pointA.X = {0} pointA.Y = {1}", pointA.X, pointA.Y);
            /*
            InitStaticField Метод
            Статический конструктор
            Конструктор по умолчанию
            pointA.X = 0 pointA.Y = 0
            */
        }

        [Test]
        public void TestConstructors2WithParameter()
        {
            // Применяем конструктор с параметрами.
            Point pointB = new Point(100, 200);
            Debug.WriteLine("pointB.X = {0} pointB.Y = {1}", pointB.X, pointB.Y);
            /*
            InitStaticField Метод
            Статический конструктор
            Пользовательский конструктор
            pointB.X = 100 pointB.Y = 200
            */
        }

        [Test]
        public void TestConstructors3CallSeveralCtors()
        {
            Point point = new Point("A");
            Debug.WriteLine("{0}.X = {1}, {0}.Y = {2}", point.Name, point.X, point.Y);
            /*
            InitStaticField Метод
            Статический конструктор
            Пользовательский конструктор
            Конструктор с одним параметром.
            A.X = 300, A.Y = 400
            */
        }

        [Test]
        public void TestConstructors4Static()
        {
            Point.staticField = 8;
            Debug.WriteLine(Point.staticField);
            /*
            InitStaticField Метод
            Статический конструктор
            8
            */
        }
    }
}
