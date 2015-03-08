using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._000_Base
{
    class TestDataObject : IDataAccessObject
    {
        public List<string> GetFiles()
        {
            List<string> list = new List<string>();
            list.Add("file1.txt");
            list.Add("file2.log");
            list.Add("file3.exe");

            return list;
        }
    }
}
