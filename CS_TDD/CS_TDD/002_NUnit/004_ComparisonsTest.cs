using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._002_NUnit
{
    [TestFixture]
    class ComparisonsTest
    {
        private int a, b;

        [SetUp]
        public void Init()
        {
            a = 10;
            b = 20;
        }

        [Test]
        public void Greater()
        {
            // Проверяет, является ли один объект больше, чем другой (a > b).
            Assert.Greater(a, b);

            // a >= b.
            Assert.GreaterOrEqual(a, b);
        }

        [Test]
        public void Less()
        {
            // Проверяет, является ли один объект меньше, чем другой (a < b).
            Assert.Less(a, b);

            // a <= b.
            Assert.LessOrEqual(a, b);
        }
    }
}

