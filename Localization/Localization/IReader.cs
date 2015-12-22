using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Localization
{
    interface IReader
    {
        SortedDictionary<string, string> Read();
    }
}
