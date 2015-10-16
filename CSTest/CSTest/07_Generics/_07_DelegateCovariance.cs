using CSTest._07_Generics._0_SetUp;

namespace CSTest._07_Generics
{
    delegate T CovarianceDelegate<out T>();   // out - Для возвращаемого значения.
    
    class _07_DelegateCovariance
    {

        public static Cat CatCreator()
        {
            return new Cat();
        }
    }
}
