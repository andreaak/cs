using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSTest._01_Elements_CS._01_Types
{
    [TestClass]
    public class _08_Nullable
    {
        [TestMethod]
        public void TestNullable1()
        {
            int? j = 0;//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0)
            int? l = new int();//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0)
            int? m = default(int);//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0)
            int? n = default(int?);//initobj valuetype [mscorlib]System.Nullable`1<int32>
            int? i = null;//initobj valuetype [mscorlib]System.Nullable`1<int32>
            int? k = 5;//call instance void valuetype [mscorlib]System.Nullable`1<int32>::.ctor(!0
        }
    }
}
