using NUnit.Framework;

namespace CS_TDD._002_NUnit._01_LifeCycle
{
    [TestFixture]
    class _03_SuccessorTest : ClassUnderTest
    {
        [Test]
        public void Ignore()
        {
            Id = 5;
            Assert.AreEqual(Id, 5);
        }
    }

    class ClassUnderTest
    {
        protected int Id;
    }
}
