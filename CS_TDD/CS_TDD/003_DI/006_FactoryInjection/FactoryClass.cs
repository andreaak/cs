using CS_TDD._003_DI._000_Base;

namespace CS_TDD._003_DI._006_FactoryInjection
{
    class FactoryClass
    {
        static IDataAccessObject dataAcessObject;

        internal static IDataAccessObject CreateDataAccessObject()
        {
            if (dataAcessObject != null)
            {
                return dataAcessObject;
            }

            return new FileDataObject();
        }

#if DEBUG
        public static void SetDataAccessObject(IDataAccessObject customDataObject)
        {
            dataAcessObject = customDataObject;
        }
#endif
    }
}
