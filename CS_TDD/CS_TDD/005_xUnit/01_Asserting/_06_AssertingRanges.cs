using CS_TDD._005_xUnit._01_Asserting.Setup;
using Xunit;

namespace CS_TDD._005_xUnit._01_Asserting
{
    public class _06_AssertingRanges
    {
        [Fact]
        public void ShouldIncreaseHealthAfterSleeping()
        {
            var sut = new PlayerCharacter { Health = 100 };

            sut.Sleep();

            Assert.InRange(sut.Health, 101, 200);
        }
    }
}
