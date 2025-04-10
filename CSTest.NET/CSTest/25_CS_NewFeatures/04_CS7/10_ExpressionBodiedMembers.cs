using NUnit.Framework;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CSTest._25_CS_NewFeatures._04_CS7
{

    [TestFixture]
    public class _10_ExpressionBodiedMembers
    {
#if CS7
        [Test]
        public void TestExpressionBodiedMembers_1()
        {
            Person p = new Person("Test");
            p.Name = "Test2";
            Debug.WriteLine($"Name: {p.Name}");

            /*
            68 characters in file.
            */
        }
    }

    class Person
    {
        #region Classic Constructor

        //public Person(string name)
        //{
        //    Name = name;
        //}

        public Person(string name) => Name = name;

        #endregion

        #region Classic Full Properties

        //private string _name;
        //public string Name
        //{
        //    get { return _name; }
        //    set { _name = value; }
        //}

        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        #endregion

        #region Classic Desctructor

        //~Person()
        //{
        //    Debug.WriteLine("Destructor was called!");
        //}

        ~Person() => Debug.WriteLine("Destructor was called!");

        #endregion
#endif
    }

}
