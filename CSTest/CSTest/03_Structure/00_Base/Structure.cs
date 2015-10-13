using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTest._03_Structure._00_Base
{
    //The modifier 'static' is not valid for this item
    //static struct BookStatic
    //{
    //}
    
    struct Book : IEquatable<int> //В структуре можно реализовать один или несколько интерфейсов
    // : BookBase Cтруктуры не могут наследовать другие структуры и классы 
    // или служить в качестве базовых для других структур и классов
    {
        public static string initStatic = "Test";// can have static field initializers in structs
        //public string Fault = "Test"; cannot have instance field initializers in structs
        public string Author;
        public string Title;
        public int ISBN;

        static Book()
        {
            initStatic = "SRT";
        }

        //Structs cannot contain explicit parameterless constructors
        //public Book()
        //{
        //}

        //Fields must be fully assigned before control is returned to the caller
        //public Book(string a)
        //{
        //    Author = a;
        //}

        public Book(string a)
            : this() //can use initializator
        {
            Author = a;
        }

        public Book(string a, string t, int c)
        //: base() // structs cannot call base class constructors
        {
            Author = a;
            Title = t;
            ISBN = c;
        }

        public Book(Book book)
        {
            this = book;
        }

        public void Dispose()
        {
        }

        public void ChangeBook(Book book)
        {
            this = book;
        }

        //Only class types can contain destructors
        //~ Book()
        //{
        //}

        //The modifier 'virtual' is not valid for this item
        //public virtual  void NonCompiled()
        //{
        //}

        //The modifier 'abstract' is not valid for this item
        //public abstract void NonCompiled()
        //{
        //}

        //new protected member declared in struct
        //protected void NonCompiled()
        //{
        //}

        //new protected member declared in struct
        //internal protected void NonCompiled()
        //{
        //}

        public bool Equals(int other)
        {
            return ISBN == other;
        }

        public override bool Equals(object obj)
        {
            if (obj is int)
            {
                return this.Equals((int)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class TestBook
    {
        //Структура, определенная как поле класса, инициализируется 
        //автоматическим обнулением при инициализации ее включающего объекта
        private Book book;

        public int GetISBN()
        {
            return book.ISBN;
        }
    }
}
