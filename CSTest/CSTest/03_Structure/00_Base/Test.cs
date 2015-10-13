using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CSTest._03_Structure._00_Base
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            Book book;// конструктор не вызывается
            //Use of possibly unassigned field 'Author'
            //член структуры нужно сначала инициализировать 
            //Console.WriteLine(book.Author);
            book.Author = "Author1";
            Debug.WriteLine(book.Author);
            book.Title = "Title1";
            Debug.WriteLine(book.Title);
            book.ISBN = 1;
            Debug.WriteLine(book.ISBN);

            // вызов конструктора по умолчанию 
            Book book1 = new Book();
            Debug.WriteLine(book1.Author);
            // вызов явно заданного конструктора
            Book book2 = new Book("Author2", "Title2", 2);
            Debug.WriteLine(book2.Author);

            Book book3 = new Book(book);
            Debug.WriteLine(book3.Author);

            Book book4 = new Book(book);
            book4.ChangeBook(book2);
            Debug.WriteLine(book4.Author);

            TestBook book5 = new TestBook();
            Debug.WriteLine(book5.GetISBN());
        }
    }
}
