using System.Linq;
using NUnit.Framework;

namespace CSTest._10_Collections._02_GenericCollections._09_Linq
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestDeferedExecution()
        {
            int[] a = { 1, 4, 3, 7, 8, 2, 9, 5, 6 };

            var b = a.Where(Filter);
            if (!b.Any())
            {

            }

            int count = b.Count();
            int c = b.First();
        }

        [Test]
        public void TestNotDeferedExecution()
        {
            int[] a = { 1, 4, 3, 7, 8, 2, 9, 5, 6 };

            var b = a.Where(Filter).ToArray();
            if (!b.Any())
            {

            }

            int count = b.Count();
            int c = b.First();
        }

        private bool Filter(int arg)
        {
            return arg > 5;
        }
    }
}
