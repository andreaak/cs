using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CS_TDD._000_Base;
using Xunit;
using Xunit.Sdk;

namespace CS_TDD._005_xUnit._03_DataDrivenTests
{
    /*
    Inherit from [DataAttribute]
    Implement GetDatamethod
    Use custom attribute in test method
    */
    public class _04_CreatingCustomDataSourceAttributes
    {
        [Theory]
        [CsvTestData("TestData.csv")]
        public void ShouldSubtractTwoNumbers(int firstNumber, int secondNumber, int expectedResult)
        {
            var sut = new MemoryCalculator();

            sut.Sub(firstNumber);
            sut.Sub(secondNumber);

            Assert.Equal(expectedResult, sut.CurrentValue);
        }

        [Theory]
        [RangeData(1, 10)]
        public void OneToTen(int number)
        {
            Assert.True(true);
        }

    }

    public class CsvTestDataAttribute : DataAttribute
    {
        private readonly string _csvFileName;

        public CsvTestDataAttribute(string csvFileName)
        {
            _csvFileName = csvFileName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            string[] csvLines = File.ReadAllLines(_csvFileName);

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

    public class RangeDataAttribute : DataAttribute
    {
        private readonly int _count;
        private readonly int _start;

        public RangeDataAttribute(int start, int count)
        {
            _start = start;
            _count = count;
        }

        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest)
        {
            return Enumerable.Range(_start, _count)
                .Select(i => new object[] { i });
        }
    }
}
