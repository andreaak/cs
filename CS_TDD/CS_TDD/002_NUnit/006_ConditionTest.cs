using CS_TDD._000_Base;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._002_NUnit
{
    [TestFixture]
    class ConditionTest
    {
        Calculator calc;

        [SetUp]
        public void Init()
        {
            calc = new Calculator();
        }

        [Test]
        public void IsTrue()
        {
            // 2 + 2 = 4 - True.
            Assert.IsTrue(calc.Add(2, 2) == 4);
        }

        [Test]
        public void IsFalse()
        {
            // 3 * 6 = 20 - False.
            Assert.IsFalse(calc.Mul(3, 6) == 20);
        }

        [Test]
        public void IsNan()
        {
            double d = double.NaN;

            Assert.IsNaN(d);   // d is not a number.
            Assert.IsNaN(0 / 0f);   // 0/0f is not a number.
        }

        [Test]
        public void IsEmpty()
        {
            Assert.IsEmpty("");
            Assert.IsNotEmpty("Hello!");

            Assert.IsEmpty(new ArrayList());
            Assert.IsNotEmpty(new List<string> { "one", "two" });
        }
    }
}
