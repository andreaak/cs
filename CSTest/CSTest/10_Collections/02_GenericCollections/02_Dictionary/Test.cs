using System.Collections.Generic;
using NUnit.Framework;
using System.Diagnostics;
using System.Linq;

namespace CSTest._10_Collections._02_GenericCollections._02_Dictionary
{
    [TestFixture]
    public class Test
    {
        const int Iterations = 1000;

        [Test]
        public void TestLDictionary1Initialization()
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

            /*
            Ivan
            Ivanov
            20
            Ivan
            Ivanov
            20 
            */
        }

        [Test]
        public void TestLDictionary2Add()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("FirstName", "Ivan");
            //dict.Add("FirstName", "Ivanov"); //ArgumentException An item with the same key has already been added.

            dict = new Dictionary<string, string>()
            {
                {"FirstName", "Ivan"},
                //{"FirstName", "Ivanov"},//ArgumentException An item with the same key has already been added.
            };

            dict = new Dictionary<string, string>()
            {
                ["FirstName"] = "Ivan",
                ["FirstName"] = "Ivanov",
            };

            /*
            */
        }

        [Test]
        public void TestLDictionary3Grow()
        {
            DictionaryNET<int, int> dict = new DictionaryNET<int, int>();
            for (int i = 0; i < Iterations; i++)
            {
                dict.Add(i, i);
                Debug.WriteLine("Capacity {0} Count {1}", dict.entries.Length, dict.Count);
            }
            Debug.WriteLine("");
            for (int i = Iterations - 1; i >= 0; i--)
            {
                dict.Remove(i);
                Debug.WriteLine("Capacity {0} Count {1}", dict.entries.Length, dict.Count);
            }

            /*
            Capacity 3 Count 1
            Capacity 3 Count 2
            Capacity 3 Count 3
            Capacity 7 Count 4
            Capacity 7 Count 5
            Capacity 7 Count 6
            Capacity 7 Count 7
            Capacity 17 Count 8
            Capacity 17 Count 9
            Capacity 17 Count 10
            Capacity 17 Count 11
            Capacity 17 Count 12
            Capacity 17 Count 13
            Capacity 17 Count 14
            Capacity 17 Count 15
            Capacity 17 Count 16
            Capacity 17 Count 17
            Capacity 37 Count 18
            Capacity 37 Count 19
            Capacity 37 Count 20
            Capacity 37 Count 21
            Capacity 37 Count 22
            Capacity 37 Count 23
            Capacity 37 Count 24
            Capacity 37 Count 25
            Capacity 37 Count 26
            Capacity 37 Count 27
            Capacity 37 Count 28
            Capacity 37 Count 29
            Capacity 37 Count 30
            Capacity 37 Count 31
            Capacity 37 Count 32
            Capacity 37 Count 33
            Capacity 37 Count 34
            Capacity 37 Count 35
            Capacity 37 Count 36
            Capacity 37 Count 37
            Capacity 89 Count 38

            Capacity 1931 Count 38
            Capacity 1931 Count 37
            Capacity 1931 Count 36
            Capacity 1931 Count 35
            Capacity 1931 Count 34
            Capacity 1931 Count 33
            Capacity 1931 Count 32
            Capacity 1931 Count 31
            Capacity 1931 Count 30
            Capacity 1931 Count 29
            Capacity 1931 Count 28
            Capacity 1931 Count 27
            Capacity 1931 Count 26
            Capacity 1931 Count 25
            Capacity 1931 Count 24
            Capacity 1931 Count 23
            Capacity 1931 Count 22
            Capacity 1931 Count 21
            Capacity 1931 Count 20
            Capacity 1931 Count 19
            Capacity 1931 Count 18
            Capacity 1931 Count 17
            Capacity 1931 Count 16
            Capacity 1931 Count 15
            Capacity 1931 Count 14
            Capacity 1931 Count 13
            Capacity 1931 Count 12
            Capacity 1931 Count 11
            Capacity 1931 Count 10
            Capacity 1931 Count 9
            Capacity 1931 Count 8
            Capacity 1931 Count 7
            Capacity 1931 Count 6
            Capacity 1931 Count 5
            Capacity 1931 Count 4
            Capacity 1931 Count 3
            Capacity 1931 Count 2
            Capacity 1931 Count 1
            Capacity 1931 Count 0
            */
        }

        [Test]
        public void TestDictionary4ToDictionary()
        {
            var dic = new Dictionary<string, TestDict>();
            dic = new List<TestDict>
            {
                new TestDict {Code = "AD"},
                //new TestDict {Code = "AD"},//ArgumentException An item with the same key has already been added.
            }.ToDictionary(c => c.Code);
        }

        class TestDict
        {
            public string Code { get; set; }
        }
    }
}
