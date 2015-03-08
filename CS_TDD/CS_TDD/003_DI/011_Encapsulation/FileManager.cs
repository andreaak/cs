using CS_TDD._003_DI._000_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("011_EncapsulationTest")]

namespace CS_TDD._003_DI._011_Encapsulation
{
    class FileManager
    {
        IDataAccessObject dataAccessObject;

        public FileManager()
        {
            this.dataAccessObject = new FileDataObject();
        }

        internal FileManager(IDataAccessObject dataAccessObject)
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
