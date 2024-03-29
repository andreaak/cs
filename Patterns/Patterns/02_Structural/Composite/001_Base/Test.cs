﻿using NUnit.Framework;

namespace Patterns._02_Structural.Composite._001_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            Component root = new Composite("ROOT");
            Component branch1 = new Composite("BR1");
            Component branch2 = new Composite("BR2");
            Component leaf1 = new Leaf("L1");
            Component leaf2 = new Leaf("L2");

            root.Add(branch1);
            root.Add(branch2);
            branch1.Add(leaf1);
            branch2.Add(leaf2);

            root.Operation();

            branch2.GetChild(0).Operation();
        }
    }
}
