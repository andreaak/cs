using System;
using System.Diagnostics;

namespace CSTest._03_Structure._0_Setup
{
    //The modifier 'static' is not valid for this item
    //Структура не может быть объявленная как static
    //static struct BookStatic
    //{
    //}

    // Cтруктуры не могут наследовать другие структуры и классы 
    // или служить в качестве базовых для других структур и классов
    //struct BookBase
    //{

    //}

    struct TestStructure : IEquatable<TestStructure> //В структуре можно реализовать один или несколько интерфейсов
    // : BookBase Cтруктуры не могут наследовать другие структуры и классы 
    // или служить в качестве базовых для других структур и классов
    {
        public static string initStatic = "Test";// can have static field initializers in structs
        //public string Fault = "Test"; cannot have instance field initializers in structs
        public string Author;
        public string Title;
        public int ISBN;

        public static int staticField;
        
        static TestStructure()
        {
            Debug.WriteLine("Static ctor");
            initStatic = "SRT";
        }

        //Structs cannot contain explicit parameterless constructors
        //public TestStructure()
        //{
        //}

        //Fields must be fully assigned before control is returned to the caller
        //public TestStructure(string a)
        //{
        //    Author = a;
        //}

        public TestStructure(string a)
            : this() //can use initializator
        {
            Debug.WriteLine("Ctor with 1 param");
            Author = a;
        }

        public TestStructure(string a, string t, int c)
        //: base() // structs cannot call base class constructors
        {
            Debug.WriteLine("Ctor with 3 param");
            Author = a;
            Title = t;
            ISBN = c;
        }

        public TestStructure(TestStructure book)
        {
            Debug.WriteLine("Copy Ctor");
            this = book;
        }

        public void ChangeBook(TestStructure book)
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

        public bool Equals(TestStructure other)
        {
            Debug.WriteLine("Equals TestStructure method");
            return ISBN == other.ISBN;
        }

        public override bool Equals(object obj)
        {
            Debug.WriteLine("Equals object method");
            if (obj is TestStructure)
            {
                return this.Equals((TestStructure)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ISBN;
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
        private TestStructure book;

        public int GetISBN()
        {
            return book.ISBN;
        }
    }

    struct Point
    {
        public int x;
        public int y;
    }
}
