using Xunit;

namespace CS_TDD._005_xUnit._01_LifeCycle
{
    /*
    Default test collection per test class
    Multiple test classes into one collection
    [Collection]
    Customized ICollectionFixture<T>
    [CollectionDefinition] 
    */

    [CollectionDefinition("MemoryCalculator Collection")]
    public class MemoryCalculatorCollection : ICollectionFixture<MemoryCalculatorFixture>
    {
    }

    [Collection("MemoryCalculator Collection")]
    public class MemoryCalculatorAddTests
    {
        private readonly MemoryCalculatorFixture _fixture;

        public MemoryCalculatorAddTests(MemoryCalculatorFixture fixture)
        {
            _fixture = fixture;

            _fixture.Sut.Clear();
        }

        [Fact]
        public void ShouldAddPositiveNumbers()
        {
            _fixture.Sut.Add(10);
            _fixture.Sut.Add(5);

            Assert.Equal(15, _fixture.Sut.CurrentValue);
        }

        [Fact]
        public void ShouldAddNegativeNumbers()
        {
            _fixture.Sut.Add(-5);
            _fixture.Sut.Add(-5);

            Assert.Equal(-10, _fixture.Sut.CurrentValue);
        }
    }

    [Collection("MemoryCalculator Collection")]
    public class MemoryCalculatorSubtractTests
    {
        private readonly MemoryCalculatorFixture _fixture;

        public MemoryCalculatorSubtractTests(MemoryCalculatorFixture fixture)
        {
            _fixture = fixture;

            _fixture.Sut.Clear();
        }

        [Fact]
        public void ShouldSubtractPositiveNumbers()
        {
            _fixture.Sut.Subtract(5);

            Assert.Equal(-5, _fixture.Sut.CurrentValue);
        }

        [Fact]
        public void ShouldSubtractNegativeNumbers()
        {
            _fixture.Sut.Subtract(-5);

            Assert.Equal(5, _fixture.Sut.CurrentValue);
        }
    }
}
