using NUnit.Framework;
using System.Diagnostics;
using CSTest._03_Structure._02_Enums._0_Setup;

namespace CSTest._03_Structure._02_Enums
{
    enum SomeEnum
    {
        Test1 = 1,
        Test2 = 4,
        Test3 = 6,
    }

    [TestFixture]
    public class EnumTests
    {
        [Test]
        public void TestEnum1()
        {
            SomeEnum en = SomeEnum.Test1;
            Debug.WriteLine(en);

            en = (SomeEnum)5;
            Debug.WriteLine(en);

            en = (SomeEnum)4;
            Debug.WriteLine(en);

            en += 10;
            Debug.WriteLine(en);
            /*
            Test1
            5
            Test2
            14
            */
        }
    }
}
