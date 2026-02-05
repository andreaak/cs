using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._03_CS7
{

    [TestFixture]
    public class _04_Deconstruction
    {
#if CS7
        [Test]
        public void TestDeconstruction_Tuples()
        {
            TupleSamples ts = new TupleSamples();
            var person = ts.GetNewCS7_Tuple();
            Debug.Write($"\n C# 7 Tuple - Author " + $"{person.name} {person.title} {person.year}");

            (var name2, var title2, var year2) = ts.GetNewCS7_Tuple();
            Debug.Write($"\n C# 7 Tuple - Author " + $"{name2} {title2} {year2}");

            var (name, title, year) = ts.GetNewCS7_Tuple();
            Debug.Write($"\n C# 7 Tuple - Author " + $"{name} {title} {year}");

            /*
             C# 7 Tuple - Author Reynald Adolphe .Net Programming 2017
             C# 7 Tuple - Author Reynald Adolphe .Net Programming 2017
             C# 7 Tuple - Author Reynald Adolphe .Net Programming 2017 
             */
        }


        [Test]
        public void TestDeconstruction_Class()
        {
            Comedian comedian = new Comedian("Reynald", "Adolphe");
            var (firstName, lastName) = comedian;
            Debug.WriteLine(firstName);
            Debug.WriteLine(lastName);
            
            (var firstName2, var lastName2) = comedian;
            Debug.WriteLine(firstName2);
            Debug.WriteLine(lastName2);

            string firstName3;
            string lastName3;

            (firstName3, lastName3) = comedian;
            Debug.WriteLine(firstName3);
            Debug.WriteLine(lastName3);


            /*
            Reynald
            Adolphe
            Reynald
            Adolphe
            Reynald
            Adolphe
             */

        }
#endif
    }

    public class Comedian
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Comedian(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }
    }
}
