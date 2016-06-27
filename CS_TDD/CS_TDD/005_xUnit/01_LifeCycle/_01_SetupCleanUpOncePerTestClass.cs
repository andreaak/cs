using CS_TDD._005_xUnit._01_LifeCycle.Setup;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CS_TDD._005_xUnit._01_LifeCycle
{

    /*
    Run code before first test executes
    Run code after all test have executed
    Expensive object creation
    Custom “fixture” class 
    IClassFixture<T>
    */
    public class MemoryCalculatorFixture : IDisposable
    {
        public MemoryCalculator Sut { get; private set; }

        public MemoryCalculatorFixture()
        {
            Sut = new MemoryCalculator(true);
        }

        public void Dispose()
        {
            Sut.Dispose();
        }
    }

    public class _01_SetupCleanUpOncePerTestClass : IClassFixture<MemoryCalculatorFixture>
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly MemoryCalculatorFixture _fixture;

        public _01_SetupCleanUpOncePerTestClass(ITestOutputHelper helper, MemoryCalculatorFixture fixture)
        {
            _testOutput = helper;
            _fixture = fixture;
        }

        [Fact]
        public void ShouldAdd()
        {
            _testOutput.WriteLine("Executing ShouldAdd");

            _fixture.Sut.Add(10);
            _fixture.Sut.Add(5);

            Assert.Equal(15, _fixture.Sut.CurrentValue);
        }

        [Fact]
        public void ShouldSubtract()
        {
            _testOutput.WriteLine("Executing ShouldSubtract");

            _fixture.Sut.Subtract(5);

            Assert.Equal(-5, _fixture.Sut.CurrentValue);
        }

        [Fact]
        public void ShouldDivide()
        {
            _testOutput.WriteLine("Executing ShouldDivide");

            _fixture.Sut.Add(10);
            _fixture.Sut.Divide(2);

            Assert.Equal(5, _fixture.Sut.CurrentValue);
        }

    }
}
