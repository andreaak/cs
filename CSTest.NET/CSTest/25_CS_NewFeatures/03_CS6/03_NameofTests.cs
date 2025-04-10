using System;
using NUnit.Framework;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._03_CS6
{
    [TestFixture]
    public class _03_NameofTests
    {
#if CS6
        [Test]
        public void TestNameof1()
        {
            Assert.Catch<ArgumentNullException>(() => TestMethod(null));
        }

        private void TestMethod(string arg)
        {
            Preconditions.CheckNotNull(arg, nameof(arg));
        }

        [Test]
        public void TestNameof2()
        {
            Person person = new Person();
            person.PropertyChanged += (sender, e) => Debug.WriteLine("Property name: " + e.PropertyName);
            person.Name = "Test";
            person.Name = "Test";
            person.Name = "Test2";
            /*
            Property name: Name
            Property name: Name
            Property name: Name
            Property name: Name
            Property name: Name
            Property name: Name
            */
        }

        [Test]
        public void TestNameof3()
        {
            Person person = new Person();
            Debug.WriteLine(nameof(person.Name));
            Debug.WriteLine(nameof(Person.Name));
            //Debug.WriteLine(nameof(Person.Code));
            /*
            Name
            Name
            */
        }
    }

    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    Preconditions.CheckNotNull(value, nameof(Name));
                    name = value;
                    PropertyChanged.SafeInvoke(this, new PropertyChangedEventArgs(nameof(Name)));
                    PropertyChanged.SafeInvoke2(this);
                    PropertyChanged.SafeInvoke2(this, nameof(Name));
                }
            }
        }

        private string Code
        {
            get; set;
        }
    }

    public static class Preconditions
    {
        public static void CheckNotNull<T>(T obj, string name) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }

    public static class NullSafeExtensions
    {
        public static void SafeInvoke(this PropertyChangedEventHandler handler,
            object sender, PropertyChangedEventArgs e)
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void SafeInvoke2(this PropertyChangedEventHandler handler,
                        object sender, [CallerMemberName] string member = null)
        {
            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs(member));
            }
        }
#endif
    }
}
