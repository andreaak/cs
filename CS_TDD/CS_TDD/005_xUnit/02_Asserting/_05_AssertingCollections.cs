using CS_TDD._005_xUnit._02_Asserting.Setup;
using System.Collections.Generic;
using System.Linq;
using CS_TDD._000_Base;
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
        public void Empty()
        {
            var names = new[] { "Sarah", "Amrit" };

            Assert.NotEmpty(names);
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

        [Fact]
        public void ValueTypesEqual()
        {
            IEnumerable<int> numbers1 = Enumerable.Range(1, 10);
            IEnumerable<int> numbers2 = Enumerable.Range(1, 10);

            Assert.Equal(numbers1, numbers2);
        }

        [Fact]
        public void ValueTypesNotEqual()
        {
            IEnumerable<int> numbers1 = Enumerable.Range(1, 10);
            IEnumerable<int> numbers2 = Enumerable.Range(1, 11);

            Assert.NotEqual(numbers1, numbers2);
        }

        [Fact]
        public void ReferenceTypesNoOverridenEquals()
        {
            var people1 = new List<Enemy>
                          {
                              new Enemy {Name = "Sarah"},
                              new Enemy {Name = "Gentry"}
                          };

            var people2 = new List<Enemy>
                          {
                              new Enemy {Name = "Sarah"},
                              new Enemy {Name = "Gentry"}
                          };

            Assert.NotEqual(people1, people2);
        }

        [Fact]
        public void ReferencesTypesWithOverriddenEquals()
        {
            var people1 = new List<PersonWithEquals>
                          {
                              new PersonWithEquals {Name = "Sarah"},
                              new PersonWithEquals {Name = "Gentry"}
                          };

            var people2 = new List<PersonWithEquals>
                          {
                              new PersonWithEquals {Name = "Sarah"},
                              new PersonWithEquals {Name = "Gentry"}
                          };

            Assert.Equal(people1, people2);
        }
    }
}