using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Localization
{
    interface IParser
    {
        bool IsEmptyLine(string line);

        bool IsCommentLine(string line);

        bool IsBaseLine(string line);

        bool IsLocalizedLine(string line);
        
        string GetBaseValue(string line);

        string GetLocalizedValue(string line);
    }
}
