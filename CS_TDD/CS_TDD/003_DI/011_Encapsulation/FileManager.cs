using CS_TDD._003_DI._000_Base;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

//Делает internal типы видимыми для заданной сборки
[assembly: InternalsVisibleTo("011_EncapsulationTest")]
//Как правило типы и элементы с модификатором доступа internal доступны только в сборке, в которой они определены. 
//Атрибут InternalsVisibleToAttribute делает их также видимыми для типов в указанной сборке, которая называется "дружественная сборка".


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
