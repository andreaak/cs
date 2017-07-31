using System;
using NUnit.Framework;
using System.Diagnostics;

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

        [Test]
        public void TestEnum2()
        {
            SomeEnum en = SomeEnum.Test1;
            Debug.WriteLine(Enum.GetName(typeof(SomeEnum), en));
            Debug.WriteLine(en.ToString());
            foreach (var item in Enum.GetValues(typeof(SomeEnum)))
            {
                
            }
            /*
            Test1
            */
        }

        [Test]
        public void TestEnum3()
        {
            SomeEnum2 en = SomeEnum2.FreeSpineOne;
            int i = 2;
            if (Enum.TryParse(i.ToString(), out en))
            {
               Debug.WriteLine(en); 
            }
            else
            {
                Debug.WriteLine("Can't parse");
            }

            SomeEnum2 en2 = (SomeEnum2) i;
            Debug.WriteLine(en2);

            /*
            Test1
            */
        }
    }

    enum SomeEnum2
    {
        Regular = -1,
        FreeSpineOne = 0,
        FreeSpineDouble = 1,
        FreeSpineQuatro = 2,
    }
}
