﻿using NUnit.Framework;

namespace Patterns._01_Creational.Prototype._002_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Prototype prototype = null;
            Prototype original = null;

            prototype = new ConcretePrototype1(1);
            original = prototype.Clone();

            prototype = new ConcretePrototype2(2);
            original = prototype.Clone();
        }

    }
}
