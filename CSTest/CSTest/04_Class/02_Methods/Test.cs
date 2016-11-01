using NUnit.Framework;

namespace CSTest._04_Class._02_Methods
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestParameters1()
        {
            _02_Parameters param = new _02_Parameters();
            param.DefaultParameters(5);
            param.DefaultParameters(10, testStruct2: new TestStruct());
            
            param.DefaultParameters(20
                , 20
                , new TestStruct()
                , new TestStruct()
                , "PPP"
                , new object()
                , new TestClass()
                , new TestClass()
                , 20.0
                , 30.0);
            
            param.DefaultParameters(20
                , prm : null);
        }
    }
}
