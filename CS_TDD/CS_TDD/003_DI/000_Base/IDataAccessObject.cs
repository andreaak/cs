using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._000_Base
{
    interface IDataAccessObject
    {
        List<string> GetFiles();
    }
}
