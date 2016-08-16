using NUnit.Framework;

namespace Patterns._02_Structural.Composite._003_RecursionComposition
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            int[] array = null;

            array = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Component tree = new Composite("root");

            tree.Build(array);

            tree.Operation();
        }
    }
}
