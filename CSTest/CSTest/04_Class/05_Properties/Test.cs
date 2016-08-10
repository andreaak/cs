using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._04_Class._05_Properties
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestProperties1()
        {
            _01_Problem instance = new _01_Problem();

            instance.SetField("Hello world!");     // Метод-мутатор

            string @string = instance.GetField();  // Метод-аксессор

            Debug.WriteLine(@string);
        }

        [Test]
        public void TestProperties2()
        {
            _02_Properties instance = new _02_Properties();

            instance.Field = "Hello world!";    // Метод-мутатор

            Debug.WriteLine(instance.Field);  // Метод-аксессор
        } 
        
        [Test]
        public void TestProperties3()
        {
            _02_Properties instance = new _02_Properties();

            instance.Field = "Goodbye";
            Debug.WriteLine(instance.Field);

            Debug.WriteLine(new string('-', 50));

            instance.Field = "hello world";
            Debug.WriteLine(instance.Field);
        }

        [Test]
        public void TestProperties4()
        {
            _02_Properties constants = new _02_Properties();

            constants.Pi = 3.14159265D;
            //Debug.WriteLine(constants.Pi); // Недопустимо.

            //constants.E = 3.71D;             // Недопустимо.
            Debug.WriteLine("e = {0}", constants.E);
        }
        
        [Test]
        public void TestProperties5()
        {
            _03_AutoProperties author1 = new _03_AutoProperties()
            {
                Name = "Jeffrey Richter",            // Блок инициализатора.
                Book = "CLR via C#"
            };

            _03_AutoProperties author2 = new _03_AutoProperties        // ()
            {
                Name = "Steve McConnell",            // Блок инициализатора.
                Book = "Code Complete"
            };

            Debug.WriteLine("Name: {0}, Book: {1}", author1.Name, author1.Book);
            Debug.WriteLine("Name: {0}, Book: {1}", author2.Name, author2.Book);
        }
    }
}
