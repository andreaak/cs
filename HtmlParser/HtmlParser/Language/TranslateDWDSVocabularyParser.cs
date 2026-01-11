using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Containers;
using HtmlParser.Language.Extensions;

namespace HtmlParser.Language
{
    public class TranslateDWDSVocabularyParser : LanguageParser, ILanguageParser
    {
        public TranslateDWDSVocabularyParser(Parameters parameters)
            : base(parameters.Order, parameters.WordType)
        { }

        public void Parse(IList<string> lines)
        {
            
            using (var sw = File.CreateText("notFound.txt"))
            {
                var temp = lines.Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(i => i.Replace("|", "").Trim())
                    .Distinct()
                    .SelectMany(l => Parse(l.Trim(), sw));

                //var list = temp.ToArray();
                
                using (var sw2 = File.CreateText("out.txt"))
                {
                    foreach (var item in temp)
                    {
                        item.Write(sw2);
                        sw2.Flush();
                    }
                }
            }
        }

        private IList<DWDSItem> Parse(string de, StreamWriter sw)
        {
            Console.WriteLine(de);

            if (_type == WordType.Subst)
            {
                de = de.RemoveArtikles();
            }
            
            var factory = new DWDSTranslationContainerFactory(de, _type);
            return factory.GetWords();
        }
    }
}