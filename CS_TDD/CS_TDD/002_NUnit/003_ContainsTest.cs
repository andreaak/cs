using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._002_NUnit
{
    [TestFixture]
    class ContainsTest : AssertionHelper
    {
        [Test]
        public void Contains()
        {
            ArrayList array = new ArrayList();
            array.Add("Alex");
            array.Add("Serg");
            array.Add("John");

            // Contains используется для проверки объектов,
            // содержащихся в коллекции или массиве.
            Assert.Contains("Alex", array);
        }

        [Test]
        public void ClassicContains()
        {
            int[] iarray = new int[] { 1, 2, 3 };
            string[] sarray = new string[] { "a", "b", "c" };

            // Classic syntax
            Assert.Contains(3, iarray);
            Assert.Contains("b", sarray);
        }

        [Test]
        public void HelperContains()
        {
            int[] iarray = new int[] { 1, 2, 3 };
            string[] sarray = new string[] { "a", "b", "c" };

            // Helper syntax
            Assert.That(iarray, Has.Member(3));

            Assert.That(sarray, Has.No.Member("x"));
        }

        [Test]
        public void InheritedContains()
        {
            int[] iarray = new int[] { 1, 2, 3 };
            string[] sarray = new string[] { "a", "b", "c" };

            // Inherited syntax
            Expect(iarray, Contains(3));
            Expect(sarray, Contains("b"));
            Expect(sarray, Not.Contains("x"));
        }
    }
}
