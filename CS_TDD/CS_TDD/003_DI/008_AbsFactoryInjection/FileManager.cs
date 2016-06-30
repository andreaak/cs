using CS_TDD._003_DI._000_Base;
using System.Collections.Generic;

namespace CS_TDD._003_DI._008_AbsFactoryInjection
{
    class FileManager : IFileManager
    {
        IDataAccessObject dataAccessObject;

        public FileManager(IDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject;
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
