using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS8
{
    [TestFixture]
    public class _08_Nullable
    {

        [Test]
        public void TestNullOp_1()
        {
            string s = null;

            if (s == null)
                s = "Добро пожаловать";


            s ??= "Добро пожаловать ";
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
            char[] firstTwo2 = vowels[firstTwoRange]; //'а', 'е'
        }

        [Test]
        public void TestUsing()
        {
            if (File.Exists("file.txt"))
            {
                using var reader = File.Open("file.txt", FileMode.OpenOrCreate);
                Debug.WriteLine(reader.ReadByte());
                Debug.WriteLine(reader.ReadByte());
            }
        }

        [Test]
        public void TestNullable()
        {

#nullable enable
            string s1 = null;
            string? s2 = null;
#nullable disable
            string s3 = null;
            string? s4 = null;
        }
    }
}
