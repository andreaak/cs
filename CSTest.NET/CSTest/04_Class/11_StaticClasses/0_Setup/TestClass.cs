using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CSTest._04_Class._11_StaticClasses._0_Setup
{
    static class  BaseStaticClass //: BaseClass
        //Static class 'BaseStaticClass' cannot derive from type 'BaseClass'. Static classes must derive from object
    {
        public static string CompanyName = "Microsoft";
        public const string CompanyNameC = "Microsoft";
        public static readonly string CompanyNameR = "Microsoft";

        static BaseStaticClass()
        {

        }

        public static void Test()
        {
            Debug.WriteLine("MyBaseClass.Test()");
        }

        public static event Action Event;
        public static int Prop { get; set; }

        //public static int this[int i] 
        ////The modifier 'static' is not valid for this item 
        ////'BaseStaticClass.this[int]': cannot declare indexers in a static class
        //{
        //    get { return 1; }
        //}
    }

    //static class DerivedStaticClass : BaseStaticClass 
    //    //Static class 'DerivedStaticClass' cannot derive from type 'BaseStaticClass'. Static classes must derive from object
    //{ }

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
