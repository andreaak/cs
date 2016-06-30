using CS_TDD._003_DI._000_Base;
using System.Collections.Generic;

namespace CS_TDD._003_DI._001_Problem
{
    class FileManager
    {
        public bool FindLogFile(string fileName)
        {
            // класс FileManager напрямую зависит от объектов доступа к данным, что затрудняет его расширение и тестирование.
            FileDataObject obj = new FileDataObject();
            // TestDataObject obj = new TestDataObject();

            List<string> files = obj.GetFiles();

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
