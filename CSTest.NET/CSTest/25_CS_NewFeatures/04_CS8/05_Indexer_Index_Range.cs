using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS8
{
    [TestFixture]
    public class _05_Indexer_Index_Range
    {
#if CS7
        [Test]
        public void TestIndexer_1()
        {
            Sentence s = new Sentence();

            Debug.WriteLine(s[^1]);           //лис
            string[] firstTwoWords = s[..2];    //(Большой,  хитрый) 

            /*

            */
        }



#endif
    }

    class Sentence
    {
        string[] words = "Большой хитрый рыжий лис".Split();
        
        public string this[int wordNum]
        {
            get { return words[wordNum]; }

            set { words[wordNum] = value; }
        }

        public string this[Index index] => words[index];

        public string[] this[Range range] => words[range];
    }

}
