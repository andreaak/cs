using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

#if CS6
namespace CSTest._25_CS6
{
    [TestClass]
    public class _01_AutopropsTests
    {
        [TestMethod]
        public void Test1()
        {
            TestClass cut = new TestClass();
            Debug.WriteLine($"Id = {cut.Id} NameId = {cut.NameId} Name = {cut.Name}");
            //cut.Name = "NewName";
            /*
            Id = bcb2f3ed-603a-45b4-8ea7-79414cfeb4f1 NameId = 5 Name = TestStr
            */
        }
    }

    class TestClass
    {
        //auto property initializing
        public Guid Id { get; } = Guid.NewGuid();

        public int NameId { get; }

        public string Name { get; }

        public TestClass()
        {
            NameId = 5;
            Name = "TestStr";
        }
    }
}
#endif
