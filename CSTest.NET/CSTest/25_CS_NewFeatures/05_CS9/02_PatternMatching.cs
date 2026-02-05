using NUnit.Framework;
using System.Diagnostics;


namespace CSTest._25_CS_NewFeatures._05_CS9
{
    [TestFixture]
    public class _02_PatternMatching
    {
#if CS7
        [Test]
        public void TestPatternMatching1()
        {

            object testItem1 = 123;
            Type t = typeof(string);
            char c = 'f';
            //Type patterns
            if (t is Type)
            {
                Debug.WriteLine($"{t} is a Type");
            }
            //Relational, Conjuctive, and Disjunctive patterns
            if (c is >= 'a' and <= 'z' or >= 'A' and <= 'Z')
            {
                Debug.WriteLine($"{c} is a character");
            }
            ;
            //Parenthesized patterns
            if (c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',')
            {
                Debug.WriteLine($"{c} is a character or separator");
            }
            ;
            //Negative patterns
            if (testItem1 is not string)
            {
                Debug.WriteLine($"{testItem1} is not a string");
            }

            if (testItem1 is not null)
            {
                Debug.WriteLine($"{testItem1} is not null");
            }

        }

#endif

    }

}