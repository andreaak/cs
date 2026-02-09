using NUnit.Framework;
using System;

namespace CSTest._25_CS_NewFeatures._03_CS7
{
#if CS7

    [TestFixture]
    public class _12_Structure
    {

        [Test]
        public void TestStructure_1()
        {
            var str1 = new Point4(1, 4);
            //str1.X = 5;
            //str1.XX = 5; //Error
            /*

            */
        }
    }

    readonly struct Point4
    {
        public readonly int X; //Х и У должны быть readonly 
        public readonly int Y; //Х и У должны быть readonly 

        public int XX { get; }
        public int YY { get; }

        public Point4(int x, int y)
        {
            X = x;
            Y = y;
            XX = 3;
        }

    }

    ref struct Point5 //: TestPoint4 Error
    {
        public readonly int X; //Х и У должны быть readonly 
        public readonly int Y; //Х и У должны быть readonly 

        public Point5(int x, int y)
        {
            X = x;
            Y = y;
        }


       
    }

    interface TestPoint4
    {

    }

#endif
}
