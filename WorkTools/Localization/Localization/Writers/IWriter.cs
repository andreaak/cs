﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Localization.Writers
{
    interface IWriter
    {
        void Write(SortedDictionary<string, string> words);
    }
}
