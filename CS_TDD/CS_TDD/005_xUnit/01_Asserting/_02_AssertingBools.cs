using CS_TDD._005_xUnit._01_Asserting.Setup;
using Xunit;

namespace CS_TDD._005_xUnit._01_Asserting
{
    public class _02_AssertingBools
    {
        [Fact]
        public void ShouldHaveDefaultRandomGeneratedName()
        {
            var sut = new PlayerCharacter();

            Assert.False(string.IsNullOrWhiteSpace(sut.Name));
        }

        [Fact]
        public void ShouldBeNewbie()
        {
            var sut = new PlayerCharacter();

            Assert.True(sut.IsNoob);
        }
    }
}
