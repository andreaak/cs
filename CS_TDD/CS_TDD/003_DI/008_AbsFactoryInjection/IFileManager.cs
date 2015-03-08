using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_TDD._003_DI._008_AbsFactoryInjection
{
    public interface IFileManager
    {
        bool FindLogFile(string fileName);
    }
}
