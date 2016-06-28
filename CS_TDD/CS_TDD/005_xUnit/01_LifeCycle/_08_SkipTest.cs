using Xunit;

namespace CS_TDD._005_xUnit._01_LifeCycle
{
    public class _08_SkipTest
    {
        [Fact(Skip = "Don't want this to run")]
        public void SkippedTest()
        {
            Assert.True(true);
        }
    }
}
