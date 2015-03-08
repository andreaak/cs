using CS_TDD._003_DI._000_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._005_LocalFactoryMethod
{
    class FileManager
    {
        public FileManager()
        {
        }

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
