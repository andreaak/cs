using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns._03_Behavioral.Strategy._003_StrategySort.Strategy;

namespace Patterns._03_Behavioral.Strategy._003_StrategySort
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            var sort = new SelectionSort();
            var context = new Context(sort);
            context.Sort();
            context.PrintArray();           
        }
    }
}
