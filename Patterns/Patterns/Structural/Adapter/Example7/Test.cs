using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Patterns.Structural.Adapter.Example7
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            // Работа с элементами старой подсистемы...

            List<ITargetOld> oldTargets = new List<ITargetOld>();

            oldTargets.Add(new AdapteeOld());
            oldTargets.Add(new TwoWayAdapter());

            foreach (ITargetOld target in oldTargets)
                target.MethodOld();


            Console.WriteLine(new string('-', 20));


            // Работа с элементами новой подсистемы...

            List<ITargetNew> newTargets = new List<ITargetNew>();

            newTargets.Add(new AdapteeNew());
            newTargets.Add(new TwoWayAdapter());

            foreach (ITargetNew target in newTargets)
                target.MethodNew();
        }
    }
}
