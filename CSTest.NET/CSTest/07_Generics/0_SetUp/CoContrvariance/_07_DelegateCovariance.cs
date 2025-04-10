namespace CSTest._07_Generics._0_Setup.CoContrvariance
{
    delegate T CovarianceDelegate<out T>();   // out - Для возвращаемого значения.
    
    class _07_DelegateCovariance
    {

        public static Derived CatCreator()
        {
            return new Derived();
        }
    }
}
