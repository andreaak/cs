using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    class TranscriptionResponse
    {
        public string Text { get; set; }
    }

    public class TranslateRuDeParser : LanguageParser, ILanguageParser
    {
        public TranslateRuDeParser(bool order, string type)
        : base(order, type)
        { }
        
        public void Parse(IList<string> lines)
        {
            var list = _order ?
                lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => Parse(l.Trim()))
                    .OrderBy(l => l.De)
                    .ToArray()
                :
                lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => Parse(l.Trim()))
                    .ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        protected WordClass Parse(string ru)
        {
            Console.WriteLine(ru);

            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/russisch-deutsch/";

            var document = GetHtml(hostUrl + ru);

            string de = null;
            string wordClass = null;
            string link = null;

            var translationContainerRu = GetTranslationContainer(document, ru);
            if (translationContainerRu?.Node != null)
            {
                if (string.IsNullOrEmpty(_type))
                {
                    wordClass = translationContainerRu.Node.GetWordClass();
                }
                de = translationContainerRu.Node.GetTranslation();
                link = translationContainerRu.Node.GetLink();
            }

            if (string.IsNullOrEmpty(de))
            {
                Console.WriteLine($"Not found {ru}");
                return new WordClass
                {
                    Ru = ru
                };
            }

            hostUrl = link == null ? $"https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/{de}" : $"https://de.pons.com{link}";
            document = GetHtml(hostUrl);
            
            var translationContainerDe = GetTranslationContainer(document, de, _type ?? wordClass);
            if (translationContainerDe == null)
            {
                Console.WriteLine($"Not found {de}");
                if ((_type ?? wordClass)?.ToLower() == "subst")
                {
                    var nd = document.DocumentNode.SelectSingleNode(".//div[@class='results']//div[@class='target']");
                    var genus = nd?.SelectSingleNode(".//span[@class='genus']")?.InnerText.Trim().ToLower();
                    return new Substantiv
                    {
                        De = de,
                        Ru = ru,
                        Genus = genus,
                        Artikle = GetArtikle(genus)
                    };
                }
                return new WordClass
                {
                    De = de,
                    Ru = ru,
                    
                };
            }

            if (translationContainerDe.Node == null)
            {
                return new WordClass
                {
                    De = de,
                    Ru = ru,
                    DeTranscription = GetTranscription(translationContainerDe.Node, translationContainerDe.AllNodes)
                };
            }

            var word = GetWord(translationContainerDe, de);
            word.Ru = ru;
            UploadSound(translationContainerDe.Node, word.De);

            return word;
        }
    }
}