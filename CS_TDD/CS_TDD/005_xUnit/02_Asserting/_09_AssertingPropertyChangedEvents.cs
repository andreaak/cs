using CS_TDD._005_xUnit._02_Asserting.Setup;
using Xunit;

namespace CS_TDD._005_xUnit._02_Asserting
{
    public class _09_AssertingPropertyChangedEvents
    {
        [Fact]
        public void ShouldRaisePropertyChangedOnConvert()
        {
            var sut = new MainViewModel();

            Assert.PropertyChanged(sut, "Text", () => sut.ConvertToUpperCommand.Execute(null));
        }
    }
}
