﻿using System.Collections.Generic;
using CS_TDD._003_DI._000_Base;

namespace CS_TDD._003_DI._002_ConstructorInjection
{
    class FileManager
    {
        IDataAccessObject dataAccessObject;

        public FileManager()
        {
            this.dataAccessObject = new FileDataObject();
        }
        //Конструктор, через который будет внедрена зависимость
        public FileManager(IDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject;
        }

        public bool FindLogFile(string fileName)
        {
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
