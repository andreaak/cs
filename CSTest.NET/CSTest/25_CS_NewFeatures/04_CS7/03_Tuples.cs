using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS7
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
