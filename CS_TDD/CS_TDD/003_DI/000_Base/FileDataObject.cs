using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._000_Base
{
    public class FileDataObject : IDataAccessObject
    {
        public List<string> GetFiles()
        {
            string path = Directory.GetCurrentDirectory();

            List<string> list = new List<string>();

            DirectoryInfo d = new DirectoryInfo(path);

            FileInfo[] files = d.GetFiles();

            foreach (var file in files)
            {
                list.Add(file.Name);
            }

            return list;
        }
    }
}
