using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CSTest._03_Structure
{
    //The modifier 'static' is not valid for this item
    //static struct BookStatic
    //{
    //}

    struct BookBase
    {

    }

    struct _01_StructureBook : IEquatable<_01_StructureBook> //В структуре можно реализовать один или несколько интерфейсов
    // : BookBase Cтруктуры не могут наследовать другие структуры и классы 
    // или служить в качестве базовых для других структур и классов
    {
        public static string initStatic = "Test";// can have static field initializers in structs
        //public string Fault = "Test"; cannot have instance field initializers in structs
        public string Author;
        public string Title;
        public int ISBN;

        public static int staticField;
        public static int StaticField
        {
            get { return staticField; }
            set { staticField = value; }
        }

        public int field;
        public int Field
        {
            get { return field; }
            set { field = value; }
        }
        
        static _01_StructureBook()
        {
            initStatic = "SRT";
        }

        //Structs cannot contain explicit parameterless constructors
        //public _01_StructureBook()
        //{
        //}

        //Fields must be fully assigned before control is returned to the caller
        //public _01_StructureBook(string a)
        //{
        //    Author = a;
        //}

        public _01_StructureBook(string a)
            : this() //can use initializator
        {
            Author = a;
        }

        public _01_StructureBook(string a, string t, int c)
        //: base() // structs cannot call base class constructors
        {
            Author = a;
            Title = t;
            ISBN = c;
            field = 1;
        }

        public _01_StructureBook(_01_StructureBook book)
        {
            this = book;
        }

        public void ChangeBook(_01_StructureBook book)
        {
            this = book;
        }
        public void Dispose()
        {
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

        public bool Equals(_01_StructureBook other)
        {
            Debug.WriteLine("Equals");
            return ISBN == other.ISBN;
        }

        public override bool Equals(object obj)
        {
            Debug.WriteLine("Equals object");
            if (obj is _01_StructureBook)
            {
                return this.Equals((_01_StructureBook)obj);
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
        private _01_StructureBook book;

        public int GetISBN()
        {
            return book.ISBN;
        }
    }
}
