using CS_TDD._000_Base;
using Xunit;

namespace CS_TDD._005_xUnit._03_DataDrivenTests
{

    /*
    Repetitive test methods
    Refactor into a single parameterized test method
    [Theory]
    [InlineData]

    */

    public class _01_InlineTestData
    {
        [Theory]
        [InlineData(5, 10, -15)]
        [InlineData(-5, -10, 15)]
        [InlineData(10, 0, -10)]
        [InlineData(0, 0, 0)]
        [InlineData(-99, 99, 0)]
        public void ShouldSubtractTwoNumbers(int firstNumber, int secondNumber, int expectedResult)
        {
            var sut = new MemoryCalculator();

            sut.Sub(firstNumber);
            sut.Sub(secondNumber);

            Assert.Equal(expectedResult, sut.CurrentValue);
        }
    }
}
