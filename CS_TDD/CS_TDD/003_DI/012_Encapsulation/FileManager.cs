using System.Collections.Generic;
using CS_TDD._003_DI._000_Base;

namespace CS_TDD._003_DI._012_Encapsulation
{
    class FileManager
    {
        IDataAccessObject dataAccessObject;

#if DEBUG

        public FileManager(IDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject;
        }
#else
        public FileManager()
        {
            this.dataAccessObject = new FileDataObject();
        }
#endif

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
