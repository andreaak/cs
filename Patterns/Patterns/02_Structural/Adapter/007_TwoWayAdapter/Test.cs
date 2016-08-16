using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using Patterns._02_Structural.Adapter._007_TwoWayAdapter.New_Subsystem;
using Patterns._02_Structural.Adapter._007_TwoWayAdapter.Old_Subsystem;

namespace Patterns._02_Structural.Adapter._007_TwoWayAdapter
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            // Работа с элементами старой подсистемы...

            List<ITargetOld> oldTargets = new List<ITargetOld>();

            oldTargets.Add(new AdapteeOld());
            oldTargets.Add(new TwoWayAdapter());

            foreach (ITargetOld target in oldTargets)
                target.MethodOld();


            Debug.WriteLine(new string('-', 20));


            // Работа с элементами новой подсистемы...

            List<ITargetNew> newTargets = new List<ITargetNew>();

            newTargets.Add(new AdapteeNew());
            newTargets.Add(new TwoWayAdapter());

            foreach (ITargetNew target in newTargets)
                target.MethodNew();
        }
    }
}
