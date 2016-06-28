using CS_TDD._000_Base;
using CS_TDD._005_xUnit._02_Asserting.Setup;
using Xunit;

namespace CS_TDD._005_xUnit._02_Asserting
{
    public class _08_AssertingObjectTypes
    {
        [Fact]
        public void ComparingValueTypes()
        {
            var sut = new MemoryCalculator();

            var result = sut.Add(10, 5);

            Assert.Equal(15, result);
        }

        [Fact]
        public void ComparingReferenceTypes()
        {
            var p1 = new Enemy { Name = "Sarah" };
            var p2 = new Enemy { Name = "Sarah" };

            // Assert.Equal(p1, p2); // Fail - different references

            var p3 = p1;

            Assert.Equal(p1, p3); // Pass - same reference
        }

        [Fact]
        public void ComparingReferenceTypesWithOverriddenEquals()
        {
            var p1 = new PersonWithEquals { Name = "Sarah" };
            var p2 = new PersonWithEquals { Name = "Sarah" };

            Assert.Equal(p1, p2);// Pass
        }

        [Fact]
        public void CustomIEqualityComparer()
        {
            var p1 = new PersonWithEquals { Name = "Sarah" };
            var p2 = new PersonWithEquals { Name = "Simon" };

            // Using an explicit IEqualityComparer works
            // in place of PersonWithEquals.Equals
            Assert.Equal(p1, p2, new FirstLetterEqualityComparer());
        }

        [Fact]
        public void ShouldCreateNormalEnemy_SimpleExample()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(false);

            Assert.IsType(typeof (NormalEnemy), enemy);
        }

        [Fact]
        public void ShouldCreateNormalEnemy_Cast()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(false);

            NormalEnemy normalEnemy = Assert.IsType<NormalEnemy>(enemy);

            Assert.Equal(10, normalEnemy.Power);
        }

        [Fact]
        public void ShouldCreateNormalEnemy_IncludeDerivedTypes()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(false);

            Assert.IsAssignableFrom(typeof (Enemy), enemy);
        }

        [Fact]
        public void ShouldCreateNormalEnemy_IncludeDerivedTypes_Cast()
        {
            var sut = new EnemyFactory();

            object enemy = sut.Create(false);

            Enemy normalEnemy = Assert.IsAssignableFrom<Enemy>(enemy);

            Assert.Equal("Default Name", normalEnemy.Name);
        }
    }
}