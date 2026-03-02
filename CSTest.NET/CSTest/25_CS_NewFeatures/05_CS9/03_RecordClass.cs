using NUnit.Framework;
using System.Diagnostics;


namespace CSTest._25_CS_NewFeatures._05_CS9
{
    record class Coordinate(Angle Longitude, Angle Latitude);

    [TestFixture]
    public class _03_Record
    {
#if CS7
        
        [Test]
        public void TestRecord1()
        {
            var point1 = new Point(1, 5);
            //point1.X = 5;
            
            var point22 = new Point1
            {
                X = 1,
                Y = 4
            };
            point22.X = 5;
            

            var point2 = new Point(1, 5);

            Debug.WriteLine(point1.Equals(point2));//true
            Debug.WriteLine(point1 == point2);//true
            Debug.WriteLine(Equals(point1, point2));//true

            var point3 = new Point(1, 5);
            var point4 = new Point(1, 4);

            Debug.WriteLine(point3.Equals(point4));//False
            Debug.WriteLine(point3 == point4);//False
            Debug.WriteLine(Equals(point3, point4));//False

            //Point point5 = new Point(2, 5);
            //var (x, y) = point5; Error


            Point2 point6 = new Point2(2, 5, 0);
            //point6.X = 3;
            var (xx, yy, zz, www) = point6;//2  5
        }

        [Test]
        public void TestRecord2Copy()
        {
            Point p1 = new Point(3, 3);
            Point p2 = p1 with { Y = 4 };

            Debug.WriteLine(p1);         // Point { X = 3, Y = 3 }
            Debug.WriteLine(p2);         // Point { X = 3, Y = 4 }
        }

        [Test]
        public void TestRecord3Equality()
        {
            var point1 = new Point(1, 5);
            var point2 = new Point(1, 5);

            Debug.WriteLine(point1.Equals(point2));//true
            Debug.WriteLine(point1 == point2);//true
            Debug.WriteLine(Equals(point1, point2));//true

            var point3 = new Point(1, 5);
            var point4 = new Point(1, 4);

            Debug.WriteLine(point3.Equals(point4));//False
            Debug.WriteLine(point3 == point4);//False
            Debug.WriteLine(Equals(point3, point4));//False
        }

        [Test]
        public void TestRecord4Inheritance()
        {
            var point1 = new Point3(1, 5);
            Debug.WriteLine(point1); // { X = 1, Y = 5 }

            var point2 = point1 with {
                X = 10
            };
            Debug.WriteLine(point2); // { X = 10, Y = 5 }

        }

#endif

    }

    record Point
    {
        public Point(double x, double y) => (X, Y) = (x, y);

        public double X { get; init; }
        public double Y { get; init; }
    }

    class PointCompiled
    {
        public PointCompiled(double x, double y) => (X, Y) = (x, y);
        public double X { get; init; }
        public double Y { get; init; }
        protected PointCompiled(PointCompiled original) // Копирующий
        {                               // конструктор
            this.X = original.X;
            this.Y = original.Y;
        }
        // Метод со странным сгенерированным компилятором именем
        //public virtual PointCompiled<Clone>$() => new PointCompiled(this);
        // Дополнительный код для перекрытия Equals, ==, !=,
        // GetHashCode, ToString()...
    }

    record Point1
    {
        public Point1()
        {

        }

        public double X { get; set; }
        public double Y { get; set; }
    }

    record Point2(double X, double Y, in int Z, params string[] W)
    {
        //public virtual bool Equals(Point2 other) =>
        //    other != null && X == other.X && Y == other.Y;
        //protected Point2(Point2 original)
        //{
        //    this.X = original.X; this.Y = original.Y;
        //}

    }

    record Point3Base(double X)
    {
        public Point3Base(Point3Base original)
        {
            X = original.X;
        }
    }

    record Point3 : Point3Base
    {
        public double Y { get; set; }

        public Point3(double X, double Y) : base(X)
        {
            this.Y = Y;
        }

        public virtual bool Equals(Point3 other) =>
            other != null && X == other.X && Y == other.Y;

        protected Point3(Point3 original) : base(original) //копирующий конструктор
        {
            this.Y = original.Y;
        }

    }
}