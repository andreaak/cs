using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._25_CS6
{
    [TestClass]
    public class _06_CollectionInitializersTests
    {
#if CS6
        [TestMethod]
        public void Test1()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("FirstName", "Ivan");
            dict.Add("LastName", "Ivanov");
            dict.Add("Age", 20.ToString());

            Debug.WriteLine(dict["FirstName"]);
            Debug.WriteLine(dict["LastName"]);
            Debug.WriteLine(dict["Age"]);

            dict = new Dictionary<string, string>()
            {
                {"FirstName", "Ivan"},
                {"LastName", "Ivanov"},
                {"Age", 20.ToString()},
            };

            Debug.WriteLine(dict["FirstName"]);
            Debug.WriteLine(dict["LastName"]);
            Debug.WriteLine(dict["Age"]);

            dict = new Dictionary<string, string>()
            {
                ["FirstName"] = "Ivan",
                ["LastName"] = "Ivanov",
                ["Age"] = 20.ToString(),
            };

            Debug.WriteLine(dict["FirstName"]);
            Debug.WriteLine(dict["LastName"]);
            Debug.WriteLine(dict["Age"]);

            Debug.WriteLine("-------------");

            //Debug.WriteLine(dict.$FirstName);
            Debug.WriteLine(dict["LastName"]);
            Debug.WriteLine(dict["Age"]);
        }
#endif
    }
}
