using CS_TDD._000_Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._002_NUnit
{
    // Класс для тестирования калькулятора.
    [TestFixture]   // TestFixture -  указывает, что класс содержит тестовый код.
    class EqualTest : AssertionHelper
    {
        // Метод помеченый атрибутом SetUp будет выполняться при запуске каждого нового теста
        [SetUp]
        public static void SetUpTestsMethod()
        {
        }

        // Метод помеченый атрибутом SetUp будет выполняться при окончании каждого теста 
        [TearDown]
        public static void TearDownTestMethod()
        {
        }
        
        [Test]   // Test -  указывает, что метод содержит тестовый код.
        public static void TestOperations()
        {
            Calculator c = new Calculator();

            Assert.AreEqual(8, c.Add(3, 5));    // 3 + 5 = 8.
            Assert.AreEqual(4, c.Sub(6, 2));    // 6 - 2 = 4.
            // Assert.AreEqual(24, c.Mul(8, 3));   // 8 * 3 = 24.
            Assert.AreEqual(2, c.Div(6, 3));    // 6 / 3 = 2.

            Assert.AreNotEqual(9, c.Add(3, 5)); // 3 + 5 != 9.
        }

        [Test]
        public void ClassicAreEqual()
        {
            // Classic syntax workarounds
            Assert.AreEqual(typeof(string), "Hello".GetType());
            Assert.AreEqual("System.String", "Hello".GetType().FullName);

            Assert.AreNotEqual(typeof(int), "Hello".GetType());
            Assert.AreNotEqual("System.Int32", "Hello".GetType().FullName);
        }

        [Test]
        public void HelperAreEqual()
        {
            // Helper syntax
            Assert.That("Hello", Is.TypeOf(typeof(string)));
            Assert.That("Hello", Is.Not.TypeOf(typeof(int)));
        }

        [Test]
        public void InheritedAreEqual()
        {
            // Inherited syntax
            Expect("Hello", TypeOf(typeof(string)));
            Expect("Hello", Not.TypeOf(typeof(int)));
        }
    }
}
