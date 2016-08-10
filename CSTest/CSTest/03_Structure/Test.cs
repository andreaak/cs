using NUnit.Framework;
using System;
using System.Diagnostics;
using CSTest._03_Structure._0_Setup;

namespace CSTest._03_Structure
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestStructure1()
        {
            _01_StructureBook book;// конструктор не вызывается
            //Use of possibly unassigned field 'Author'
            //член структуры нужно сначала инициализировать 
            //Debug.WriteLine(book.Author);
            book.Author = "Author1";
            Debug.WriteLine(book.Author);
            book.Title = "Title1";
            Debug.WriteLine(book.Title);
            book.ISBN = 1;
            Debug.WriteLine(book.ISBN);
            book.field = 1;
            Debug.WriteLine(book.ISBN);

            // вызов конструктора по умолчанию 
            _01_StructureBook book1 = new _01_StructureBook();
            Debug.WriteLine(book1.Author);
            // вызов явно заданного конструктора
            _01_StructureBook book2 = new _01_StructureBook("Author2", "Title2", 2);
            Debug.WriteLine(book2.Author);

            _01_StructureBook book3 = new _01_StructureBook(book);
            Debug.WriteLine(book3.Author);

            _01_StructureBook book4 = new _01_StructureBook(book);
            book4.ChangeBook(book2);
            Debug.WriteLine(book4.Author);

            TestBook book5 = new TestBook();
            Debug.WriteLine(book5.GetISBN());
        }

        [Test]
        public void TestStructure2()
        {
            _01_StructureBook book6 = new _01_StructureBook();
            _01_StructureBook book7 = new _01_StructureBook();
            Debug.WriteLine(book6.Equals(book7));
            Debug.WriteLine(book6.Equals((ValueType)book7));

            Debug.WriteLine(book6.GetHashCode());
            Debug.WriteLine(book7.GetHashCode());
            /*
            Equals
            True
            Equals object
            Equals
            True

            346948956
            346948956
            */
        }

        [Test]
        public void TestStructure3()
        {
            MyStruct1 mstr;
            mstr.field = 1;
            Debug.WriteLine(mstr.field);
        }

        [Test]
        public void TestStructure4()
        {
            MyStruct1 mstr = new MyStruct1();
            Debug.WriteLine(mstr.field);
        }

        [Test]
        public void TestStructure5()
        {
            MyStruct mstr = new MyStruct();
            ValueType valueType = mstr;
            Debug.WriteLine("instance = " + mstr.GetHashCode());
            Debug.WriteLine("valueType = " + valueType.GetHashCode());

        }

        [Test]
        public void TestStructure6()
        {
            var p = new Point(); p.x = 5; p.y = 9;
            var list = new System.Collections.Generic.List<Point>(10);
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

        [Test]
        public void TestStructure7()
        {

        }
    }

    struct Point
    {
        public int x;
        public int y;
    }
}
