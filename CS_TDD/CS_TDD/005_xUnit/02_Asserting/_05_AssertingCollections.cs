using CS_TDD._005_xUnit._02_Asserting.Setup;
using Xunit;

namespace CS_TDD._005_xUnit._02_Asserting
{
    public class _05_AssertingCollections
    {
        [Fact]
        public void ShouldHaveNoEmptyDefaultWeapons()
        {
            var sut = new PlayerCharacter();

            Assert.All(sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        }

        [Fact]
        public void ShouldHaveALongBow()
        {
            var sut = new PlayerCharacter();

            Assert.Contains("Long Bow", sut.Weapons);
        }


        [Fact]
        public void ShouldNotHaveAStaffOfWonder()
        {
            var sut = new PlayerCharacter();

            Assert.DoesNotContain("Staff Of Wonder", sut.Weapons);
        }


        [Fact]
        public void ShouldHaveAtLeastOneKindOfSword()
        {
            var sut = new PlayerCharacter();

            Assert.Contains(sut.Weapons, weapon => weapon.Contains("Sword"));
        }


        [Fact]
        public void ShouldHaveAllExpectedWeapons()
        {
            var sut = new PlayerCharacter();

            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };


            Assert.Equal(expectedWeapons, sut.Weapons);
        }
    }
}