using NUnit.Framework;
using System;

namespace CSTest._25_CS_NewFeatures._03_CS7
{
#if CS7

    [TestFixture]
    public class _13_privateprotected
    {

        [Test]
        public void TestStructure_1()
        {
            var str1 = new Point3(1);
            //str1.X = 5;
            /*

            */
        }
    }

    class Point3
    {
        private protected int X; //Use private protected when you want the most restrictive protected access (only derived classes in the same assembly)

        public Point3(int x)
        {
            X = x; 
        }

        public int Test()
        {
            return X;
        }

    }

    class Point33 : Point3
    {

        public Point33(int x)
            :base(x)
        { }

        public int Test2()
        {
            var baseObject = new Point3(2);

            // Error CS1540, because myValue can only be accessed by
            // classes derived from BaseClass.
            //baseObject.X = 5;

            var baseObject2 = new Point33(2);
            baseObject2.X = 5;


            return X;
        }
    }




#endif
}
