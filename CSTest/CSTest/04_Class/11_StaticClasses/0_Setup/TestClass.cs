using System.Diagnostics;

namespace CSTest._04_Class._11_StaticClasses._0_Setup
{
    class BaseClass
    {
        public static string CompanyName = "Microsoft";

        public static void Test()
        {
            Debug.WriteLine("MyBaseClass.Test()");
        }
    }

    class DerivedClass : BaseClass
    { }
}
