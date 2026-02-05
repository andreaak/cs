using NUnit.Framework;
using System.Diagnostics;

namespace CSTest._25_CS_NewFeatures._04_CS8
{
    [TestFixture]
    public class _04_LocalFunction
    {
#if CS7
        [Test]
        public void TestLocalFunction_1()
        {
            int start = 6;
            MyNumbers();

            // Local Functions
            void MyNumbers()
            {
                int myFavNumber3 = 0b1111111111;
                int myFavNumber4 = 0b1111_1111_11;
                int OneMillion = 1_000_000;
                Debug.WriteLine($"These are my fav numbers: " +
                 $"{myFavNumber3}, {myFavNumber4}, { OneMillion}, {start}");
            }

            // Local Functions
            static void MyNumbersStatic()
            {
                int myFavNumber3 = 0b1111111111;
                Debug.WriteLine(myFavNumber3);
                //Debug.WriteLine(start); Error
            }


            /*
            These are my fav numbers: 1023, 1023, 1000000
            total of 10 + 6 = 16 
            */
        }



#endif
    }
}
