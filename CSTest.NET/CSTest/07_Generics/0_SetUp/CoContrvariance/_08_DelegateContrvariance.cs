using System.Diagnostics;

namespace CSTest._07_Generics._0_Setup.CoContrvariance
{
    delegate void ContrvarianceDelegate<in T>(T a);  // in - Для аргумента.
    class _08_DelegateContrvariance
    {
        public static void CatUser(Base animal)
        {
            Debug.WriteLine(animal.GetType().Name);
        }
    }
}
