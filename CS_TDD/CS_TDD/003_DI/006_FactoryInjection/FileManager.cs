using System.Collections.Generic;
using CS_TDD._003_DI._000_Base;

namespace CS_TDD._003_DI._006_FactoryInjection
{
    class FileManager
    {
        IDataAccessObject dataAccessObject;

        public FileManager()
        {
            dataAccessObject = FactoryClass.CreateDataAccessObject();
        }

        public bool FindLogFile(string fileName)
        {
            List<string> files = dataAccessObject.GetFiles();

            foreach (var file in files)
            {
                if (file.Contains(fileName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
