﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Patterns._02_Structural.Adapter._005_PluggableAdapter
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            List<ITarget> targets = new List<ITarget>();

            targets.Add(new Target());

            targets.Add(new PluggableAdapter(Adaptee.A));
            // или/и ...
            targets.Add(new PluggableAdapter(Adaptee.B));

            foreach (ITarget target in targets)
                target.Request();

        }
    }
}