using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._04_Class._03_Constructors
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestConstructors1()
        {
            // Применяем конструктор по умолчанию.
            Point pointA = new Point();
            Debug.WriteLine("pointA.X = {0} pointA.Y = {1}", pointA.X, pointA.Y);

            Debug.WriteLine(new string('-', 30));

            // Применяем конструктор с параметрами.
            Point pointB = new Point(100, 200);
            Debug.WriteLine("pointB.X = {0} pointB.Y = {1}", pointB.X, pointB.Y);
        }

        [TestMethod]
        public void TestConstructors2()
        {
            Point point = new Point("A");
            Debug.WriteLine("{0}.X = {1}, {0}.Y = {2}", point.Name, point.X, point.Y);
        }
    }
}
