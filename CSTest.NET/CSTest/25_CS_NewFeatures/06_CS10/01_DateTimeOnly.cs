using NUnit.Framework;
using System.Diagnostics;


namespace CSTest._25_CS_NewFeatures._06_CS10;

    [TestFixture]
    public class _01_DateTimeOnly
    {
#if CS7
        [Test]
        public void TestDateOnly()
        {
            Debug.WriteLine("=> Dates and Times:");

            DateOnly d = new DateOnly(2021, 07, 21);
            Debug.WriteLine(d);
            TimeOnly t = new TimeOnly(13, 30, 0, 0);
            Debug.WriteLine(t);
        }

#endif

    }

