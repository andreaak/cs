using System;
using CS_TDD._003_DI._000_Base;
using CS_TDD._003_DI._002_ConstructorInjection;
using NUnit.Framework;

namespace CS_TDD._003_DI._007_AbstractTestClassPattern.Test2
{

    class AbsBaseFileManagerTest<T> where T : IDataAccessObject
    {
        protected  T LocalFactoryMethod()
        {
            return (T) Activator.CreateInstance(typeof(T));
        }

        [Test]
        public void FindLogFileTestGeneric()
        {
            T dataAccessObj = LocalFactoryMethod();

            var mgr = new FileManager(dataAccessObj);

            var result = mgr.FindLogFile("file1.txt");

            Assert.IsTrue(result);
        }
    }
}
