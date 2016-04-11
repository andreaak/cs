using CS_TDD._003_DI._000_Base.Test;
using NUnit.Framework;

namespace CS_TDD._003_DI._007_AbstractTestClassPattern.Test2
{
    [TestFixture]
    class ConcreteFIleManagerTest : AbsBaseFileManagerTest<StubFileDataObject>
    {
        [Test]
        public void AbstractBaseTestClassTest()
        {
            FindLogFileTestGeneric();
        }
    }
}
