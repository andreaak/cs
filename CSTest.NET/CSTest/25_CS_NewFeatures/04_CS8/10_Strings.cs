
using NUnit.Framework;
using System.Diagnostics;


namespace CSTest._25_CS_NewFeatures._04_CS8
{
    [TestFixture]
    public class _10_Strings
    {
#if CS7
        [Test]
        public void TestStrings()
        {
            Debug.WriteLine(@"Cerebus said ""Darrr! Pret-ty sun-sets""");

            string interp = "interpolation";
            string myLongString2 = $@"This is a very
                                    very
                                    long string with {interp}";
        }

#endif

    }

}