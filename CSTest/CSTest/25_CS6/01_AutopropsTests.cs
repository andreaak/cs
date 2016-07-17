using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSTest._25_CS6
{
    [TestClass]
    public class _01_AutopropsTests
    {
        //auto property initializing
        public Guid Id { get; } = Guid.NewGuid();

#if CS6
        [TestMethod]
        public void Test1()
        {

        }
#endif
    }
}
