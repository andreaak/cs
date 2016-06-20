using CS_TDD._005_xUnit._01_Asserting.Setup;
using Xunit;

namespace CS_TDD._005_xUnit._01_Asserting
{
    public class _01_AssertingNumericResults
    {
        [Fact]
        public void ShouldAddIntValues()
        {
            var sut = new Calculator();

            var result = sut.AddInts(1, 2);

            Assert.Equal(3, result);
        }


        [Fact]
        public void ShouldAddDoubleValues()
        {
            var sut = new Calculator();

            double result = sut.AddDoubles(1.1, 2.2);

            Assert.Equal(3.3, result, 1);
        }
    }
}
