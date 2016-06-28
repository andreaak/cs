using System.Collections.Generic;
using System.IO;
using System.Linq;
using CS_TDD._000_Base;
using CS_TDD._005_xUnit._03_DataDrivenTests.Setup;
using Xunit;
using Xunit.Extensions;

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
        [Xunit.Theory]
        [MemberData("TestData", MemberType = typeof(MemoryCalculatorTestDataFile))]
        public void DataFromCsv(int firstNumber, int secondNumber, int expectedResult)
        {
            var sut = new MemoryCalculator();

            sut.Sub(firstNumber);
            sut.Sub(secondNumber);

            Assert.Equal(expectedResult, sut.CurrentValue);
        }

        [Xunit.Theory]
        [ExcelData("TestData.xls", "Select * from TestData")]
        public void DataFromExcel(int number, bool expectedResult)
        {
            var sut = new NumberChecker();

            var result = sut.IsLessThanTen(number);

            Assert.Equal(expectedResult, result);
        }
    }
}
