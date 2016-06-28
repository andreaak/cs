using System.Collections.Generic;
using System.Linq;
using CS_TDD._000_Base;
using Xunit;
using Xunit.Abstractions;

namespace CS_TDD._005_xUnit._01_LifeCycle
{
    /*
    Default random collection order
    Implement ITestCollectionOrderer
    Run collections in alphabetical order
    Configure in assembly info [assembly: TestCollectionOrderer("CS_TDD._005_xUnit._01_LifeCycle.AlphabeticCollectionOrderer", "CS_TDD._005")]
    */

    public class AlphabeticCollectionOrderer : ITestCollectionOrderer
    {
        public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
        {
            var collectionOrder = testCollections.OrderBy(testCollection => testCollection.DisplayName);

            return collectionOrder;
        }
    }

    [Collection("Zeta")]
    public class MemoryCalculatorAddTestsOrderBy
    {
        [Fact]
        public void ShouldAddPositiveNumbers()
        {
            var sut = new MemoryCalculator();

            sut.Add(10);
            sut.Add(5);

            Assert.Equal(15, sut.CurrentValue);
        }
    }

    public class MemoryCalculatorDivideTestsOrderBy
    {
        [Fact]
        public void ShouldDividePositiveNumbers()
        {
            var sut = new MemoryCalculator();

            sut.Add(10);
            sut.Div(2);

            Assert.Equal(5, sut.CurrentValue);
        }
    }

    public class MemoryCalculatorSubtractTestsOrderBy
    {
        [Fact]
        public void ShouldSubtractPositiveNumbers()
        {
            var sut = new MemoryCalculator();

            sut.Sub(5);

            Assert.Equal(-5, sut.CurrentValue);
        }
    }
}
