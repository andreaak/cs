using NUnit.Framework;


namespace CSTest._25_CS_NewFeatures._04_CS8
{
    [TestFixture]
    public class _09_Generics
    {

#nullable enable
        class GenericTest1<T> where T : class?
        {
            public void Test(T t)
            {

            }

        }

        class GenericTest2<T> where T : class
        {
            public void Test(T t)
            {

            }

        }

        class GenericTest3<T> where T : notnull
        {
            public void Test(T t)
            {

            }

        }

        [Test]
        public void TestNullable2()
        {
            GenericTestClass1 pt = null;
            var t = new GenericTest1<GenericTestClass1>();
            t.Test(pt);

            GenericTestClass1 pt2 = null;
            var t2 = new GenericTest2<GenericTestClass1>();
            t2.Test(pt2);

            GenericTestClass1 pt3 = null;
            var t3 = new GenericTest3<GenericTestClass1>();
            t3.Test(pt3);


        }

#nullable disable
    }

    class GenericTestClass1
    {

    }
}