using CSTest._07_Generics._0_Setup;
using System.Diagnostics;

namespace CSTest._07_Generics
{
    delegate void ContrvarianceDelegate<in T>(T a);  // in - Для аргумента.
    class _08_DelegateContrvariance
    {
        public static void CatUser(Animal animal)
        {
            Debug.WriteLine(animal.GetType().Name);
        }
    }
}
