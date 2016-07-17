using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CSTest._25_CS6
{
    [TestClass]
    public class _06_CollectionInitializersTests
    {
#if CS6
        Dictionary<string, string> _defaultUsers
        = new Dictionary<string, string>()
        {
            ["admin"] = "admin",
            ["guest"] = "guest",
        };

        [TestMethod]
        public void Test1()
        {

        }
#endif
    }
}
