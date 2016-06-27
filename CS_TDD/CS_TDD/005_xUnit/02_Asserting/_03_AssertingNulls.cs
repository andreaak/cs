using CS_TDD._005_xUnit._02_Asserting.Setup;
using Xunit;

namespace CS_TDD._005_xUnit._02_Asserting
{
    public class _03_AssertingNulls
    {
        [Fact]
        public void ShouldNotHaveANickName()
        {
            var sut = new PlayerCharacter();

            Assert.Null(sut.NickName);
        }
    }
}
