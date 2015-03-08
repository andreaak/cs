using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._002_NUnit
{
    [TestFixture]
    class TypeAssertsTest
    {
        [Test]
        public void InInstanceOf()
        {
            // Методы для проверки типов объектов.
            Assert.IsInstanceOf(typeof(object), "Hello");
            Assert.IsNotInstanceOf(typeof(string), 5);
        }
    }
}
