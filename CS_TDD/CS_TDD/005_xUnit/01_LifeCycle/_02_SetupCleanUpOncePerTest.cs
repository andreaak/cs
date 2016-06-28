using System;
using CS_TDD._000_Base;
using Xunit;
using Xunit.Abstractions;

namespace CS_TDD._005_xUnit._01_LifeCycle
{
    /*
    Constructor for setup code
    Dispose() for cleanup code 
    */
    public class _02_SetupCleanUpOncePerTest : IDisposable
    {
        private readonly ITestOutputHelper _testOutput;

        private MemoryCalculator _sut;

        public _02_SetupCleanUpOncePerTest(ITestOutputHelper helper)
        {
            _testOutput = helper;

            _testOutput.WriteLine("Creating sut");
            _sut = new MemoryCalculator();
        }

        [Fact]
        public void ShouldAdd()
        {
            _testOutput.WriteLine("Executing ShouldAdd");

            _sut.Add(10);
            _sut.Add(5);

            Assert.Equal(15, _sut.CurrentValue);
        }

        [Fact]
        public void ShouldSubtract()
        {
            _testOutput.WriteLine("Executing ShouldSubtract");

            _sut.Sub(5);

            Assert.Equal(-5, _sut.CurrentValue);
        }

        [Fact]
        public void ShouldDivide()
        {
            _testOutput.WriteLine("Executing ShouldDivide");

            _sut.Add(10);
            _sut.Div(2);

            Assert.Equal(5, _sut.CurrentValue);
        }

        public void Dispose()
        {
            _testOutput.WriteLine("Disposing sut");
            _sut.Dispose();
        }
    }
}
