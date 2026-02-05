using NUnit.Framework;

namespace CSTest._25_CS_NewFeatures._04_CS8
{
    [TestFixture]
    public class _01_IndexesAndRanges
    {

        [Test]
        public void TestIndexes_1()
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            char lastElement = vowels[^1];  //'u'
            char secondToLast = vowels[^2]; //'о'

            Index first = 0;
            Index last = ^1;
            char firstElement2 = vowels[first];// 'a'
            char lastElement2 = vowels[last];  //  'u'  
        }


        [Test]
        public void TestIndexes_Range()
        {
            /*
            Второе число в диапазоне является исключающим, поэтому ..2 возвращает
            элементы, находящиеся перед vowels[2] . 
            ( ^0 равно длине массива, так что vowels[^0] приведет к генерации ошибки.)
             */
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            char[] firstTwo = vowels[..2];  //'а', 'е'
            char[] lastThree = vowels[2..]; //'i', 'о', 'u'
            char[] middleOne = vowels[2..3];//'i'
            char[] middleOne2 = vowels[2..2];//char[0]

            char[] lastTwo = vowels[^2..];  //'о', 'u'
                                            //char[] last = vowels[9..]; 
                                            //char[] last = vowels[0..9];

            Range firstTwoRange = 0..2;
            char[] firstTwo2 = vowels[firstTwoRange] ; //'а', 'е'
        }
    }
}
