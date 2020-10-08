using System.Diagnostics;

namespace CSTest._05_Delegates_and_Events._01_Delegates._0_Setup
{
    class BaseClass
    {
    }

    class DerivedClass : BaseClass
    {
    }

    class TestCoContrVarianceClass
    {
        //base
        public BaseClass FirstMethod(DerivedClass arg)
        {
            Debug.WriteLine("FirstMethod - base");
            Debug.WriteLine("In param: " + typeof(DerivedClass));
            return new BaseClass();
        }

        //covariance
        public DerivedClass Covariant(DerivedClass arg)
        {
            Debug.WriteLine("SecondMethod - covariance");
            Debug.WriteLine("In param: " + typeof(DerivedClass));
            return new DerivedClass();
        }

        //contrvariance
        public BaseClass Contrvariant(BaseClass arg)
        {
            Debug.WriteLine("ThirdMethod - contrvariance");
            Debug.WriteLine("In param: " + typeof(BaseClass));
            return new BaseClass();
        }

        //covariance and contrvariance
        public DerivedClass CoContrvariant(BaseClass arg)
        {
            Debug.WriteLine("FourthMethod - covariance and contrvariance");
            Debug.WriteLine("In param: " + typeof(BaseClass));
            return new DerivedClass();
        }

        public object TestMethodRefBase(string p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return new object();
        }

        public string TestMethodRefCoContrvariant(object p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return "TEST";
        }

        public object TestMethodValBase(int p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return new object();
        }

        public int TestMethodValCovariant(int p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return 22;
        }

        public object TestMethodValContrvariant(object p)
        {
            Debug.WriteLine("Parameter: " + p.GetType());
            return 22;
        }
    }
}
