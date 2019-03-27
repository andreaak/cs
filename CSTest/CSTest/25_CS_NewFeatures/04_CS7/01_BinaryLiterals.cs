using NUnit.Framework;

namespace CSTest._25_CS_NewFeatures._04_CS7
{

    [TestFixture]
    public class _01_BinaryLiterals
    {
#if CS7
        [Test]
        public void TestBinaryLiterals_1()
        {
            int myFavNumber3 = 0b1111_1111_1111;
            int oneMillion = 1_000_000;
            int[] number = { 0b1, 0b10, 0b100, 0b1_000 };
        }
#endif
    }

}
