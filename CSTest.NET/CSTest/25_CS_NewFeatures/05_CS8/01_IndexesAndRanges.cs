using NUnit.Framework;

namespace CSTest._25_CS_NewFeatures._05_CS8
{
    [TestFixture]
    public class _01_IndexesAndRanges
    {

        [Test]
        public void TestIndexes_1()
        {
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            char lastElement = vowels[^1];  //'u'
            char secondToLast = vowels[^2]; //'о'

            /*
            Второе число в диапазоне является исключающим, поэтому ..2 возвращает
            элементы, находящиеся перед vowels[2] . 
            ( ^0 равно длине массива, так что vowels[^0] приведет к генерации ошибки.)
             */

            char[] firstTwo = vowels[..2];  //'а', 'е'
            char[] lastThree = vowels[2..]; //'i', 'о', 'u'
            char[] middleOne = vowels[2..3];//'i'
            char[] lastTwo = vowels[^2..];  //'о', 'u'
            //char[] last = vowels[9..]; 
            //char[] last = vowels[0..9];
        }


        [Test]
        public void TestIndexes_2()
        {
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            
            Index last = ^1 ;
            char lastChar = vowels[last];

            Range firstTwoRange = 0..2;
            char[] firstTwo = vowels[firstTwoRange] ; //'а', 'е'
        }
    }
}
