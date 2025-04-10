using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using CSTest._03_Structure._0_Setup;

namespace CSTest._03_Structure
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestStructure1Creation()
        {
            TestStructure book;// конструктор и статический конструктор не вызывается
            //Use of possibly unassigned field 'Author'
            //член структуры нужно сначала инициализировать 
            //Debug.WriteLine(book.Author);
            book.Author = "Author1";
            Debug.WriteLine(book.Author);
            book.Title = "Title1";
            Debug.WriteLine(book.Title);
            book.ISBN = 1;
            Debug.WriteLine(book.ISBN);
            /*
            Author1
            Title1
            1
           */
        }

        [Test]
        public void TestStructure2Ctors()
        {
            // вызов конструктора по умолчанию 
            Debug.WriteLine("Default ctor test");
            TestStructure book1 = new TestStructure();//конструктор по умолчанию не вызывает статический конструктор
            Debug.WriteLine(book1.Author ?? "Null");

            // вызов явно заданного конструктора
            Debug.WriteLine("Not default ctor test");
            TestStructure book2 = new TestStructure("Author2", "Title2", 2);
            Debug.WriteLine(book2.Author);

            TestClass book5 = new TestClass();
            Debug.WriteLine(book5.GetISBN());
            /*
            Default ctor test
            Null

            Not default ctor test
            Static ctor
            Ctor with 3 param
            Author2
            0
           */
        }

        [Test]
        public void TestStructure3CopyCtors()
        {
            Debug.WriteLine("");
            TestStructure book;
            book.Author = "Author1";
            book.Title = "Title1";
            book.ISBN = 1;

            Debug.WriteLine("");
            TestStructure book2;
            book2.Author = "Author2";
            book2.Title = "Title2";
            book2.ISBN = 2;

            Debug.WriteLine("Copy ctor test");
            TestStructure book3 = new TestStructure(book);
            Debug.WriteLine(book3.Author);

            Debug.WriteLine("");
            TestStructure book4 = new TestStructure(book);
            book4.ChangeBook(book2);
            Debug.WriteLine(book4.Author);

            TestClass book5 = new TestClass();
            Debug.WriteLine(book5.GetISBN());
            /*
            Copy ctor test
            Static ctor
            Copy Ctor
            Author1

            Copy Ctor
            Author2
            0
            */
        }

        [Test]
        public void TestStructure4Equals()
        {
            TestStructure book6 = new TestStructure();
            TestStructure book7 = new TestStructure();
            Debug.WriteLine("book6.Equals(book7) = " + book6.Equals(book7));
            Debug.WriteLine("book6.Equals((ValueType)book7) = " + book6.Equals((ValueType)book7));
            //Debug.WriteLine(book6 == book7);//можно использовать после переопределния оператора ==

            /*
            Static ctor
            Equals TestStructure method
            book6.Equals(book7) = True
            Equals object method
            Equals TestStructure method
            book6.Equals((ValueType)book7) = True
            */
        }

        [Test]
        public void TestStructure4EqualsDefault()
        {
            /*
            Microsoft предлагает уже перегруженный метод Equals() в классе System.ValueType, 
            предназначенный для проверки равенства типов значений. 
            Когда вызывается pt1.Equals(pt2), то возвращаемое значение будет true или false - в зависимости от того, 
            содержат ли pt1 и pt2 одинаковые значения во всех своих полях, 
            если тип значений содержит поля типа ссылок по умолчанию просто сравниваются их адреса.
            */

            Point pt1;
            pt1.x = pt1.y = 5;
            pt1.test = null;

            Point pt2;
            pt2.x = pt2.y = 8;
            pt2.test = null;

            Point pt3;
            pt3.x = pt3.y = 5;
            pt3.test = null;

            Point pt4;
            pt4.x = pt4.y = 5;
            pt4.test = new TestClass();

            Debug.WriteLine("pt1.Equals(pt2) = " + pt1.Equals(pt2));
            Debug.WriteLine("pt1.Equals(pt3) = " + pt1.Equals(pt3));
            Debug.WriteLine("pt1.Equals(pt4) = " + pt1.Equals(pt4));

            /*
            pt1.Equals(pt2) = False
            pt1.Equals(pt3) = True
            pt1.Equals(pt4) = False
            */
        }

        [Test]
        public void TestStructure5EqualsStatic()
        {
            TestStructure book6 = new TestStructure();
            TestStructure book7 = new TestStructure();
            Debug.WriteLine(Equals(book6, book7));
            /*
            Static ctor
            Equals object method
            Equals TestStructure method
            True
            */
        }

        [Test]
        public void TestStructure6ReferenceEquals()
        {
            TestStructure book6 = new TestStructure();
            TestStructure book7 = new TestStructure();
            Debug.WriteLine(ReferenceEquals(book6, book7));//is always false with value type
            /*
            False
            */
        }

        [Test]
        public void TestStructure7HashCode()
        {
            TestStructure book6 = new TestStructure();
            TestStructure book7 = new TestStructure();

            Debug.WriteLine(book6.GetHashCode());
            Debug.WriteLine(book7.GetHashCode());

            ValueType valueType = book6;
            Debug.WriteLine("valueType = " + valueType.GetHashCode());

            /*
            Static ctor
            0
            0
            valueType = 0
            */
        }

        [Test]
        public void TestStructure8()
        {
            var p = new Point();
            p.x = 5;
            p.y = 9;

            var list = new List<Point>(10);
            list.Add(p);
            Point pt = list[0];
            //list[0].x = 90;//ошибка компиляции

            var array = new Point[10];
            array[0] = p;
            array[0].x = 90;//все ок
            /*В первом случае мы получим ошибку компиляции, 
             поскольку индексатор коллекции это всего-навсего метод, 
             который возвращает копию нашей структуры. 
             Во втором случае мы ошибки не получим, поскольку 
             индексация в массивах это не вызов метода, 
             а обращение именно к нужному элементу.*/
        }
    }
}
