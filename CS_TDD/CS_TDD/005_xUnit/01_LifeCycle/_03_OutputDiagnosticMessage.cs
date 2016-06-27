using CS_TDD._005_xUnit._02_Asserting.Setup;
using Xunit;
using Xunit.Abstractions;

namespace CS_TDD._005_xUnit._01_LifeCycle
{
    public class _03_OutputDiagnosticMessage
    {
        private readonly ITestOutputHelper _testOutput;

        public _03_OutputDiagnosticMessage(ITestOutputHelper helper)
        {
            _testOutput = helper;
        }

        [Fact]
        public void ShouldIncreaseHealthAfterSleeping()
        {
            _testOutput.WriteLine("Creating PlayerCharacter");
            var sut = new PlayerCharacter { Health = 100 };

            _testOutput.WriteLine("PlayerCharacter going to sleep");
            sut.Sleep();
            _testOutput.WriteLine("PlayerCharacter awoken");

            Assert.InRange(sut.Health, 101, 200);

            /*
            Creating PlayerCharacter
            PlayerCharacter going to sleep
            PlayerCharacter awoken
            */
        }
    }
}
