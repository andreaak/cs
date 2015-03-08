using CS_TDD._003_DI._000_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._007_FactoryInjection
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
