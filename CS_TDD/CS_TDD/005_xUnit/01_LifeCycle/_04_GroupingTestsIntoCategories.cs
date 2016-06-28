using System;
using System.Threading;
using CS_TDD._000_Base;
using Xunit;

namespace CS_TDD._005_xUnit._01_LifeCycle
{
    [Trait("Category", "Error Checking")]
    public class CalculatorTests
    {
        [Fact]
        public void ShouldErrorWhenDivideByZero()
        {
            var sut = new MemoryCalculator();

            Assert.Throws<DivideByZeroException>(() => sut.Div(10, 0));
        }

        [Fact]
        [Trait("Category", "Slow Running")]
        public void ShouldErrorWhenNumberTooBig_SLOW()
        {
            var sut = new MemoryCalculator();

            // Simulate long running test
            Thread.Sleep(TimeSpan.FromSeconds(2));

            ArgumentOutOfRangeException thrownException =
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.Div(201, 1));

            Assert.Equal("value", thrownException.ParamName);
        }
    }
}
