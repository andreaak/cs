using NUnit.Framework;
using System.Diagnostics;
using System.Xml.Linq;

namespace CSTest._25_CS_NewFeatures._03_CS7
{

    [TestFixture]
    public class _03_Tuples
    {
#if CS7
        [Test]
        public void TestTuples_1()
        {
            TupleSamples ts = new TupleSamples();
            var person = ts.GetNewCS7_Tuple();
            Debug.Write($"\n C# 7 Tuple - Author " +
                $"{person.Item1} {person.Item2} {person.Item3}");
            Debug.Write($"\n C# 7 Tuple - Author " +
                $"{person.name} {person.title} {person.year}");

            (string name, string title, long year) = ts.GetNewCS7_Tuple();
            Debug.Write($"\n C# 7 Tuple - Author " + $"{name} {title} {year}");
        }


        [Test]
        public void TestTuples_NoNames()
        {
            (string, int) bob = ("Bob", 23); //Ключевое слово var для кортежей не обязательно!
            Debug.WriteLine(bob.Item1);  //Bob 
            Debug.WriteLine(bob.Item2);  // 23

            (string, int) person = GetPerson();  // При желании здесь можно было бы
                                                 // применить var 
            Debug.WriteLine(person.Item1);  //Bob 
            Debug.WriteLine(person.Item2);  // 23


            ValueTuple<string, int> bob1 = ValueTuple.Create("Bob", 23);
            (string, int) bob2 = ValueTuple.Create("Bob", 23);
            Debug.WriteLine(bob2.Item1);  //Bob 
            Debug.WriteLine(bob2.Item2);  // 23
        }

        [Test]
        public void TestTuples_Names()
        {
            var (name, age) = ("Bob", 23);
            Debug.WriteLine(name);  // Bob 
            Debug.WriteLine(age);  // 23

            var tuple = (Name: "Bob", Age: 23);
            Debug.WriteLine(tuple.Name);  // Bob 
            Debug.WriteLine(tuple.Age);  // 23

            var person2 = GetPerson2();
            Debug.WriteLine(person2.Name); // Bob
            Debug.WriteLine(person2.Age);  // 23

            (string Name, int Age, char Sex) bob3 = ("Bob", 23, 'M');
            (string Age, int Sex, char Name) bob4 = bob3;
        }

        [Test]
        public void TestTuples_Equal()
        {
            var tl = ("one", 1);
            var t2 = ("one", 1);
            Debug.WriteLine(tl.Equals(t2));  // True
        }

        static (string, int) GetPerson() => ("Bob", 23);
        static (string Name, int Age) GetPerson2() => ("Bob", 23);

    }


    class TupleSamples
    {
        // Ex 6 - C# 7 Tuple
        public (string name, string title, long year) GetNewCS7_Tuple()
        {
            string name = "Reynald Adolphe";
            string title = ".Net Programming";
            long year = 2017;

            return (name, title, year);
        }
#endif
    }
}
