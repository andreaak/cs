using CS_TDD._000_Base;
using Xunit;

namespace CS_TDD._005_xUnit._02_Asserting
{
    public class _01_AssertingNumericResults
    {
        [Fact]
        public void ShouldAddIntValues()
        {
            var sut = new MemoryCalculator();

            var result = sut.Add(1, 2);

            Assert.Equal(3, result);
        }

        [Fact]
        public void ShouldAddDoubleValues()
        {
            var sut = new MemoryCalculator();

            double result = sut.Add(1.1, 2.2);

            // Pass - first 1 decimal places are equal
            Assert.Equal(3.3, result, 1);
        }

        [Fact]
        public void DecimalExact()
        {
            var d1 = 24m;
            var d2 = 24m;

            Assert.Equal(d1, d2);
        }

        [Fact]
        public void DecimalWithPrecision()
        {
            var d1 = 24.111m;
            var d2 = 24.112m;

            // Pass - first 2 decimal places are equal
            Assert.Equal(d1, d2, 2);

            // Fail - third decimal place is not equal
            //Assert.Equal(d1, d2, 3);
        }
    }
}
