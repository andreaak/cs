using CS_TDD._005_xUnit._01_LifeCycle.Setup;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace CS_TDD._005_xUnit._03_DataDrivenTests
{
    public class MemoryCalculatorTestDataFile
    {
        public static IEnumerable<object> TestData
        {
            get
            {
                string[] csvLines = File.ReadAllLines("TestData.csv");

                var testCases = new List<object[]>();

                foreach (var csvLine in csvLines)
                {
                    IEnumerable<int> values = csvLine.Split(',').Select(int.Parse);

                    object[] testCase = values.Cast<object>().ToArray();

                    testCases.Add(testCase);
                }

                return testCases;
            }
        }
    }

    public class _03_ExternalData
    {
        [Theory]
        [MemberData("TestData", MemberType = typeof(MemoryCalculatorTestDataFile))]
        public void ShouldSubtractTwoNumbers(int firstNumber, int secondNumber, int expectedResult)
        {
            var sut = new MemoryCalculator();

            sut.Subtract(firstNumber);
            sut.Subtract(secondNumber);

            Assert.Equal(expectedResult, sut.CurrentValue);
        }
    }
}
