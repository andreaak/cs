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

            //dict = new Dictionary<string, string>()
            //{
            //    ["FirstName"] = "Ivan",
            //    ["FirstName"] = "Ivanov",
            //};

            /*
            */
        }

        [Test]
        public void TestLDictionary3Iteration()
        {
            DictionaryNET<int, int> dict = new DictionaryNET<int, int>();
            dict.Add(0, 0);
            dict.Add(3, 33);
            dict.Add(6, 66);
            DisplayDictionary(dict);

            dict.Remove(3);
            dict.Add(8, 88);
            DisplayDictionary(dict);
            /*
            ----
            KeyValue
            Key: 0 Value: 0
            Key: 3 Value: 33
            Key: 6 Value: 66
            Buckets: 2 -1 -1 
            Entries
            Key: 0 Value: 0 Hash: 0 Next: -1
            Key: 3 Value: 33 Hash: 3 Next: 0
            Key: 6 Value: 66 Hash: 6 Next: 1
            ----
            KeyValue
            Key: 0 Value: 0
            Key: 8 Value: 88
            Key: 6 Value: 66
            Buckets: 2 -1 1 
            Entries
            Key: 0 Value: 0 Hash: 0 Next: -1
            Key: 8 Value: 88 Hash: 8 Next: -1
            Key: 6 Value: 66 Hash: 6 Next: 0
            */
        }

        [Test]
        public void TestLDictionary4Iteration()
        {
            DictionaryNET<int, int> dict = new DictionaryNET<int, int>();
            Debug.WriteLine("Add 1");
            dict.Add(1, 11);
            DisplayDictionary(dict);
            Debug.WriteLine("Add 0");
            dict.Add(0, 0);
            DisplayDictionary(dict);
            Debug.WriteLine("Add 4");
            dict.Add(4, 44);
            DisplayDictionary(dict);
            Debug.WriteLine("Add 2");
            dict.Add(2, 22);
            DisplayDictionary(dict);
            Debug.WriteLine("Remove 1");
            dict.Remove(1);
            DisplayDictionary(dict);
            Debug.WriteLine("Add 9");
            dict.Add(9, 88);
            DisplayDictionary(dict);
            /*
            Add 1
            ----
            KeyValue
            Key: 1 Value: 11
            Buckets: -1 0 -1 
            Entries
            Key: 1 Value: 11 Hash: 1 Next: -1
            Key: 0 Value: 0 Hash: 0 Next: 0
            Key: 0 Value: 0 Hash: 0 Next: 0
            Add 0
            ----
            KeyValue
            Key: 1 Value: 11
            Key: 0 Value: 0
            Buckets: 1 0 -1 
            Entries
            Key: 1 Value: 11 Hash: 1 Next: -1
            Key: 0 Value: 0 Hash: 0 Next: -1
            Key: 0 Value: 0 Hash: 0 Next: 0
            Add 4
            ----
            KeyValue
            Key: 1 Value: 11
            Key: 0 Value: 0
            Key: 4 Value: 44
            Buckets: 1 2 -1 
            Entries
            Key: 1 Value: 11 Hash: 1 Next: -1
            Key: 0 Value: 0 Hash: 0 Next: -1
            Key: 4 Value: 44 Hash: 4 Next: 0
            Add 2
            ----
            KeyValue
            Key: 1 Value: 11
            Key: 0 Value: 0
            Key: 4 Value: 44
            Key: 2 Value: 22
            Buckets: 1 0 3 -1 2 -1 -1 
            Entries
            Key: 1 Value: 11 Hash: 1 Next: -1
            Key: 0 Value: 0 Hash: 0 Next: -1
            Key: 4 Value: 44 Hash: 4 Next: -1
            Key: 2 Value: 22 Hash: 2 Next: -1
            Key: 0 Value: 0 Hash: 0 Next: 0
            Key: 0 Value: 0 Hash: 0 Next: 0
            Key: 0 Value: 0 Hash: 0 Next: 0
            Remove 1
            ----
            KeyValue
            Key: 0 Value: 0
            Key: 4 Value: 44
            Key: 2 Value: 22
            Buckets: 1 -1 3 -1 2 -1 -1 
            Entries
            Key: 0 Value: 0 Hash: -1 Next: -1
            Key: 0 Value: 0 Hash: 0 Next: -1
            Key: 4 Value: 44 Hash: 4 Next: -1
            Key: 2 Value: 22 Hash: 2 Next: -1
            Key: 0 Value: 0 Hash: 0 Next: 0
            Key: 0 Value: 0 Hash: 0 Next: 0
            Key: 0 Value: 0 Hash: 0 Next: 0
            Add 9
            ----
            KeyValue
            Key: 9 Value: 88
            Key: 0 Value: 0
            Key: 4 Value: 44
            Key: 2 Value: 22
            Buckets: 1 -1 0 -1 2 -1 -1 
            Entries
            Key: 9 Value: 88 Hash: 9 Next: 3
            Key: 0 Value: 0 Hash: 0 Next: -1
            Key: 4 Value: 44 Hash: 4 Next: -1
            Key: 2 Value: 22 Hash: 2 Next: -1
            Key: 0 Value: 0 Hash: 0 Next: 0
            Key: 0 Value: 0 Hash: 0 Next: 0
            Key: 0 Value: 0 Hash: 0 Next: 0
            */
        }

        private void DisplayDictionary(DictionaryNET<int, int> dict)
        {
            Debug.WriteLine("----");
            Debug.WriteLine("KeyValue");
            foreach (var item in dict)
            {
                Debug.WriteLine("Key: {0} Value: {1}", item.Key, item.Value);
            }
            Debug.Write("Buckets: ");
            foreach (var item in dict.buckets)
            {
                Debug.Write(item + " ");
            }
            Debug.WriteLine("");
            Debug.WriteLine("Entries");
            foreach (var item in dict.entries)
            {
                Debug.WriteLine("Key: {0} Value: {1} Hash: {2} Next: {3}", item.key, item.value, item.hashCode, item.next);
            }
        }

        [Test]
        public void TestLDictionary4Grow()
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
        public void TestDictionary5ToDictionary()
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
