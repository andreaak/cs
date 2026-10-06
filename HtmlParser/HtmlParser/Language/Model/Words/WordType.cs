using System;

namespace HtmlParser.Language.HTMLClients
{
    [Flags]
    public enum WordType
    {
        None = 0,
        Verb = 1,
        Subst = 2,
        Pron = 4,
        Adv = 8,
        Adj = 16,
        Konj = 32,
        Prep = 64,
        All = 128,
        Complex = 256
    }
}