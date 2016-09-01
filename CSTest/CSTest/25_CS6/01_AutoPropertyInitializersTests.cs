using NUnit.Framework;
using System;
using System.Diagnostics;

#if CS6
namespace CSTest._25_CS6
{
    [TestFixture]
    public class _01_AutoPropertyInitializersTests
    {
        [Test]
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
        //public Guid Id { get; protected set; }

        //public TestClass()
        //{
        //    Id = Guid.NewGuid();
        //}
        
        //auto property initializing
        public Guid Id { get; } = Guid.NewGuid();

        //auto property with getter only - can set only in ctor
        public int NameId { get; } = 3;

        public string Name { get; set; } = "Test";

        public TestClass()
        {
            NameId = 5;
            Name = "TestStr";
        }

        public void SetName()
        {
            //Name = "Test2"; can be set only in ctor
        }
    }
}
#endif
