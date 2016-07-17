using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CSTest._10_Collections._02_GenericCollections._05_Linq
{
    [TestClass]
    public class Test
    {
        [TestMethod]
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

        [TestMethod]
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
