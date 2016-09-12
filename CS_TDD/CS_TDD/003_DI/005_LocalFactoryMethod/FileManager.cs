using CS_TDD._003_DI._000_Base;
using System.Collections.Generic;

namespace CS_TDD._003_DI._005_LocalFactoryMethod
{
    class FileManager
    {
        protected virtual IDataAccessObject LocalFactoryMethod()
        {
            return new FileDataObject();
        }

        public bool FindLogFile(string fileName)
        {
            var dataAccessObject = LocalFactoryMethod();

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
