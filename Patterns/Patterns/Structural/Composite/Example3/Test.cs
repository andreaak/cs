using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace Patterns.Structural.Composite.Example3
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            int[] array = null;

            array = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Component tree = new Composite("root");

            tree.Build(array);

            tree.Operation();
        }
    }
}
