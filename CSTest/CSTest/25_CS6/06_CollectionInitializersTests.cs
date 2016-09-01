using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;


namespace CSTest._25_CS6
{
    [TestFixture]
    public class _06_CollectionInitializersTests
    {
#if CS6
        Dictionary<string, string> _defaultUsers
        = new Dictionary<string, string>()
        {
            ["admin"] = "admin",
            ["guest"] = "guest",
        };

        [Test]
        public void Test1()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("FirstName", "Ivan");
            dict.Add("LastName", "Ivanov");
            dict.Add("Age", 20.ToString());

            Debug.WriteLine(dict["FirstName"]);
            Debug.WriteLine(dict["LastName"]);
            Debug.WriteLine(dict["Age"]);

            dict = new Dictionary<string, string>()
            {
                {"FirstName", "Ivan"},
                {"LastName", "Ivanov"},
                {"Age", 20.ToString()},
            };

            Debug.WriteLine(dict["FirstName"]);
            Debug.WriteLine(dict["LastName"]);
            Debug.WriteLine(dict["Age"]);

            dict = new Dictionary<string, string>()
            {
                ["FirstName"] = "Ivan",
                ["LastName"] = "Ivanov",
                ["Age"] = 20.ToString(),
            };

            Debug.WriteLine(dict["FirstName"]);
            Debug.WriteLine(dict["LastName"]);
            Debug.WriteLine(dict["Age"]);

            Debug.WriteLine("-------------");

            //Debug.WriteLine(dict.$FirstName);
            Debug.WriteLine(dict["LastName"]);
            Debug.WriteLine(dict["Age"]);
        }

        [Test]
        public void Test3InitializationIEnumerable()
        {

            Storage2 st2 = new Storage2()
            {
                new User("1"),//В C# 6.0 можно использовать метод расширения Add 
                new User("2")
            };
        }

#endif

        [Test]
        public void Test2InitializationIEnumerable()
        {
            Storage st = new Storage()
            {
                new User("1"),//Можно использовать если класс имплементит IEnumerable<User> и прописан метод Add()
                new User("2")
            };

            //Storage2 st2 = new Storage2()
            //{
            //    new User("1"),//Можно использовать если класс имплементит IEnumerable<User> и прописан метод Add()
            //    new User("2")
            //};
        }
    }

    public class Storage : IEnumerable<User>
    {
        private readonly List<User> users = new List<User>();
        public IEnumerator<User> GetEnumerator()
        {
            return users.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public User Add(User user)
        {
            users.Add(user);
            return user;
        }
    }

    public class Storage2 : IEnumerable<User>
    {
        private readonly List<User> users = new List<User>();
        public IEnumerator<User> GetEnumerator()
        {
            return users.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public User Insert(User user)
        {
            users.Add(user);
            return user;
        }
    }

    public static class Storage2Extensions
    {
        public static User Add(this Storage2 storage, User user)
        {
            storage.Insert(user);
            return user;
        }
    }

    public class User
    {
        public string Name;

        public User(string name)
        {
            Name = name;
        }
    }
}
