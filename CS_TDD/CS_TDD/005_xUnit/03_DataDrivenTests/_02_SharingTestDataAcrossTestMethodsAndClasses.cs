using CS_TDD._005_xUnit._01_LifeCycle.Setup;
using System.Collections.Generic;
using Xunit;

namespace CS_TDD._005_xUnit._03_DataDrivenTests
{
    /*
    Repetitive [InlineData] uses
    Refactor to static test data property
    [MemberData]
    Separate class
    */

    public class _02_SharingTestDataAcrossTestMethodsAndClasses
    {
        [Theory]
        [MemberData("TestData", MemberType = typeof(MemoryCalculatorTestData))]
        public void ShouldSubtractTwoNumbers2(int firstNumber, int secondNumber, int expectedResult)
        {
            var sut = new MemoryCalculator();

            sut.Subtract(firstNumber);
            sut.Subtract(secondNumber);

            Assert.Equal(expectedResult, sut.CurrentValue);
        }
    }

    public class MemoryCalculatorTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[] { 5, 10, -15 };
                yield return new object[] { -5, -10, 15 };
                yield return new object[] { 10, 0, -10 };
                yield return new object[] { 0, 0, 0 };
                yield return new object[] { -99, 99, 0 };
            }
        }
    }
}
