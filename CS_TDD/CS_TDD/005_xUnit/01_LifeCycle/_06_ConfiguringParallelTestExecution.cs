using CS_TDD._005_xUnit._01_LifeCycle.Setup;
using Xunit;

namespace CS_TDD._005_xUnit._01_LifeCycle
{
    /*
    Default test collection per test class
    Assembly configuration add into AssemblyInfo.cs //[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)] 
    [Collection]
    */
    [Collection("My tests")]
    public class MemoryCalculatorAddTestsParallel
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

    [Collection("My tests")]
    public class MemoryCalculatorSubtractTestsParallel
    {
        [Fact]
        public void ShouldSubtractPositiveNumbers()
        {
            var sut = new MemoryCalculator();

            sut.Subtract(5);

            Assert.Equal(-5, sut.CurrentValue);
        }
    }
}
