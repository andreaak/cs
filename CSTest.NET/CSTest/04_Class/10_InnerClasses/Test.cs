using NUnit.Framework;

namespace CSTest._04_Class._10_InnerClasses
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestInnerClass1()
        {
            Outer outer = new Outer();
            //Outer.InnerPrivate privat = new Outer.InnerPrivate();//Outer.InnerPrivate is inaccessible due to its protection level
            //Outer.InnerProtected prot = new Outer.InnerProtected();//Outer.InnerProtected is inaccessible due to its protection level
            Outer.InnerInternal inter = new Outer.InnerInternal();

            Derived derived = new Derived();
        }
    }

    public class Outer
    {
        public Outer()
        {
            InnerInternal inter = new InnerInternal();
            InnerProtected prot = new InnerProtected();
            InnerPrivate privat = new InnerPrivate();
        }

        private class InnerPrivate
        {
            
        }

        protected class InnerProtected
        {

        }

        internal class InnerInternal
        {

        }

        internal static class InnerStatic
        {
            public static int T()
            {
                return 1;
            }
        }
    }

    public class Derived : Outer
    {
        public Derived()
        {
            InnerInternal inter = new InnerInternal();
            InnerProtected prot = new InnerProtected();
            var t = InnerStatic.T();
            //InnerPrivate privat = new InnerPrivate();//InnerPrivate is inaccessible due to its protection level
        }
    }
}
