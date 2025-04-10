using System.Diagnostics;
using CSTest._04_Class._11_StaticClasses._0_Setup;
using NUnit.Framework;

namespace CSTest._04_Class._11_StaticClasses
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestStatic1FieldAndMethodsInheritance()
        {
            Debug.WriteLine(BaseClass.CompanyName);
            BaseClass.Test();

            //Статические поля и методы наследуются и доступны через имя класса наследника
            Debug.WriteLine(DerivedClass.CompanyName);
            DerivedClass.Test();

            BaseClass.CompanyName = "Google";
            Debug.WriteLine(BaseClass.CompanyName);
            Debug.WriteLine(DerivedClass.CompanyName);

            /*
            Microsoft
            MyBaseClass.Test()
            Microsoft
            MyBaseClass.Test()
            Google
            Google
            */
        }
    }
}
