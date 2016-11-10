using NUnit.Framework;

namespace CSTest._25_CS_NewFeatures._03_CS6
{
    [TestFixture]
    public class _02_ExpressionBodiedMembersTests
    {
#if CS6
        //public int Id
        //{
        //    get { return 10 * 5; }
        //}

        //or
        public int Id => 10 * 5;//expression bodied property

        [Test]
        public void Test1()
        {

        }

        //public override string ToString()
        //{
        //    return "New";
        //}

        //or
        public override string ToString() => "New";//expression bodied method

        public static _02_ExpressionBodiedMembersTests Add(_02_ExpressionBodiedMembersTests first, _02_ExpressionBodiedMembersTests second) => first + second;

        public static _02_ExpressionBodiedMembersTests operator +(_02_ExpressionBodiedMembersTests first, _02_ExpressionBodiedMembersTests second) => new _02_ExpressionBodiedMembersTests();//expression bodied operator

#endif
    }
}
