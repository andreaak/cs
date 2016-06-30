using CS_TDD._003_DI._000_Base;
using System;
using System.Collections.Generic;

namespace CS_TDD._003_DI._004_InterfaceInjection
{
    class FileManager
    {
        //Интерфейс, через который будет внедрена зависимость
        public bool FindLogFile(string fileName, IDataAccessObject dataAccessObject)
        {
            if (dataAccessObject == null)
            {
                throw new ArgumentNullException("dataAccessObject", "Parameter dataAcessObject cannot be null");
            }
            //Вызов метода Stub объекта
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
