using System.Collections.Generic;

namespace Localization.Readers
{
    interface IReader
    {
        SortedDictionary<string, string> Read();
    }
}
