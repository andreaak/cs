using System;
using CS_TDD._000_Base;
using Xunit;

namespace CS_TDD._005_xUnit._02_Asserting
{
    public class _07_AssertingExceptions
    {
        [Fact]
        public void ShouldErrorWhenDivideByZero()
        {
            var sut = new MemoryCalculator();

            Assert.Throws<DivideByZeroException>(() => sut.Div(10, 0));
        }

        [Fact]
        public void ShouldErrorWhenNumberTooBig()
        {
            var sut = new MemoryCalculator();

            ArgumentOutOfRangeException thrownException =
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.Div(201, 1));

            Assert.Equal("value", thrownException.ParamName);
        }
    }
}
