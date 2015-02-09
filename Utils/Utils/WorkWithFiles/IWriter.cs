using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.WorkWithFiles
{
    interface IWriter
    {
        void WriteLine(object data);
        void Write(object data);
        void Close();
    }
}
